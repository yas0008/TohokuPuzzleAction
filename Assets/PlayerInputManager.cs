using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : AbstractInputManager
{
    [SerializeField] PlayerCursorBehaviour cursor;
    [SerializeField] CameraController cameraController;
    RaycastHit hit;
    LayerMask mask1;
    LayerMask mask2;
    Vector3 snappedPosition;

    void Start()
    {
        //onPressUpArrow += () => LevelObjectManager.Instance.Cursor.Move(Vector3.forward);
        //onPressDownArrow += () => LevelObjectManager.Instance.Cursor.Move(Vector3.back);
        //onPressLeftArrow += () => LevelObjectManager.Instance.Cursor.Move(Vector3.left);
        //onPressRightArrow += () => LevelObjectManager.Instance.Cursor.Move(Vector3.right);

        //onPressZ += () => LevelObjectManager.Instance.Cursor.SwitchVoxeroid();
        //onPressX += () => LevelObjectManager.Instance.Cursor.RotateVoxeroid();
        //onPressSpace += () => LevelObjectManager.Instance.Cursor.ReleaseVoxeroid();

        onScrollMouseWheel += (i) =>
        {
            if (0 < i)
                SwitchVoxeroid(i);
            else
                cursor.RotateVoxeroid();
        };
        onPressMouseLeftButtonDown += () => cursor.ReleaseVoxeroid();
        onPressMouseRightButton += () => cameraController.RotateCamera();

        mask1 = LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Terrain) |
            LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Player);
        mask2 = LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Terrain) |
            LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Player) |
            LayerMaskUtility.Instance.GetLayerMask(LayerMaskUtility.LayerName.Target);
        snappedPosition = Vector3.zero;
    }

    new void Update()
    {
        base.Update();
        UpdateMousePosition();
        cameraController.UpdateMousePosition();
    }

    void UpdateMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask1))
        {
            var from = hit.point + (Camera.main.transform.position - transform.position).normalized * 0.01f + Vector3.up * 10f;
            from.x = Mathf.RoundToInt(from.x);
            from.z = Mathf.RoundToInt(from.z);
            if (Physics.Raycast(from + Vector3.up * 20, Vector3.down, out hit, Mathf.Infinity, mask2))
            {
                if (hit.collider.gameObject.layer == LayerMaskUtility.Instance.GetLayerNumber(LayerMaskUtility.LayerName.Target))
                {
                    return;
                }
                else
                {
                    cursor.transform.position = hit.point;
                }
            }
        }
    }

    void SwitchVoxeroid(int direction)
    {
        cursor.SwitchVoxeroid(direction);
    }

    void IsTargetLayer()
    {

    }

}
