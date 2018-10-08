using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : AbstractInputManager
{
    void Start()
    {
        onPressUpArrow += () => PlayerCursorBehaviour.Instance.Move(Vector3.forward);
        onPressDownArrow += () => PlayerCursorBehaviour.Instance.Move(Vector3.back);
        onPressLeftArrow += () => PlayerCursorBehaviour.Instance.Move(Vector3.left);
        onPressRightArrow += () => PlayerCursorBehaviour.Instance.Move(Vector3.right);

        onPressZ += () => PlayerCursorBehaviour.Instance.SwitchVoxeroid();
        onPressX += () => PlayerCursorBehaviour.Instance.RotateVoxeroid();
        onPressSpace += () => PlayerCursorBehaviour.Instance.ReleaseVoxeroid();
    }

    new void Update()
    {
        base.Update();
    }
}
