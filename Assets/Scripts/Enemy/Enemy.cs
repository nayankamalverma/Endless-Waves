using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform player; // Reference to the player's transform
    [SerializeField]
    private float speed = 2.0f; // Speed at which the enemy moves
    [SerializeField]
    private int enemyHeath;
    [SerializeField]
    private int damage=10;
    [SerializeField]
    private GameObject blood;


    private float stoppingDistance = 1.0f; // Distance to stop from the player
    private float smoothTime = 0.3f; // Smooth time for the movement
    

    private Vector3 velocity = Vector3.zero;
    private Vector3 scale;
    private float attackCooldown = 1.0f;
    private float lastAttackTime;

    private void Start()
    {
        scale = transform.localScale;
        enemyHeath = 2;
        lastAttackTime = -attackCooldown; // Ensure the enemy can attack immediately
    }
    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player transform not assigned!");
            return;
        }

        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if (distance > stoppingDistance)
        {
            Vector3 targetPosition = player.position - direction.normalized * stoppingDistance;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, speed);

            if (velocity.x > 0)
            {
                scale.x = 0.4f;
            }
            else if (velocity.x < 0)
            {
                scale.x = -0.4f;
            }
            transform.localScale = scale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Attack();
        }
    }

    void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            //reduce player health
            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage()
    {
        Instantiate(blood,new Vector3(transform.position.x,transform.position.y, -4f), blood.transform.rotation);
        enemyHeath -= 1;
        if (enemyHeath <= 0) { 
            //UIService.Instance.UpdateScore();
            Destroy(gameObject);
        }
    }
}