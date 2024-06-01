using System.Collections;
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
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private GameObject line;

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
        StartCoroutine( HandleShooting());
    }
    void FixedUpdate()
    {
        
    }

    private void HandleAming()
    {
        mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 normalized = (mousePosition - aimObject.position).normalized;
        Vector3 lookDirection = normalized;
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

    private IEnumerator HandleShooting()
    {
        if(Input.GetMouseButtonDown(0))
        {
            line.SetActive(true);
            shootAnimator.SetTrigger("shoot");

            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

            if (hitInfo)
            {
                Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                enemy.TakeDamage();
                
               lineRenderer.SetPosition(0, firePoint.position);
               lineRenderer.SetPosition(1, hitInfo.point);
            }
            else
            {
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, firePoint.position + firePoint.right *100);
            }
            lineRenderer.enabled = true;

            yield return new WaitForSeconds(0.02f); 

            lineRenderer.enabled = false;
             
        }
    }
}