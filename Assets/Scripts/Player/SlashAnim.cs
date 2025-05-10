using UnityEngine;

public class SlashAnim : MonoBehaviour
{
    // * Refs
    private ParticleSystem ps;
    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if(ps && !ps.IsAlive()) {
            DistroySelf();
        }
    }
    public void DistroySelf () {
        Destroy(gameObject);
    }
}
