using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour
{
    public ParticleSystem successParticle;
    public ParticleSystem failedParticle;
    VoxeroidController shooter;

    [SerializeField] float forwardSpeed;
    [SerializeField] float upSpeed;
    [SerializeField] float rightSpeed;

    void Update()
    {
        transform.position += (transform.forward * forwardSpeed + transform.up * upSpeed + transform.right * rightSpeed) * Time.deltaTime;
    }

    public Projectile CloneProjectile(VoxeroidController shooter, Vector3 position, Quaternion quaternion)
    {
        Projectile projectile = Instantiate(gameObject, position, quaternion).GetComponent<Projectile>();
        projectile.shooter = shooter;
        return projectile;
    }

    protected void OnTriggerEnter(Collider other)
    {
        var voxeroid = other.gameObject.transform.root.GetComponent<VoxeroidController>();
        var target = other.gameObject.transform.root.GetComponent<Target>();

        if (voxeroid != null)
        {
            if (shooter.GetInstanceID() != voxeroid.GetInstanceID())
            {
                SuccessDestroy();
            }
            else
            {
                return;
            }
        }
        else if (target != null)
        {
            SuccessDestroy();
        }
        else
        {
            FailDestroy();
        }

        Destroy(gameObject);
    }

    void SuccessDestroy()
    {
        ParticleSystem success = Instantiate(successParticle, transform.position, Quaternion.identity);
        success.Play();
        Destroy(success.gameObject, 5f);
    }

    void FailDestroy()
    {
        ParticleSystem failed = Instantiate(failedParticle, transform.position, Quaternion.identity);
        failed.Play();
        Destroy(failed.gameObject, 5f);
    }

}