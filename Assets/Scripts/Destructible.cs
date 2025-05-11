using UnityEngine;

public class Destructible : MonoBehaviour
{
    // * Parameters
    [SerializeField] GameObject destroyVFX;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<DamageSource>()) {
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
