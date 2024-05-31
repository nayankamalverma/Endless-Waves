using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform player; // Reference to the player's transform
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField]
    private float speed = 2.0f; // Speed at which the enemy moves
    [SerializeField]
    private int enemyHeath;
    [SerializeField]
    private int damage=10;


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
            // Perform attack logic here
            Debug.Log("Enemy attacks!");
            playerHealth.TakeDamge(damage);

 //           PlayerHealth playerHealth = GameManager.Instance.GetPlayerHealth();
//if (playerHealth != null)
 //           {
//playerHealth.TakeDamge(damage);
//            }

            // Update the last attack time
            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage()
    {
        enemyHeath -= 1;
        if (enemyHeath <= 0) { 
            Destroy(gameObject);
        }
    }
}