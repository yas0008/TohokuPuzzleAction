using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZundaMochiSkill : VoxeroidSkill
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject particlePrefab;

    public override List<VoxeroidController> ExecuteSkill(VoxeroidController performer)
    {
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = performer.transform.position + performer.GetForward();
        projectile.transform.rotation = performer.transform.rotation * Quaternion.AngleAxis(180, Vector3.up);
        projectile.GetComponent<ZundaMochi>().SetState(ZundaMochi.ZundaMochiState.Falling);
        Destroy(projectile, 5f);

        var particle = Instantiate(particlePrefab);
        particle.transform.position = performer.transform.position;
        Destroy(particle, 5f);

        var list = FindNextVoxeroid(performer);
        list.ForEach(v =>
        {
            v.SetColliderActive(false);
            StartCoroutine(DelayExecuteSkill(v, null, 1f));
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
