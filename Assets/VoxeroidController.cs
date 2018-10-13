using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VoxeroidController : MonoBehaviour
{
    [SerializeField] List<GameObject> models;
    [SerializeField] List<GameObject> darkModels;
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
        SetVoxeroidActive(true);
        playerCollider.gameObject.SetActive(false);
    }

    GameObject GetModel(VoxeroidType type)
    {
        return models[(int)type];
    }

    public void SetVoxeroidActive(bool active)
    {
        playerCollider.gameObject.SetActive(active);
        if (active)
        {
            models[(int)type].gameObject.SetActive(true);
            darkModels[(int)type].gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(DelaySetDarkModel());
        }
    }

    IEnumerator DelaySetDarkModel()
    {
        yield return new WaitForSeconds(1.5f);
        models[(int)type].gameObject.SetActive(false);
        darkModels[(int)type].gameObject.SetActive(true);
    }

    public Animator GetAnimator(VoxeroidType type)
    {
        return animators[(int)type];
    }

    public void SetVoxeroid(VoxeroidType type)
    {
        this.type = type;
        models.ForEach(v => v.gameObject.SetActive(false));
        GetModel(type).SetActive(true);
    }

    public void RotateCharacter()
    {
        transform.rotation = transform.rotation * Quaternion.AngleAxis(90, Vector3.up);
    }

    public void SwitchVoxeroid(int direction)
    {
        int target = (int)type + direction;

        if (target < 0)
        {
            target = Enum.GetValues(typeof(VoxeroidType)).Length - 1;
        }
        else if (Enum.GetValues(typeof(VoxeroidType)).Length <= target)
        {
            target = 0;
        }

        SetVoxeroid((VoxeroidType)Enum.ToObject(typeof(VoxeroidType), target));
    }

    public Vector3 GetForward()
    {
        return transform.forward * -1;
    }

    public Vector3 GetCenter()
    {
        return transform.position + Vector3.up * 0.5f;
    }

    public void TweenModels()
    {
        models.ForEach(v =>
        {
            Vector3 position = v.transform.position;
            v.transform
                .DOMove(Vector3.up * 0.5f, 0.05f, false)
                .SetRelative()
                .SetEase(Ease.Linear)
                .SetLoops(4, LoopType.Yoyo)
                .OnComplete(() => v.transform.position = position);
        });
    }
}
