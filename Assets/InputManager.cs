using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    public delegate void OnPressUpArrow();
    public OnPressUpArrow onPressUpArrow = () => { };

    public delegate void OnPressDownArrow();
    public OnPressDownArrow onPressDownArrow = () => { };

    public delegate void OnPressLeftArrow();
    public OnPressDownArrow onPressLeftArrow = () => { };

    public delegate void OnPressRightArrow();
    public OnPressDownArrow onPressRightArrow = () => { };

    public delegate void OnPressSpace();
    public OnPressSpace onPressSpace = () => { };

    public delegate void OnPressZ();
    public OnPressSpace onPressZ = () => { };

    public delegate void OnPressX();
    public OnPressSpace onPressX = () => { };

    public delegate void OnPressC();
    public OnPressSpace onPressC = () => { };

    public delegate void OnPressA();
    public OnPressSpace onPressA = () => { };

    new void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            onPressUpArrow();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            onPressDownArrow();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            onPressLeftArrow();
        }

        if (Input.GetKey(KeyCode.RightArrow))
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
