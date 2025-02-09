using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.player
{
	public class PlayerView : MonoBehaviour
    {
        public Rigidbody2D rigidbody { private get; set; }
        public Animator animator { private get; set; }
        public Animator gunAnimator { private get; set; }

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

        //health
        [Header("Health")]
        [SerializeField]
        private Transform healthBar;
        [SerializeField]
        private Slider healthSlider;
        [SerializeField]
        private ParticleSystem bloodParticle;

        private PlayerController playerController;

        public void SetServices(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        private void Update()
        {
            //playerController.Update();
        }

        private void FixedUpdate()
        {
           // playerController.FixedUpdate();
        }

    }
}