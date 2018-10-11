using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractSupportSkill : VoxeroidSkill
{
    public GameObject particlePrefab;

    public override IEnumerator DelayExecuteSkill(VoxeroidController voxeroid)
    {
        yield return new WaitForSeconds(0.5f);
        var particle = Instantiate(particlePrefab) as GameObject;
        particle.transform.position = voxeroid.transform.position;
        Destroy(particle, 5f);
    }

    public override List<VoxeroidController> ExecuteSkill(VoxeroidController supporter)
    {
        var list = FindNextVoxeroid(supporter);
        VoxeroidController performer = list.FirstOrDefault();

        if(performer == null)
        {
            Debug.Log("Unexpected Situation");
            return list;
        }

        PerformAnimation();
        StartCoroutine(DelayExecuteNextSkill(performer, supporter, 1f));

        return list;
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
