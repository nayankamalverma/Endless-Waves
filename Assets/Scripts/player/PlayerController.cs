using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Events;
using System.Collections;
using Assets.Scripts.Enemy;
using UnityEngine;
using Assets.Scripts.Utilities.VFX;

namespace Assets.Scripts.player
{
	public class PlayerController
	{
		private Vector3 mousePosition;
		float angle;
		Vector3 playerScale;
		private Vector2 movement;
		private int kills;
		private bool IsActive;

		private PlayerView playerView;
		private PlayerModel playerModel;
		private EventService eventService;
		private CoroutineRunner coroutineRunner;

		public PlayerController(EventService eventService, PlayerScriptableObject playerSO, PlayerView playerView)
		{
			this.eventService = eventService;
			this.playerView = playerView;

			playerView.SetPlayerController(this);
			playerModel = new PlayerModel(playerSO);
			coroutineRunner = CoroutineRunner.Instance;
			AddEventListeners();
		}

		private void AddEventListeners()
		{
			eventService.OnGameStart.AddListener(OnGameStart);
			eventService.OnGameResume.AddListener(OnGameResume);
			eventService.OnPlayerHurt.AddListener(TakeDamage);
			eventService.OnEnemyKilled.AddListener(UpdateKills);
			eventService.OnGameOverPauseMenu.AddListener(GameOver);
		}

		public void Start()
		{
			playerScale = playerView.GetPlayerTransform().localScale;
		}

		public void OnGameStart()
		{
			playerView.GetAnimator().Play("player_idel");
			playerView.GetPlayerTransform().position = Vector3.zero;
			SetMaxHealth();
			kills = 0;
			IsActive = true;
		}

		private void GamePause()
		{
			IsActive = false;
			eventService.OnGamePause.Invoke();
		}
		private void OnGameResume()
		{
			IsActive = true;
		}

		public void Update()
		{
			if (!IsActive) return;
			if (Input.GetKeyDown(KeyCode.Escape)) GamePause();

			movement.x = Input.GetAxisRaw("Horizontal");
			movement.y = Input.GetAxisRaw("Vertical");
			movement = movement.normalized;
			float speed = movement.magnitude;

			playerView.GetAnimator().SetFloat("speed", speed);
			//shooting
			HandleAim();
			HandelShooting();

		}

		public void FixedUpdate()
		{
			if (!IsActive) return;
			Vector2 targetVelocity = movement * playerModel.GetMoveSpeed();
			playerView.GetRigidBody().linearVelocity =
			Vector2.Lerp(playerView.GetRigidBody().linearVelocity, targetVelocity, 0.25f);
		}

		#region health
		public void LateUpdate()
		{
			if (IsActive) playerView.GetHealthBar().position = playerView.GetRigidBody().position;
		}

		private void TakeDamage(int health)
		{
			playerModel.ReduceHealth(health);
			UpdateHealthSlider();
			if (playerModel.GetCurrentHealth() <= 0)
			{
				GameOver();
			}
			VFXService.Instance.PlayVFX(VFXType.Blood, playerView.GetAimObject().transform.position);
		}


		private void SetMaxHealth()
		{
			playerModel.SetCurrentHealthToMax();
			playerView.GetHealthSlider().maxValue = playerModel.GetMaxHealth();
			UpdateHealthSlider();
		}

		private void Heal(int hp)
		{
			playerModel.Heal(hp);
			if (playerModel.GetCurrentHealth() > playerModel.GetMaxHealth())
			{
				playerModel.SetCurrentHealthToMax();
			}
			UpdateHealthSlider();
		}

		private void UpdateHealthSlider()
		{
			playerView.GetHealthSlider().value = playerModel.GetCurrentHealth();
		}

		private void GameOver()
		{
			SoundManger.Instance.Play(Sounds.GameOver);
			playerView.GetAnimator().SetTrigger("death");
			eventService.OnGameOver.Invoke(kills);
			IsActive = false;
		}
		#endregion

		#region shooting

		private void HandleAim()
		{
			mousePosition = playerView.GetCamera().ScreenToWorldPoint(Input.mousePosition);

			Vector3 lookDirection = (mousePosition - playerView.GetAimObject().position).normalized;
			angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
			playerView.GetAimObject().eulerAngles = new Vector3(0, 0, angle);

			bool shouldFlip = angle > 90 || angle < -90;
			Vector3 aimScale = shouldFlip ? new Vector3(-1f, -1f, 1f) : Vector3.one;
			playerScale.x = shouldFlip ? -0.4f : Mathf.Abs(playerScale.x);

			playerView.GetAimObject().localScale = aimScale;
			playerView.GetPlayerTransform().localScale = playerScale;
		}

		private void HandelShooting()
		{
			if (Input.GetMouseButtonDown(0)) coroutineRunner.StartCoroutine(HandleShooting());
		}

		private IEnumerator HandleShooting()
		{
			var firePoint = playerView.GetFirePoint();
			var firePointPos = firePoint.position;
			var fireDirection = firePoint.right;

			playerView.GetGunAnimator().SetTrigger("shoot");
			SoundManger.Instance.Play(Sounds.Shoot);
			playerView.GetLineRenderer().enabled = true;
			playerView.GetLine().SetActive(true);

			RaycastHit2D hitInfo = Physics2D.Raycast(firePointPos, fireDirection, Mathf.Infinity, playerView.GetEnemyLayer());
			Vector3 endPoint = hitInfo ? hitInfo.point : (firePointPos + fireDirection * 100);

			playerView.GetLineRenderer().SetPositions(new[] { firePointPos, endPoint });

			if (hitInfo.collider != null && hitInfo.transform.CompareTag("enemy"))
			{
				if (hitInfo.transform.TryGetComponent<EnemyController>(out var enemyController))
				{
					eventService.OnEnemyHurt.Invoke(enemyController);
				}
			}
			yield return new WaitForSeconds(0.02f);
			playerView.GetLineRenderer().enabled = false;
		}

		private void UpdateKills()
		{
			kills++;
		}
		#endregion
	}
}