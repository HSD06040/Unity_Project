using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerSkill", menuName = "Skill/Power")]
public class PowerSkill : Skill
{
    public override IEnumerator SkillRoutine()
    {
        yield return waitDelay;
    }

    protected override bool SkillCondition()
    {
        return true;
    }
}
