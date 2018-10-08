using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZundaArrow : Projectile
{
    [SerializeField] float speed;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
