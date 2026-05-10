using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;

    [Header("Death Settings")]
    [SerializeField] private bool restartLevelOnDeath = true;
    [SerializeField] private float restartDelay = 1f;

    [Header("Debug")]
    [SerializeField] private bool enableDebugKeys = true;
    [SerializeField] private KeyCode damageTestKey = KeyCode.H;
    [SerializeField] private KeyCode healTestKey = KeyCode.J;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => currentHealth <= 0;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (!enableDebugKeys)
            return;

        if (Input.GetKeyDown(damageTestKey))
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(healTestKey))
        {
            Heal(1);
        }
    }

    public void TakeDamage(int amount)
    {
        if (IsDead)
            return;

        if (amount <= 0)
        {
            Debug.LogWarning("Damage amount must be greater than 0.");
            return;
        }

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"Player took {amount} damage. Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (IsDead)
            return;

        if (amount <= 0)
        {
            Debug.LogWarning("Heal amount must be greater than 0.");
            return;
        }

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"Player healed {amount}. Health: {currentHealth}/{maxHealth}");
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        Debug.Log($"Player health reset: {currentHealth}/{maxHealth}");
    }

    private void Die()
    {
        Debug.Log("Player died. Restarting level...");

        if (restartLevelOnDeath)
        {
            Invoke(nameof(RestartCurrentLevel), restartDelay);
        }
    }

    private void RestartCurrentLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
