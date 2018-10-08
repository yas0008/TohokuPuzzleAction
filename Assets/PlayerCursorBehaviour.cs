using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCursorBehaviour : MonoBehaviour
{
    [SerializeField] Vector3 initialPosition;
    [SerializeField] float spawnHeight;
    [SerializeField] float rayHeight;
    [SerializeField] LayerMask terrainMask;
    [SerializeField] LayerMask playerMask;

    VoxeroidController voxeroid;

    void Start()
    {
        transform.position = CalculatePosition(initialPosition);
        GenerateVoxeroid();
    }

    public void ReleaseVoxeroid()
    {
        if (!LevelObjectManager.Instance.LevelStateManager.State.Equals(LevelStateManager.LevelState.Cursor))
            return;

        VoxeroidSkillManager.Instance.ResetChain();
        VoxeroidSkillManager.Instance.ExecuteSkill(voxeroid, null);
        voxeroid.transform.SetParent(null);
        transform.position = CalculatePosition(transform.position);
        FindObjectsOfType<VoxeroidController>()
            .ToList()
            .ForEach(v => v.SetColliderActive(true));
        GenerateVoxeroid();
    }

    void GenerateVoxeroid()
    {
        VoxeroidController newVoxeroid = PlayersManager.Instance.GenerateVoxeroid(VoxeroidController.VoxeroidType.Zunko);
        if(voxeroid != null)
        {
            newVoxeroid.transform.root.transform.rotation = voxeroid.transform.root.transform.rotation;
        }
        voxeroid = newVoxeroid;
        voxeroid.transform.SetParent(transform);
        voxeroid.transform.localPosition = Vector3.zero;
    }

    public void RotateVoxeroid()
    {
        voxeroid.RotateCharacter();
    }

    public void SwitchVoxeroid()
    {
        voxeroid.SwitchVoxeroid();
    }

    Vector3 CalculatePosition(Vector3 position)
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(position + Vector3.up * rayHeight, Vector3.down, out hit, Mathf.Infinity, LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Terrain) | LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.CursorHitBox)))
        {
            return hit.point + Vector3.up * spawnHeight;
        }
        return Vector3.zero;
    }

    bool IsMoveble(Vector3 position)
    {
        return Physics.Raycast(position + Vector3.up * rayHeight, Vector3.down, Mathf.Infinity, LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Terrain) | LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.CursorHitBox));
    }

    public void Move(Vector3 movement)
    {
        if (IsMoveble(transform.position + movement))
        {
            Vector3 toMove = transform.position + movement;
            toMove = new Vector3(Mathf.RoundToInt(toMove.x), Mathf.RoundToInt(toMove.y), Mathf.RoundToInt(toMove.z));
            transform.position = CalculatePosition(toMove);
        }
    }
}
