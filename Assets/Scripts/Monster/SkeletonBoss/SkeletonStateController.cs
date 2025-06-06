using UnityEngine;

public class SkeletonStateController : MonoBehaviour
{
    #region States
    public SkeletonStateMachine sm {  get; private set; }
    public Skeleton_Idle idle { get; private set; }
    public Skeleton_Chase chase { get; private set; }
    public Skeleton_Attack attack { get; private set; }
    public Skeleton_Hit hit { get; private set; }
    public Skeleton_Stun stun { get; private set; }
    public Skeleton_Die die { get; private set; }

    // Skill
    public Skeleton_SwordSkill skill1 { get; private set; }
    public Skeleton_RoarSkill skill2 { get; private set; }
    #endregion

    public SkeletonBoss skeleton;

    private void Awake()
    {
        skeleton ??= GetComponent<SkeletonBoss>();

        sm = new SkeletonStateMachine();

        idle    = new Skeleton_Idle(skeleton, sm, "Idle");
        chase   = new Skeleton_Chase(skeleton, sm, "Chase");
        hit     = new Skeleton_Hit(skeleton, sm, "Hit");
        stun    = new Skeleton_Stun(skeleton, sm, "Stun");
        die     = new Skeleton_Die(skeleton, sm, "Dead");

        attack  = new Skeleton_Attack(skeleton, sm, "Attack");
        skill1  = new Skeleton_SwordSkill(skeleton, sm, "Skill1");
        skill2  = new Skeleton_RoarSkill(skeleton, sm, "Skill2");

        skeleton.statusCon.OnSettingEnded += Init;
    }

    private void Init()
    {
        sm.InitState(idle);
        skeleton.statusCon.OnHitted += HitState;
        skeleton.statusCon.OnStunGaugeChanged += StunState;
        skeleton.statusCon.OnDied += DieState;

        Manager.Game.CreateBossBarUI(skeleton.statusCon);
    }

    private void Update()
    {
        sm.UpdateStateMachine();
    }

    private void HitState()
    {
        if (!skeleton.isUnhittable && !(sm.currentState == stun))
        {
            sm.ChangeState(hit);
        }
    }

    private void StunState(float value)
    {
        if(value <= 0 && !skeleton.isStun)
        {
            skeleton.isStun = true;
            sm.ChangeState(stun);
        }
    }

    private void DieState()
    {
        sm.ChangeState(die);
    }

    private void OnDestroy()
    {
        skeleton.statusCon.OnHitted -= HitState;
    }

    private void AnimFinishTrigger() => sm.currentState.AnimFinishEvent();
    private void MobMoveEvent(float force) => skeleton.rigid.AddForce(skeleton.transform.forward * force, ForceMode.Impulse);
}
