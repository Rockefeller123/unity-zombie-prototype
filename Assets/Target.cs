using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;
    private Renderer rend;
    private Color originalColor;

    private void Awake()
    {
        currentHealth = maxHealth;

        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            originalColor = rend.material.color;
        }
    }

    public void OnHit()
    {
        currentHealth--;

        if (rend != null)
        {
            rend.material.color = Color.red;
            Invoke(nameof(ResetColor), 0.1f);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void ResetColor()
    {
        if (rend != null)
        {
            rend.material.color = originalColor;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}



