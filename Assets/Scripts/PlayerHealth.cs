using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Transform healthBar;
    [SerializeField]
    private Slider healthSlider;

    private int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        SetMaxHealth(maxHealth);
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

    }
}
