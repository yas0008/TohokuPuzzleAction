using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInputManager : MonoBehaviour
{
    protected delegate void OnPressUpArrow();
    protected OnPressUpArrow onPressUpArrow = () => { };

    protected delegate void OnPressDownArrow();
    protected OnPressDownArrow onPressDownArrow = () => { };

    protected delegate void OnPressLeftArrow();
    protected OnPressDownArrow onPressLeftArrow = () => { };

    protected delegate void OnPressRightArrow();
    protected OnPressDownArrow onPressRightArrow = () => { };

    protected delegate void OnPressSpace();
    protected OnPressSpace onPressSpace = () => { };

    protected delegate void OnPressZ();
    protected OnPressSpace onPressZ = () => { };

    protected delegate void OnPressX();
    protected OnPressSpace onPressX = () => { };

    protected delegate void OnPressC();
    protected OnPressSpace onPressC = () => { };

    protected delegate void OnPressA();
    protected OnPressSpace onPressA = () => { };

    public delegate void OnPressMouseLeftButtonDown();
    public OnPressMouseLeftButtonDown onPressMouseLeftButtonDown = () => { };

    public delegate void OnPressMouseRightButton();
    public OnPressMouseRightButton onPressMouseRightButton = () => { };

    public delegate void OnScrollMouseWheel(int direction);
    public OnScrollMouseWheel onScrollMouseWheel = (i) => { };

    protected void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onPressMouseLeftButtonDown();
        }

        if (Input.GetMouseButton(1))
        {
            onPressMouseRightButton();
        }

        if (0 < Input.GetAxis("Mouse ScrollWheel"))
        {
            onScrollMouseWheel(1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            onScrollMouseWheel(-1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            onPressUpArrow();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            onPressDownArrow();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            onPressLeftArrow();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            onPressRightArrow();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            onPressSpace();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            onPressZ();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            onPressX();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            onPressC();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            onPressA();
        }

    }
}
