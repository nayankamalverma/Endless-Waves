using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Transform aimObject;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Animator shootAnimator;

    private Vector3 mousePosition;
    float angle;
    Vector3 playerScale;
    void Start()
    {
        playerScale = transform.localScale;
    }

    void Update()
    {
        HandleAming();
        HandleShooting();
    }
    void FixedUpdate()
    {
        
    }

    private void HandleAming()
    {
        mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
       
        Vector3 lookDirection = (mousePosition - transform.position).normalized;
        angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        aimObject.eulerAngles = new Vector3(0, 0, angle);

        Vector3 aimScale = Vector3.one;
        if(angle >90 || angle < -90)
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
        aimObject.localScale = aimScale;
        transform.localScale = playerScale;
    }

    private void HandleShooting()
    {
        if(Input.GetMouseButtonDown(0))
        {
            shootAnimator.SetTrigger("shoot");
        }
    }
}