using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float moveSpeed=10;
    [SerializeField]
    private Animator animator;

    private Vector2 movement;
    
    void Start()
    {
    }
     
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
        float speed = movement.magnitude;
        if(animator != null )
        {
            animator.SetFloat("speed", speed);
        }
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = movement * moveSpeed;

        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, 0.25f);
    }

}
