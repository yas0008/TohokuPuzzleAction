using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectManager : SingletonMonoBehaviour<LevelObjectManager>
{
    private LevelStateManager levelStateManager;
    public LevelStateManager LevelStateManager
    {
        get
        {
            if (levelStateManager == null)
                levelStateManager = FindObjectOfType<LevelStateManager>();
            return levelStateManager;
        }
        private set { }
    }

    private PlayerCursorBehaviour cursor;
    public PlayerCursorBehaviour Cursor
    {
        get
        {
            if (cursor == null)
                cursor = FindObjectOfType<PlayerCursorBehaviour>();
            return cursor;
        }
        private set { }
    }
}
