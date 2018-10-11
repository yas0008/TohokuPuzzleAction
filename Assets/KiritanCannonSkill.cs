using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiritanCannonSkill : VoxeroidSkill
{
    [SerializeField] ParticleSystem particlePrefab;
    [SerializeField] Projectile projectilePrefab;

    public override IEnumerator DelayExecuteSkill(VoxeroidController voxeroid)
    {
        yield return new WaitForSeconds(1.2f);
        var pos1 = voxeroid.transform.position + voxeroid.GetForward() + Vector3.down * 0.5f + voxeroid.transform.right;
        var pos2 = voxeroid.transform.position + voxeroid.GetForward() + Vector3.down * 0.5f + voxeroid.transform.right * -1;

        var particle1 = Instantiate(particlePrefab, pos1, Quaternion.identity);
        var particle2 = Instantiate(particlePrefab, pos2, Quaternion.identity);
        Destroy(particle1.gameObject, 5f);
        Destroy(particle2.gameObject, 5f);

        var projectile1 = projectilePrefab.CloneProjectile(voxeroid, pos1 + Vector3.up * 0.5f, Quaternion.identity);
        var projectile2 = projectilePrefab.CloneProjectile(voxeroid, pos2 + Vector3.up * 0.5f, Quaternion.identity);
        Destroy(projectile1, 5f);
        Destroy(projectile2, 5f);
    }

    public override List<VoxeroidController> ExecuteSkill(VoxeroidController voxeroid)
    {
        StartCoroutine(DelayExecuteSkill(voxeroid));

        var targets = FindNextVoxeroid(voxeroid);

        targets.ForEach(p =>
        {
            p.SetColliderActive(false);
            StartCoroutine(DelayExecuteNextSkill(p, null, 1.5f));
        });

        return targets;
    }

    protected override List<VoxeroidController> FindNextVoxeroid(VoxeroidController voxeroid)
    {
        var list = new List<VoxeroidController>();

        var positions = new List<Vector3>();
        positions.Add(voxeroid.transform.position + voxeroid.GetForward() + Vector3.up + voxeroid.transform.right);
        positions.Add(voxeroid.transform.position + voxeroid.GetForward() + Vector3.up + voxeroid.transform.right * -1);

        positions.ForEach(p =>
        {
            Debug.Log(p);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(p, Vector3.down, out hit, 0.6f, LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Player)))
            {
                VoxeroidController target = hit.collider.gameObject.transform.root.GetComponent<VoxeroidController>();
                if (target != null)
                {
                    Debug.Log(target.name);
                    list.Add(target);
                }
            }
        });

        return list;
    }

}
