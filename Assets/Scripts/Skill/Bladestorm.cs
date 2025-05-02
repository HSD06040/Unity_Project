using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "BladstormSkill", menuName = "Skill/Bladstorm")]
public class Bladestorm : Skill
{
    [Header("Bladstorm")]
    [SerializeField] private Vector3 bladestormOffset;
    [SerializeField] private float bladestormWait;

    public override IEnumerator SkillRoutine()
    {
        yield return waitDelay;
        float startTime = Time.time;

        while (Time.time - startTime < bladestormWait)
        {
            if (curEffect != null)
                DamageToTargets(skillPower);

            yield return waitTickDelay;
        }

        yield return new WaitForSeconds(bladestormWait);

        while (true)
        {
            if(curEffect != null)
                DamageToTargets(skillPower, bladestormOffset);

            yield return waitTickDelay;
        }
    }

    protected override bool SkillCondition()
    {
        return true;
    }
}
