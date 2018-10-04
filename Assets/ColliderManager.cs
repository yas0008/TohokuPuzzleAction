using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColliderManager : SingletonMonoBehaviour<ColliderManager>
{
    public List<Collider> sideColliders
    {
        get;
        private set;
    }

    public List<Collider> groundColliders
    {
        get;
        private set;
    }

    new void Awake()
    {
        base.Awake();
        Initialize();
    }

    public void Initialize()
    {
        sideColliders = new List<Collider>();
        groundColliders = new List<Collider>();
    }

    public void RegisiterCollider(List<Collider> colliders, Collider collider)
    {
        if (colliders.All(c => c.GetInstanceID() != collider.GetInstanceID()))
        {
            colliders.Add(collider);
            PlayerStateManager.Instance.UpdateState();
        }
    }

    public void UnregisterCollider(List<Collider> colliders, Collider collider)
    {
        colliders.RemoveAll(c => c.GetInstanceID() == collider.GetInstanceID());
        PlayerStateManager.Instance.UpdateState();
    }

}
