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
    }

    new void Awake()
    {
        base.Awake();
    }

    public LayerMask GetLayerMask(LayerName name)
    {
        return 1 << Enumerable.Range(0, max)
            .Select(i => LayerMask.LayerToName(i))
            .Where(s => string.Compare(s, name.ToString(), true) == 0)
            .Select(s => LayerMask.NameToLayer(s))
            .FirstOrDefault();
    }

}
