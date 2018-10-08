using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoxeroidSkillManager : SingletonMonoBehaviour<VoxeroidSkillManager>
{
    [SerializeField] List<VoxeroidSkill> downSkills;
    [SerializeField] List<VoxeroidSkill> supportSkills;
    [SerializeField] List<VoxeroidSkill> upSkills;

    new void Awake()
    {
        base.Awake();
    }

    public void ExecuteSkill(VoxeroidController performer, VoxeroidController supporter)
    {
        VoxeroidController up = FindUpVoxeroid(performer);
        if (up != null)
        {
            supportSkills[(int)performer.type].ExecuteSkill(performer);
        }
        else if(supporter != null)
        {
            upSkills[(int)performer.type].ExecuteSkill(performer);
        }
        else
        {
            downSkills[(int)performer.type].ExecuteSkill(performer);
        }
    }

    VoxeroidController FindUpVoxeroid(VoxeroidController voxeroid)
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(voxeroid.transform.position, Vector3.up, out hit, 1f, LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Player)))
        {
            VoxeroidController target = hit.collider.gameObject.GetComponentInParent<VoxeroidController>();
            if (target != null)
            {
                return target;
            }
        }
        return null;
    }

}
