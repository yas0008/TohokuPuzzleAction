using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayersManager : SingletonMonoBehaviour<PlayersManager>
{
    //[SerializeField] float gravity;

    new void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        //if (voxeroid == null) return;

        //if (PlayerStateManager.Instance.state.Equals(PlayerStateManager.PlayerState.Air))
        //{
        //    voxeroid.transform.position += Vector3.down * gravity * Time.deltaTime;
        //}
    }

    public VoxeroidController GenerateVoxeroid(VoxeroidController.VoxeroidType type)
    {
        GameObject obj = Instantiate(Resources.Load("Prefab/Unit/Unit")) as GameObject;
        VoxeroidController voxeroid = obj.GetComponent<VoxeroidController>();
        voxeroid.SetVoxeroid(type);
        return voxeroid;
    }

}