using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractSupportSkill : VoxeroidSkill
{
    public GameObject particlePrefab;

    public override void ExecuteSkill(VoxeroidController supporter)
    {
        VoxeroidController performer = FindNextVoxeroid(supporter).FirstOrDefault();

        if(performer == null)
        {
            Debug.Log("Unexpected Situation");
            return;
        }

        var particle = Instantiate(particlePrefab) as GameObject;
        particle.transform.position = supporter.transform.position;
        Destroy(particle, 5f);

        PerformAnimation();

        StartCoroutine(DelayExecuteSkill(performer, supporter, 0.5f));
    }

    protected override List<VoxeroidController> FindNextVoxeroid(VoxeroidController voxeroid)
    {
        List<VoxeroidController> list = new List<VoxeroidController>();
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(voxeroid.transform.position, Vector3.up, out hit, 1f, LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Player)))
        {
            VoxeroidController target = hit.collider.gameObject.transform.root.GetComponent<VoxeroidController>();
            if (target != null)
            {
                list.Add(target);
            }
        }
        return list;
    }

    protected abstract void PerformAnimation();

}
