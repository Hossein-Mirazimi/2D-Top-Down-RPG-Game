using System.Collections;
using UnityEditor;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // * Parameters
    [SerializeField] int startingHealth = 3;
    [SerializeField] GameObject deathVFXPrefab;
    [SerializeField] float knockBackThrust = 15f;
    // * Refs;
    private Knockback knockback;
    private Flash flash;

    // * States
    private int currentHealth;

    void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }
    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeInfoDamage(int damage) {
        currentHealth -= damage;
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    IEnumerator CheckDetectDeathRoutine () {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }
    public void DetectDeath() {
        if (currentHealth <= 0) {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
