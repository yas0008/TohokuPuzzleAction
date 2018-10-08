using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBehaviour : MonoBehaviour
{
    public TerrainType type;

    public enum TerrainType
    {
        Playable,
        Placeble,
    }

}
