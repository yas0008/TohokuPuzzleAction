using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZundaMochiSkill : VoxeroidSkill
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject particlePrefab;

    public override IEnumerator DelayExecuteSkill(VoxeroidController performer)
    {
        yield return new WaitForSeconds(0.5f);
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = performer.transform.position + performer.GetForward();
        projectile.transform.rotation = performer.transform.rotation * Quaternion.AngleAxis(180, Vector3.up);
        projectile.GetComponent<ZundaMochi>().SetState(ZundaMochi.ZundaMochiState.Falling);
        Destroy(projectile, 5f);

        var particle = Instantiate(particlePrefab);
        particle.transform.position = performer.transform.position;
        Destroy(particle, 5f);
    }

    public override List<VoxeroidController> ExecuteSkill(VoxeroidController performer)
    {
        StartCoroutine(DelayExecuteSkill(performer));

        var list = FindNextVoxeroid(performer);
        list.ForEach(v =>
        {
            v.SetColliderActive(false);
            StartCoroutine(DelayExecuteNextSkill(v, null, 1.5f));
        });

        return list;
    }

    protected override List<VoxeroidController> FindNextVoxeroid(VoxeroidController voxeroid)
    {
        List<VoxeroidController> list = new List<VoxeroidController>();
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(voxeroid.transform.position + voxeroid.GetForward() + Vector3.up, Vector3.down, out hit, Mathf.Infinity, layerMask))
        {
            VoxeroidController target = hit.collider.gameObject.transform.root.GetComponent<VoxeroidController>();
            if (target != null)
            {
                list.Add(target);
            }
        }
        return list;
    }

}
