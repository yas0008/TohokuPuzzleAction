using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KiritanDownCannonSkill : VoxeroidSkill
{
    [SerializeField] ParticleSystem particlePrefab;
    [SerializeField] Projectile projectilePrefab;

    public override IEnumerator DelayExecuteSkill(VoxeroidController voxeroid)
    {
        yield return new WaitForSeconds(1.2f);

        var positions = new List<Vector3>();
        positions.Add(voxeroid.GetCenter() + voxeroid.transform.right);
        positions.Add(voxeroid.GetCenter() - voxeroid.transform.right);

        RaycastHit hit = new RaycastHit();
        positions.ToList().ForEach(p =>
        {
            if (Physics.Raycast(p, Vector3.down, out hit, Mathf.Infinity, LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Terrain)))
            {
                Destroy(Instantiate(particlePrefab, hit.point, Quaternion.identity).gameObject, 5f);
                Destroy(projectilePrefab.CloneProjectile(voxeroid, hit.point + Vector3.up * 0.5f, Quaternion.identity).gameObject);
            }
        });
    }

    public override List<VoxeroidController> ExecuteSkill(VoxeroidController voxeroid)
    {
        StartCoroutine(DelayExecuteSkill(voxeroid));

        var targets = FindNextVoxeroid(voxeroid);

        targets.ForEach(p => StartCoroutine(DelayExecuteNextSkill(p, null, 1.5f)));

        return targets;
    }

    protected override List<VoxeroidController> FindNextVoxeroid(VoxeroidController voxeroid)
    {
        var list = new List<VoxeroidController>();

        var positions = new List<Vector3>();
        positions.Add(voxeroid.GetCenter() + Vector3.down * 100 + voxeroid.transform.right);
        positions.Add(voxeroid.GetCenter() + Vector3.down * 100 + voxeroid.transform.right * -1);

        positions.ForEach(p =>
        {
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(p, Vector3.up, out hit, Mathf.Infinity, LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Player)))
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
