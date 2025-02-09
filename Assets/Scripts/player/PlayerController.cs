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

        public PlayerController(PlayerView playerView)
        {
            this.playerView = playerView;
            playerModel = new PlayerModel();
        }
/*
        public void Start()
        {
            SetMaxHealth(maxHealth);
            blood.GetComponent<ParticleSystem>().startColor = Color.red;
            playerScale = transform.localScale;
        }

        public void Update()
        {


            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement = movement.normalized;
            float speed = movement.magnitude;
            
            playerView..SetFloat("speed", speed);
            //shooting

            HandleAming();
            StartCoroutine(HandleShooting());

        }

        public void FixedUpdate()
        {
            Vector2 targetVelocity = movement * moveSpeed;

            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, 0.25f);
        }

        #region health
        public void LateUpdate()
        {
            healthBar.position = rb.position;
        }

        public void TakeDamage(int health)
        {
            currentHealth -= health;
            healthSlider.value = currentHealth;
            if (currentHealth <= 0)
            {
                // GameOver();
            }
            //Instantiate(blood, new Vector3(transform.position.x, transform.position.y, -4f), blood.transform.rotation);
        }

        public void SetMaxHealth(int health)
        {
            currentHealth = maxHealth;
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }

        public void Heal(int hp)
        {
            currentHealth += hp;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthSlider.value = currentHealth;
        }

        public void GameOver()
        {
            SoundManger.Instance.Play(Sounds.GameOver);
            animator.SetTrigger("death");
            //UIService.Instance.GameOver();
        }
        #endregion

        #region shooting

        private void HandleAming()
        {
            mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            Vector3 normalized = (mousePosition - aimObject.position).normalized;
            Vector3 lookDirection = normalized;
            angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            aimObject.eulerAngles = new Vector3(0, 0, angle);

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
            aimObject.localScale = aimScale;
            transform.localScale = playerScale;
        }

        private IEnumerator HandleShooting()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SoundManger.Instance.Play(Sounds.Shoot);

                line.SetActive(true);
                shootAnimator.SetTrigger("shoot");

                RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

                if (hitInfo.transform.CompareTag("enemy"))
                {
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                    enemy.TakeDamage();

                    lineRenderer.SetPosition(0, firePoint.position);
                    lineRenderer.SetPosition(1, hitInfo.point);
                }
                else
                {
                    lineRenderer.SetPosition(0, firePoint.position);
                    lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
                }
                lineRenderer.enabled = true;

                yield return new WaitForSeconds(0.02f);

                lineRenderer.enabled = false;

            }
        }
        #endregion
*/
    }
}
