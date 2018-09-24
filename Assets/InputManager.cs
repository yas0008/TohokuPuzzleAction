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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
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

    }

}
