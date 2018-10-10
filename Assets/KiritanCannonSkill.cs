using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiritanCannonSkill : VoxeroidSkill
{
    public override List<VoxeroidController> ExecuteSkill(VoxeroidController voxeroid)
    {
        return FindNextVoxeroid(voxeroid);
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
