using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    // * Parameters
    [SerializeField] Material whiteFlashMat;
    [SerializeField] float restoreDefaultMatTime = 0.2f;

    // * Refs
    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;

    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }

    public IEnumerator FlashRoutine () {
        spriteRenderer.material = whiteFlashMat;
        yield return new WaitForSeconds(restoreDefaultMatTime);
        spriteRenderer.material = defaultMat;
        enemyHealth.DetectDeath();
    }
}
