using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayersManager : SingletonMonoBehaviour<PlayersManager>
{
    [SerializeField] VoxeroidController unit;

    new void Awake()
    {
        base.Awake();
    }

    public VoxeroidController GenerateVoxeroid(VoxeroidController.VoxeroidType type)
    {
        VoxeroidController voxeroid = Instantiate(unit);
        voxeroid.SetVoxeroid(type);
        return voxeroid;
    }

}