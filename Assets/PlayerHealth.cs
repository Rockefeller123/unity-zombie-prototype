using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private bool isDead = false;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
{
    isDead = true;
    Debug.Log("PLAYER DIED");

    // Stop time (simple game over)
    Time.timeScale = 0f;

    // Optional later:
    // disable movement
    // show UI
    // restart button
}



}

