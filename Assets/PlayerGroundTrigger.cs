using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundTrigger : MonoBehaviour
{
    [SerializeField] List<int> layers;

    void OnTriggerEnter(Collider other)
    {
        if (layers.Contains(other.gameObject.layer))
        {
            //ColliderManager.Instance.RegisiterCollider(ColliderManager.Instance.groundColliders, other);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (layers.Contains(other.gameObject.layer))
        {
            //ColliderManager.Instance.UnregisterCollider(ColliderManager.Instance.groundColliders, other);
        }
    }
}
