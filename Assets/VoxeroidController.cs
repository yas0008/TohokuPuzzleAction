using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxeroidController : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 up = Vector3.zero;
    Vector3 down = Vector3.zero;
    Vector3 left = Vector3.zero;
    Vector3 right = Vector3.zero;

    Vector3 upAngle = Vector3.zero;
    Vector3 downAngle = Vector3.zero;
    Vector3 leftAngle = Vector3.zero;
    Vector3 rightAngle = Vector3.zero;

    void Awake()
    {
        up = new Vector3(speed, 0, 0);
        down = new Vector3(-speed, 0, 0);
        left = new Vector3(0, 0, speed);
        right = new Vector3(0, 0, -speed);

        upAngle = new Vector3(0, 90, 0);
        downAngle = new Vector3(0, 270, 0);
        leftAngle = new Vector3(0, 0, speed);
        rightAngle = new Vector3(0, 180, -speed);
    }

    void Start()
    {
        InputManager.Instance.onPressUpArrow += () => CharacterMove(up);
        InputManager.Instance.onPressUpArrow += () => CharacterTurn(up);

        InputManager.Instance.onPressDownArrow += () => CharacterMove(down);
        InputManager.Instance.onPressDownArrow += () => CharacterTurn(down);

        InputManager.Instance.onPressLeftArrow += () => CharacterMove(left);
        InputManager.Instance.onPressLeftArrow += () => CharacterTurn(left);

        InputManager.Instance.onPressRightArrow += () => CharacterMove(right);
        InputManager.Instance.onPressRightArrow += () => CharacterTurn(right);

    }

    void CharacterMove(Vector3 direction)
    {
        transform.position += direction;
    }

    void CharacterTurn(Vector3 direction)
    {
        if (0 < direction.x)
        {
            transform.localEulerAngles = upAngle;
        }
        else if (direction.x < 0)
        {
            transform.localEulerAngles = downAngle;
        }
        else if (0 < direction.z)
        {
            transform.localEulerAngles = leftAngle;
        }
        else if (direction.z < 0)
        {
            transform.localEulerAngles = rightAngle;
        }
    }

}
