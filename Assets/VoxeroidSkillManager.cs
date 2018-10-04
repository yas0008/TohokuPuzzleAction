using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxeroidSkillManager : SingletonMonoBehaviour<VoxeroidSkillManager>
{
    [SerializeField] List<VoxeroidSkill> skillsA;
    [SerializeField] List<VoxeroidSkill> skillsS;

    new void Awake()
    {
        base.Awake();
    }

    public void ExecuteSkillA(VoxeroidController.VoxeroidType type)
    {
        skillsA[(int)type].ExecuteSkill();
    }

}
