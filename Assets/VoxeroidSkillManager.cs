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
        GiveTween(performer.gameObject);

        var list = new List<VoxeroidController>();
        VoxeroidController up = FindUpVoxeroid(performer);
        if (up != null)
        {
            list = supportSkills[(int)performer.type].ExecuteSkill(performer);
        }
        else if (supporter != null)
        {
            list = upSkills[(int)performer.type].ExecuteSkill(performer);
        }
        else
        {
            list = downSkills[(int)performer.type].ExecuteSkill(performer);
        }

        if (list.Count == 0)
        {
            LevelObjectManager.Instance.LevelStateManager.State = LevelStateManager.LevelState.Cursor;
        }
    }

    bool IsOnPlayable(VoxeroidController performer)
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(performer.transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Player)))
        {
            return true;
        }

        if (Physics.Raycast(performer.transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Terrain)))
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

    public void ResetChain()
    {
        chain = 0;
    }

    void GiveTween(GameObject target)
    {
        Vector3 position = target.transform.position;
        target.transform
            .DOMove(Vector3.up * 0.5f, 0.05f, false)
            .SetRelative()
            .SetEase(Ease.Linear)
            .SetLoops(4, LoopType.Yoyo)
            .OnComplete(() => target.transform.position = position);
    }

}
