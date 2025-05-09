using UnityEditor;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // * Parameters
    [SerializeField] int startingHealth = 3;

    // * States
    private int currentHealth;

    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeInfoDamage(int damage) {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        DetectDeath();
    }
    void DetectDeath() {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

}
