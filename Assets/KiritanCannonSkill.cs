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
        var positions = new List<Vector3>();
        positions.Add(voxeroid.GetCenter() + voxeroid.GetForward() + Vector3.down * 0.5f + voxeroid.transform.right);
        positions.Add(voxeroid.GetCenter() + voxeroid.GetForward() + Vector3.down * 0.5f - voxeroid.transform.right);

        positions.ForEach(p =>
        {
            Destroy(Instantiate(particlePrefab, p, Quaternion.identity).gameObject, 5f);
            Destroy(projectilePrefab.CloneProjectile(voxeroid, p + Vector3.up * 0.5f, Quaternion.identity).gameObject, 5f);
        });

    }

    public override List<VoxeroidController> ExecuteSkill(VoxeroidController voxeroid)
    {
        StartCoroutine(DelayExecuteSkill(voxeroid));

        var targets = FindNextVoxeroid(voxeroid);

        targets.ForEach(p =>
        {
            StartCoroutine(DelayExecuteNextSkill(p, null, 1.5f));
        });

        return targets;
    }

    protected override List<VoxeroidController> FindNextVoxeroid(VoxeroidController voxeroid)
    {
        var list = new List<VoxeroidController>();

        var positions = new List<Vector3>();
        positions.Add(voxeroid.GetCenter() + voxeroid.GetForward() + Vector3.up + voxeroid.transform.right);
        positions.Add(voxeroid.GetCenter() + voxeroid.GetForward() + Vector3.up - voxeroid.transform.right);

        positions.ForEach(p =>
        {
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(p, Vector3.down, out hit, 0.6f, LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Player)))
            {
                VoxeroidController target = hit.collider.gameObject.transform.root.GetComponent<VoxeroidController>();
                if (target != null)
                {
                    list.Add(target);
                }
            }
        });

        return list;
    }

}
