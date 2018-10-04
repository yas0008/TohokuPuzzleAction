using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : SingletonMonoBehaviour<PlayerStateManager>
{
    public PlayerState state;

    public enum PlayerState
    {
        Air,
        Ground,
        Climbing,
    }

    public void Initialize()
    {
        state = PlayerState.Air;
    }

    public void UpdateState()
    {
        if (0 < ColliderManager.Instance.sideColliders.Count)
        {
            state = PlayerState.Climbing;
        }
        else if (0 < ColliderManager.Instance.groundColliders.Count)
        {
            state = PlayerState.Ground;
        }
        else
        {
            state = PlayerState.Air;
        }

        Debug.Log(state);
    }

}
