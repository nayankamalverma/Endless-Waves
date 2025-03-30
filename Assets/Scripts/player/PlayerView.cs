using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody;
        [SerializeField] private Animator animator;
        [SerializeField] private Animator gunAnimator;

        //shooting
        [Header("Movement and shooting")] [SerializeField]
        private Camera _camera;

        [SerializeField] private Transform aimObject;
        [SerializeField] private Transform firePoint;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private GameObject line;
        [SerializeField] private LayerMask enemyLayer;

        //health
        [Header("Health")] [SerializeField] private Transform healthBar;
        [SerializeField] private Slider healthSlider;

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

        private void LateUpdate()
        {
            playerController.LateUpdate();
        }

        private void OnDestroy()
        {
            playerController.OnDestroy();
        }

        public Transform GetPlayerTransform() => transform;
        public Rigidbody2D GetRigidBody() => rigidbody;
        public Animator GetAnimator() => animator;
        public Animator GetGunAnimator() => gunAnimator;

        public Camera GetCamera() => _camera;
        public Transform GetAimObject() => aimObject;
        public Transform GetFirePoint() => firePoint;
        public LineRenderer GetLineRenderer() => lineRenderer;
        public GameObject GetLine() => line;
        public LayerMask GetEnemyLayer() => enemyLayer;

        public Transform GetHealthBar() => healthBar;
        public Slider GetHealthSlider() => healthSlider;
    }
}