using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxeroidController : MonoBehaviour
{
    [SerializeField] List<GameObject> voxeroids;
    [SerializeField] List<Animator> animators;

    public enum VoxeroidType
    {
        Zunko,
        Kiritan,
        Itako,
    }

    public VoxeroidType type;

    GameObject GetBody(VoxeroidType type)
    {
        return voxeroids[(int) type];
    }

    public Animator GetAnimator(VoxeroidType type)
    {
        return animators[(int) type];
    }

    public void SetLayerAsFrozenPlayer()
    {
        gameObject.layer = 13;
        SetLayerRecursively(gameObject);
    }

    void SetLayerRecursively(GameObject self)
    {
        foreach (Transform n in self.transform)
        {
            n.gameObject.layer = 13;
            SetLayerRecursively(n.gameObject);
        }
    }

    public void SetVoxeroid(VoxeroidType type)
    {
        this.type = type;
        voxeroids.ForEach(v => v.gameObject.SetActive(false));
        GetBody(type).SetActive(true);
    }

}