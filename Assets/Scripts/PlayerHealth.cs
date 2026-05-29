using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private bool canTakeDamage = true;
    public float damageCooldown = 1f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int dmg)
    {
        if (!canTakeDamage) return;

        currentHealth -= dmg;
        canTakeDamage = false;

        Debug.Log("HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

        Invoke(nameof(ResetDamage), damageCooldown);
    }

    void ResetDamage()
    {
        canTakeDamage = true;
    }

    void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}