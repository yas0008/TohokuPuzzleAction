using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateManager : MonoBehaviour
{
    public enum LevelState
    {
        Cursor,
        Unit
    }
    
    public LevelState State { get; set; }

}
