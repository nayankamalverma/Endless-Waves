using UnityEngine;

namespace Assets.Scripts.player
{
    public class PlayerController
    {
        private Vector3 mousePosition;
        float angle;
        Vector3 playerScale;
        private Vector2 movement;

        private PlayerView playerView;
        private PlayerModel playerModel;        

        public PlayerController(PlayerScriptableObject playerSO, PlayerView playerView)
        {
            this.playerView = playerView;
            playerView.SetPlayerController(this);
            playerModel = new PlayerModel(playerSO);
        }

        public void Start()
        {
            SetMaxHealth();
            playerScale = playerView.GetPlayerTransform().localScale;
        }

        public void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement = movement.normalized;
            float speed = movement.magnitude;

            playerView.GetAnimator().SetFloat("speed", speed);
            //shooting

            HandleAim();
            playerView.ShootingLine();

        }

        public void FixedUpdate()
        {
            Vector2 targetVelocity = movement * playerModel.GetMoveSpeed();

            playerView.GetRigidBody().linearVelocity = Vector2.Lerp(playerView.GetRigidBody().linearVelocity, targetVelocity, 0.25f);
        }

        #region health
        public void LateUpdate()
        {
            playerView.GetHealthBar().position = playerView.GetRigidBody().position;
        }

        public void TakeDamage(int health)
        {
            playerModel.ReduceHealth(health);
            UpdateHealthSlider();
            if (playerModel.GetCurrentHealth() <= 0)
            {
                GameOver();
            }
            GameObject.Instantiate(playerView.GetBloodParticle(), new Vector3(playerView.GetPlayerTransform().position.x, playerView.GetPlayerTransform().position.y, -4f), playerView.GetBloodParticle().transform.rotation);
        }


        public void SetMaxHealth()
        {
            playerModel.SetCurrentHealthToMax();
            playerView.GetHealthSlider().maxValue = playerModel.GetMaxHealth();
            UpdateHealthSlider();
        }

        public void Heal(int hp)
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

        public void GameOver()
        {
            SoundManger.Instance.Play(Sounds.GameOver);
            playerView.GetAnimator().SetTrigger("death");
        }
        #endregion

        #region shooting

        private void HandleAim()
        {
            mousePosition = playerView.GetCamera().ScreenToWorldPoint(Input.mousePosition);

            Vector3 normalized = (mousePosition - playerView.GetAimObject().position).normalized;
            Vector3 lookDirection = normalized;
            angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            playerView.GetAimObject().eulerAngles = new Vector3(0, 0, angle);

            Vector3 aimScale = Vector3.one;
            if (angle > 90 || angle < -90)
            {
                aimScale.x = -1f;
                aimScale.y = -1f;
                playerScale.x = -0.4f;
            }
            else
            {
                aimScale = Vector3.one;
                playerScale.x = Mathf.Abs(playerScale.x);
            }
            playerView.GetAimObject().localScale = aimScale;
            playerView.GetPlayerTransform().localScale = playerScale;
        }

        
        #endregion
    }
}
