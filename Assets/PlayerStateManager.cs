using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : SingletonMonoBehaviour<PlayerStateManager>
{
    public PlayerState state;

    public enum PlayerState
    {
        Ready,
        Air,
        Ground,
    }

    public void Initialize()
    {
        state = PlayerState.Ready;
    }

    public void UpdateState()
    {
        //if (0 < ColliderManager.Instance.groundColliders.Count)
        //{
        //    state = PlayerState.Ground;
        //}
        //else
        //{
        //    state = PlayerState.Air;
        //}
    }

}
