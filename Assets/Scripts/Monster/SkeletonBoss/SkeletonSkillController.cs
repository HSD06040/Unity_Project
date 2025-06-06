using UnityEngine;

public class SkeletonSkillController : SkillController
{
    private SkeletonBoss boss;

    protected override void Awake()
    {
        base.Awake();

        boss = GetComponent<SkeletonBoss>();
    }

    public void UseSkills(int index)
    {
        UseSKill(ownerSkills[index]);
    }
}