using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCursorBehaviour : MonoBehaviour
{
    [SerializeField] Vector3 initialPosition;
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

        voxeroid.transform.SetParent(null);
        FindObjectsOfType<VoxeroidController>()
            .ToList()
            .ForEach(v => v.SetVoxeroidActive(true));

        VoxeroidSkillManager.Instance.ResetChain();
        VoxeroidSkillManager.Instance.ExecuteSkill(voxeroid, null);

        StartCoroutine(DelayGenerateVoxeroid());

    }

    IEnumerator DelayGenerateVoxeroid()
    {
        yield return new WaitForSeconds(0.5f);
        GenerateVoxeroid();
    }

    void GenerateVoxeroid()
    {
        VoxeroidController newVoxeroid = PlayersManager.Instance.GenerateVoxeroid(VoxeroidController.VoxeroidType.Zunko);
        if (voxeroid != null)
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

    public void SwitchVoxeroid(int direction)
    {
        voxeroid.SwitchVoxeroid(direction);
    }

    Vector3 CalculatePosition(Vector3 position)
    {
        Ray ray = new Ray(position + Vector3.up * rayHeight, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Terrain)))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
