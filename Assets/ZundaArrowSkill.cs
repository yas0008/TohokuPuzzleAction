using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZundaArrowSkill : VoxeroidSkill
{
    [SerializeField] float speed;

    public override void ExecuteSkill()
    {
        var obj = Instantiate(Resources.Load("Prefab/ZundaArrow/ZundaArrow")) as GameObject;
        obj.transform.position = PlayersManager.Instance.voxeroid.transform.position;
        obj.transform.rotation = PlayersManager.Instance.voxeroid.transform.rotation;
        PlayersManager.Instance.voxeroid.GetAnimator(VoxeroidController.VoxeroidType.Zunko).Play("Attack", 0, 0);

        Destroy(obj, 5f);
    }
}
