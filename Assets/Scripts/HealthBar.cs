using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Health playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.Find("BodyCollider").GetComponent<Health>();
        healthBar.maxValue = playerHealth.maxHealth;
    }

    private void Update()
    {
        healthBar.value = playerHealth.currentHealth;
    }
}