using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZundaArrow : MonoBehaviour
{
    [SerializeField] float speed;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
