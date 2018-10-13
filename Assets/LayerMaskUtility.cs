using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayerMaskUtility : SingletonMonoBehaviour<LayerMaskUtility>
{
    int max = 32;

    public enum LayerName
    {
        Player,
        Terrain,
        Target,
        CursorHitBox,
    }

    List<LayerMask> layerMasks;
    List<int> layers;

    new void Awake()
    {
        base.Awake();
        Initialize();
    }

    public LayerMask GetLayerMask(LayerName name)
    {
        return layerMasks[(int) name];
    }

    public int GetLayerNumber(LayerName name)
    {
        return layers[(int) name];
    }

    LayerMask FindLayerMask(LayerName name)
    {
        return 1 << Enumerable.Range(0, max)
            .Select(i => LayerMask.LayerToName(i))
            .Where(s => string.Compare(s, name.ToString(), true) == 0)
            .Select(s => LayerMask.NameToLayer(s))
            .FirstOrDefault();
    }

    int FindLayerNumber(LayerName name)
    {
        return Enumerable.Range(0, max)
            .Select(i => LayerMask.LayerToName(i))
            .Where(s => string.Compare(s, name.ToString(), true) == 0)
            .Select(s => LayerMask.NameToLayer(s))
            .FirstOrDefault();
    }

    void Initialize()
    {
        layerMasks = new List<LayerMask>();
        layers = new List<int>();
        Enum.GetValues(typeof(LayerName)).Cast<LayerName>()
            .ToList()
            .ForEach(n =>
            {
                layerMasks.Add(FindLayerMask(n));
                layers.Add(FindLayerNumber(n));
            });
    }

}
