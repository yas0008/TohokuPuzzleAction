using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZundaMochi : MonoBehaviour
{
    [SerializeField] Vector3 fallingSpeed;
    ZundaMochiState state;

    public enum ZundaMochiState
    {
        Static,
        Falling,
    }

    void Update()
    {
        if(state.Equals(ZundaMochiState.Falling))
        {
            transform.position += fallingSpeed * Time.deltaTime;
        }
    }

    public void SetState(ZundaMochiState state)
    {
        this.state = state;
    }

}
