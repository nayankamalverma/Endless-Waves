using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Transform healthBar;
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject blood;

    private int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        SetMaxHealth(maxHealth);
        blood.GetComponent<ParticleSystem>().startColor = Color.red;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamge(20);
        }
    }
    private void LateUpdate()
    {
        healthBar.position = transform.position;
    }
    public void TakeDamge(int health)
    {
        currentHealth -= health;
        healthSlider.value = currentHealth;
        if(currentHealth <= 0) {
            GameOver();
        }
        Instantiate(blood, new Vector3(transform.position.x, transform.position.y, -4f), blood.transform.rotation);
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
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthSlider.value = currentHealth;
    }

    public void GameOver()
    {
        animator.SetTrigger("death");
        GameManager.Instance.GameOver();
    }
}
