using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "CrossSlashSkill", menuName = "Skill/CrossSlash")]
public class CrossSlashSkill : Skill
{
    public override IEnumerator SkillRoutine()
    {
        yield return waitDelay;

        DamageToTargets(skillPower);

        yield return new WaitForSeconds(.35f);

        DamageToTargets(skillPower);
    }

    protected override bool SkillCondition()
    {
        return true;
    }
}
