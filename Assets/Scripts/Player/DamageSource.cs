using UnityEngine;

public class DamageSource : MonoBehaviour
{
    // * Parameters
    [SerializeField] int damageAmount = 1;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemyAi>()) {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeInfoDamage(damageAmount);
        }
    }
}
