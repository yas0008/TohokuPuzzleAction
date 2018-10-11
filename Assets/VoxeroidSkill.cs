using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VoxeroidSkill : MonoBehaviour
{
    public abstract List<VoxeroidController> ExecuteSkill(VoxeroidController voxeroid);
    public abstract IEnumerator DelayExecuteSkill(VoxeroidController voxeroid);

    protected abstract List<VoxeroidController> FindNextVoxeroid(VoxeroidController voixeroid);

    protected IEnumerator DelayExecuteNextSkill(VoxeroidController performer, VoxeroidController supporter, float wait)
    {
        yield return new WaitForSeconds(wait);
        VoxeroidSkillManager.Instance.ExecuteSkill(performer, supporter);
    }

}
