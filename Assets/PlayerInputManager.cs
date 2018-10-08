using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : AbstractInputManager
{
    void Start()
    {
        onPressUpArrow += () => LevelObjectManager.Instance.Cursor.Move(Vector3.forward);
        onPressDownArrow += () => LevelObjectManager.Instance.Cursor.Move(Vector3.back);
        onPressLeftArrow += () => LevelObjectManager.Instance.Cursor.Move(Vector3.left);
        onPressRightArrow += () => LevelObjectManager.Instance.Cursor.Move(Vector3.right);

        onPressZ += () => LevelObjectManager.Instance.Cursor.SwitchVoxeroid();
        onPressX += () => LevelObjectManager.Instance.Cursor.RotateVoxeroid();
        onPressSpace += () => LevelObjectManager.Instance.Cursor.ReleaseVoxeroid();
    }

    new void Update()
    {
        base.Update();
    }
}
