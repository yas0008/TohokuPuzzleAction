using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxeroidController : MonoBehaviour
{
    [SerializeField] List<GameObject> voxeroids;
    [SerializeField] List<Animator> animators;
    [SerializeField] Collider playerCollider;

    public enum VoxeroidType
    {
        Zunko,
        Kiritan,
        Itako,
    }

    public VoxeroidType type;

    void Start()
    {
        SetColliderActive(false);
    }

    GameObject GetBody(VoxeroidType type)
    {
        return voxeroids[(int)type];
    }

    public void SetColliderActive(bool active)
    {
        playerCollider.gameObject.SetActive(active);
    }

    public Animator GetAnimator(VoxeroidType type)
    {
        return animators[(int)type];
    }

    public void SetVoxeroid(VoxeroidType type)
    {
        this.type = type;
        voxeroids.ForEach(v => v.gameObject.SetActive(false));
        GetBody(type).SetActive(true);
    }

    public void RotateCharacter()
    {
        transform.rotation = transform.rotation * Quaternion.AngleAxis(90, Vector3.up);
    }

    public void SwitchVoxeroid()
    {
        int target = Enum.GetValues(typeof(VoxeroidType)).Length <= (int)type + 1 ? 0 : (int)type + 1;
        SetVoxeroid((VoxeroidType)Enum.ToObject(typeof(VoxeroidType), target));
    }

    public Vector3 GetForward()
    {
        return transform.forward * -1;
    }

    public void ApplyTerainEffect()
    {

    }

}
