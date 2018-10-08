using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZundaArrowSkill : VoxeroidSkill
{
    [SerializeField] float speed;
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] GameObject particlePrefab;

    public override List<VoxeroidController> ExecuteSkill(VoxeroidController performer)
    {
        var projectile = projectilePrefab.CloneProjectile(performer, performer.transform.position, performer.transform.rotation * Quaternion.AngleAxis(180, Vector3.up));
        Destroy(projectile, 5f);

        var particle = Instantiate(particlePrefab, performer.transform.position, Quaternion.identity);
        Destroy(particle, 5f);

        var list = FindNextVoxeroid(performer);
        list.ForEach(p =>
        {
            p.SetColliderActive(false);
            StartCoroutine(DelayExecuteSkill(p, null, 1f));
        });

        performer.GetAnimator(VoxeroidController.VoxeroidType.Zunko).Play("Attack", 0, 0);

        return list;
    }

    protected override List<VoxeroidController> FindNextVoxeroid(VoxeroidController voxeroid)
    {
        List<VoxeroidController> list = new List<VoxeroidController>();
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(voxeroid.transform.position, voxeroid.transform.forward * -1, out hit, Mathf.Infinity, LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Player)))
        {
            VoxeroidController target = hit.collider.gameObject.transform.root.GetComponent<VoxeroidController>();
            if(target != null)
            {
                list.Add(target);
            } 
        }
        return list;
    }
}
