using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float rotateSpeed;

    Vector3 rotation;
    Vector2 lastCameraPosition;

    void Start()
    {
        rotation = Vector3.zero;
        lastCameraPosition = Input.mousePosition;
    }

    public void UpdateMousePosition()
    {
        lastCameraPosition = Input.mousePosition;
    }

    public void RotateCamera()
    {
        float x = lastCameraPosition.x - Input.mousePosition.x;
        float y = lastCameraPosition.y - Input.mousePosition.y;

        rotation.x = y;
        rotation.x = 0;
        rotation.y = -x;

        transform.Rotate(rotation * rotateSpeed);

    }

}
