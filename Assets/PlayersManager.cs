using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayersManager : SingletonMonoBehaviour<PlayersManager>
{
    public VoxeroidController voxeroid;

    [SerializeField] float speed;
    Vector3 up = Vector3.zero;
    Vector3 down = Vector3.zero;
    Vector3 left = Vector3.zero;
    Vector3 right = Vector3.zero;

    Vector3 upAngle = Vector3.zero;
    Vector3 downAngle = Vector3.zero;
    Vector3 leftAngle = Vector3.zero;
    Vector3 rightAngle = Vector3.zero;

    [SerializeField] float jumpHeight;

    new void Awake()
    {
        base.Awake();

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

        InputManager.Instance.onPressSpace += () => JumpUp();

        InputManager.Instance.onPressZ += () => GenerateVoxeroid(VoxeroidController.VoxeroidType.Zunko);
        InputManager.Instance.onPressX += () => GenerateVoxeroid(VoxeroidController.VoxeroidType.Kiritan);
        InputManager.Instance.onPressC += () => GenerateVoxeroid(VoxeroidController.VoxeroidType.Itako);

        InputManager.Instance.onPressA += () => ExecuteSkillA(voxeroid.type);

    }

    void Update()
    {
        if (voxeroid == null) return;

        if (PlayerStateManager.Instance.state.Equals(PlayerStateManager.PlayerState.Air))
        {
            voxeroid.transform.position += Vector3.down * 0.01f;
        }
    }

    void CharacterMove(Vector3 direction)
    {
        if (voxeroid == null) return;

        if (PlayerStateManager.Instance.state.Equals(PlayerStateManager.PlayerState.Ground) || PlayerStateManager.Instance.state.Equals(PlayerStateManager.PlayerState.Climbing))
        {
            voxeroid.transform.position += direction;
        }
        else if (PlayerStateManager.Instance.state.Equals(PlayerStateManager.PlayerState.Air))
        {
        }
    }

    void CharacterTurn(Vector3 direction)
    {
        if (voxeroid == null) return;

        if (0 < direction.x)
        {
            voxeroid.transform.localEulerAngles = upAngle;
        }
        else if (direction.x < 0)
        {
            voxeroid.transform.localEulerAngles = downAngle;
        }
        else if (0 < direction.z)
        {
            voxeroid.transform.localEulerAngles = leftAngle;
        }
        else if (direction.z < 0)
        {
            voxeroid.transform.localEulerAngles = rightAngle;
        }
    }

    void GeneratePlayer()
    {
        if (voxeroid != null)
        {
            voxeroid.SetLayerAsFrozenPlayer();
        }

        GameObject obj = Instantiate(Resources.Load("Prefab/TestPlayer/TestPlayer")) as GameObject;
        voxeroid = obj.GetComponent<VoxeroidController>();
        ColliderManager.Instance.Initialize();
        PlayerStateManager.Instance.Initialize();
    }

    void GenerateVoxeroid(VoxeroidController.VoxeroidType type)
    {
        GeneratePlayer();
        voxeroid.SetVoxeroid(type);
    }

    void JumpUp()
    {
        if(0 < ColliderManager.Instance.sideColliders.Count)
        {
            Collider target = ColliderManager.Instance.sideColliders
                .OrderBy(c => Vector3.Distance(voxeroid.transform.position, c.bounds.center))
                .FirstOrDefault();
            voxeroid.transform.position = Vector3.up * jumpHeight + target.bounds.center;
        }
    }

    void ExecuteSkillA(VoxeroidController.VoxeroidType type)
    {
        VoxeroidSkillManager.Instance.ExecuteSkillA(type);
    }
}