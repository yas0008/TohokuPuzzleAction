using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZundaArrowSkill : VoxeroidSkill
{
    [SerializeField] float speed;
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] GameObject particlePrefab;

    LayerMask mask;

    private void Start()
    {
        mask = LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Player) | LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Terrain);
    }

    public override IEnumerator DelayExecuteSkill(VoxeroidController performer)
    {
        yield return new WaitForSeconds(0.5f);

        var projectile = projectilePrefab.CloneProjectile(performer, performer.GetCenter(), performer.transform.rotation * Quaternion.AngleAxis(180, Vector3.up));
        Destroy(projectile.gameObject, 5f);

        var particle = Instantiate(particlePrefab, performer.GetCenter(), Quaternion.identity);
        Destroy(particle.gameObject, 5f);
    }

    public override List<VoxeroidController> ExecuteSkill(VoxeroidController performer)
    {
        StartCoroutine(DelayExecuteSkill(performer));

        var list = FindNextVoxeroid(performer);
        list.ForEach(p =>
        {
            StartCoroutine(DelayExecuteNextSkill(p, null, 1.5f));
        });

        //performer.GetAnimator(VoxeroidController.VoxeroidType.Zunko).Play("Attack", 0, 0);

        return list;
    }

    protected override List<VoxeroidController> FindNextVoxeroid(VoxeroidController voxeroid)
    {
        List<VoxeroidController> list = new List<VoxeroidController>();

        RaycastHit hit;
        Ray ray = new Ray(voxeroid.GetCenter() + voxeroid.GetForward() * 0.5f, voxeroid.GetForward());

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            int layer = hit.collider.gameObject.layer;
            if (layer == LayerMaskUtility.Instance.GetLayerNumber(LayerMaskUtility.LayerName.Terrain))
            {
                //NothingToDo
            }
            else if(layer == LayerMaskUtility.Instance.GetLayerNumber(LayerMaskUtility.LayerName.Player))
            {
                list.Add(hit.collider.GetComponentInParent<VoxeroidController>());
            }
        }
        return list;
    }

}
