using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.player
{
	public class PlayerView : MonoBehaviour
	{
		[SerializeField]
		private Rigidbody2D rigidbody;
		[SerializeField] private Animator animator;
		[SerializeField] private Animator gunAnimator;

		//shooting
		[Header("Movement and shooting")]
		[SerializeField]
		private Camera _camera;
		[SerializeField]
		private Transform aimObject;
		[SerializeField]
		private Transform firePoint;
		[SerializeField]
		private LineRenderer lineRenderer;
		[SerializeField]
		private GameObject line;
        [SerializeField]
        private LayerMask enemyLayer;

		//health
		[Header("Health")]
		[SerializeField]
		private Transform healthBar;
        [SerializeField]
        private Slider healthSlider;
        [SerializeField]
		private ParticleSystem bloodParticle;

        private PlayerController playerController;

		public void SetPlayerController(PlayerController playerController)
		{
			this.playerController = playerController;
		}

		private void Start()
        {
            playerController.Start();
        }

        private void Update()
		{
			playerController.Update();
		}

		private void FixedUpdate()
		{
		   playerController.FixedUpdate();
		}

		public Transform GetPlayerTransform()=> transform;
		public Rigidbody2D GetRigidBody() => rigidbody;
        public Animator GetAnimator() => animator;

        public Camera GetCamera() => _camera;
        public Transform GetAimObject() => aimObject;

        public Transform GetHealthBar() => healthBar;
        public Slider GetHealthSlider() => healthSlider;
        public ParticleSystem GetBloodParticle() => bloodParticle;

        public void ShootingLine()
        {
            if (Input.GetMouseButtonDown(0)) StartCoroutine(HandleShooting());
        }

        private IEnumerator HandleShooting()
        {
            gunAnimator.SetTrigger("shoot");

            lineRenderer.enabled = true;
            line.SetActive(true);

            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, Mathf.Infinity, enemyLayer);
            Vector3 endPoint = hitInfo ? hitInfo.point : (firePoint.position + firePoint.right * 100);

            lineRenderer.SetPositions(new Vector3[] { firePoint.position, endPoint });

            if (hitInfo.collider != null && hitInfo.transform.CompareTag("enemy"))
            {
                // hitInfo.transform.GetComponent<Enemy>()?.TakeDamage(); // Uncomment if needed
            }

            yield return new WaitForSeconds(0.02f);

            lineRenderer.enabled = false;
        }

    }

}