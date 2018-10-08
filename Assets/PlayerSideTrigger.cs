using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSideTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (gameObject.layer == 13) return;
        //ColliderManager.Instance.RegisiterCollider(ColliderManager.Instance.sideColliders, other);
    }
    void OnTriggerExit(Collider other)
    {
        if (gameObject.layer == 13) return;
        //ColliderManager.Instance.UnregisterCollider(ColliderManager.Instance.sideColliders, other);
    }
}
