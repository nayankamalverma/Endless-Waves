using Assets.Scripts.player;
using Assets.Scripts.UI.ScriptableObjects;
using Assets.Scripts.Utilities.VFX;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private Transform playerTransform;
        private EnemyScriptableObjects enemySO;
        private EnemyService enemyService;

        private Vector3 velocity = Vector3.zero;
        private float smoothTime = 0.3f;
        private Vector3 scale;
        private float lastAttackTime;
        private int health;

        public void SetReferences(EnemyService enemyService, EnemyScriptableObjects enemySO, Transform playerTransform)
        {
            this.enemyService = enemyService;
            this.enemySO = enemySO;
            this.playerTransform = playerTransform;
        }

        public void ConfigureEnemy(Vector2 position)
        {
            transform.position = position;
            health = enemySO.enemyHeath;
        }

        public EnemyType GetEnemyType() => enemySO.enemyType;

        private void Start()
        {
            scale = transform.localScale;
            lastAttackTime = -enemySO.attackCooldown; // Ensure the enemy can attack immediately
        }

        private void Update()
        {
            Vector3 direction = playerTransform.position - transform.position;
            float distance = direction.magnitude;

            if (distance > enemySO.stoppingDistance)
            {
                MoveTowardsPlayer(direction);
                UpdateFacingDirection();
            }
        }

        private void MoveTowardsPlayer(Vector3 direction)
        {
            Vector3 targetPosition = playerTransform.position - direction.normalized * enemySO.stoppingDistance;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, enemySO.speed);
        }

        private void UpdateFacingDirection()
        {
            if (velocity.x > 0)
            {
                scale.x = Mathf.Abs(scale.x); // Face right
            }
            else if (velocity.x < 0)
            {
                scale.x = -Mathf.Abs(scale.x); // Face left
            }
            transform.localScale = scale;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Player") && Time.time >= lastAttackTime + enemySO.attackCooldown)
            {
                Attack();
            }
        }

        private void Attack()
        {
            lastAttackTime = Time.time;
            enemyService.eventService.OnPlayerHurt.Invoke(enemySO.damage);
        }

        public void TakeDamage()
        {
            VFXService.Instance.PlayVFX(VFXType.Blood, new Vector3(transform.position.x, transform.position.y, -4f));
            health--;
            if (health <= 0)
            {
                enemyService.ReturnEnemy(this);
            }
        }
    }
}