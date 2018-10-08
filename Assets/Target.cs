using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] ParticleSystem targetBreakParticle;

    void OnTriggerEnter(Collider other)
    {
        var particle = Instantiate(targetBreakParticle, transform.position, Quaternion.identity);
        particle.Play();
        Destroy(particle.gameObject, 5f);
        Destroy(gameObject);
    }

}
