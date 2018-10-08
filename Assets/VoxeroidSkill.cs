﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VoxeroidSkill : MonoBehaviour
{
    public abstract void ExecuteSkill(VoxeroidController voxeroid);

    protected abstract List<VoxeroidController> FindNextVoxeroid(VoxeroidController voixeroid);

    protected IEnumerator DelayExecuteSkill(VoxeroidController performer, VoxeroidController supporter, float wait)
    {
        yield return new WaitForSeconds(wait);
        VoxeroidSkillManager.Instance.ExecuteSkill(performer, supporter);
    }

}
