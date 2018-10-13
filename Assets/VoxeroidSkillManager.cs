using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class VoxeroidSkillManager : SingletonMonoBehaviour<VoxeroidSkillManager>
{
    [SerializeField] List<VoxeroidSkill> downSkills;
    [SerializeField] List<VoxeroidSkill> supportSkills;
    [SerializeField] List<VoxeroidSkill> upSkills;

    int chain = 0;

    new void Awake()
    {
        base.Awake();
    }

    public void ExecuteSkill(VoxeroidController performer, VoxeroidController supporter)
    {
        if (chain == 0 && !IsOnPlayable(performer))
        {
            return;
        }

        LevelObjectManager.Instance.LevelStateManager.State = LevelStateManager.LevelState.Unit;
        chain++;
        performer.TweenModels();

        var list = new List<VoxeroidController>();
        VoxeroidController up = FindUpVoxeroid(performer);
        if (up != null)
        {
            list = supportSkills[(int)performer.type].ExecuteSkill(performer);
        }
        else if (supporter != null)
        {
            list = upSkills[(int)performer.type].ExecuteSkill(performer);
            performer.SetVoxeroidActive(false);
        }
        else
        {
            performer.SetVoxeroidActive(false);
            list = downSkills[(int)performer.type].ExecuteSkill(performer);
        }

        if (list.Count == 0)
        {
            LevelObjectManager.Instance.LevelStateManager.State = LevelStateManager.LevelState.Cursor;
        }
    }

    bool IsOnPlayable(VoxeroidController performer)
    {
        Ray ray = new Ray(performer.GetCenter(), Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Player)))
        {
            return true;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Terrain)))
        {
            TerrainBehaviour terrain = hit.collider.gameObject.GetComponentInParent<TerrainBehaviour>();
            if (terrain != null)
            {
                if (terrain.type.Equals(TerrainBehaviour.TerrainType.Playable))
                {
                    return true;
                }
            }
        }
        return false;
    }

    VoxeroidController FindUpVoxeroid(VoxeroidController voxeroid)
    {
        Ray ray = new Ray(voxeroid.GetCenter(), Vector3.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1f, LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Player)))
        {
            VoxeroidController target = hit.collider.gameObject.GetComponentInParent<VoxeroidController>();
            if (target != null)
            {
                return target;
            }
        }
        return null;
    }

    public void ResetChain()
    {
        chain = 0;
    }

}
