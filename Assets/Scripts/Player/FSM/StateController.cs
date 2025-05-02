using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class StateController : MonoBehaviour 
{
    private Player player;

#region States
    public StateMachine stateMachine {  get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerStopState stopState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public Player_Roll_State rollState { get; private set; }
    //public PlayerDieState dieState { get; private set; }

    // Skill
    public Player_CrossSlash_State crossSlashState { get; private set; }
    public Player_PowerSkill_State powerSkillState { get; private set; }
    public Player_Bladestorm_State bladestormState { get; private set; }

#endregion
    private void Awake()
    {
        player = GetComponent<Player>();

        stateMachine = new StateMachine();
                
        moveState = new PlayerMoveState(player, stateMachine, "Move");
        jumpState = new PlayerJumpState(player, stateMachine, "Jump");
        idleState = new PlayerIdleState(player, stateMachine, "Idle");
        attackState = new PlayerAttackState(player, stateMachine, "Attack");
        stopState = new PlayerStopState(player, stateMachine, "Stop");
        rollState = new Player_Roll_State(player, stateMachine, "Roll");
        // dieState = new PlayerDieState(player, stateMachine, "Die");

        // Skill

        crossSlashState = new Player_CrossSlash_State(player, stateMachine, "CrossSlash");
        powerSkillState = new Player_PowerSkill_State(player, stateMachine, "Power");
        bladestormState = new Player_Bladestorm_State(player, stateMachine, "Bladestorm");
    }

    private void Start()
    {
        stateMachine.InitState(idleState); // 처음엔 상태 초기화
    }

    private void Update()
    {
        stateMachine.UpdateStateMachine(); // state자체에는 Update를 호출할 수 없어서 Controller에서 실행
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    public void AnimFinishEvent() => stateMachine.currentState.AnimFinishEvent();
    public void CanNextComboEvent() => attackState.canNextCombo = true;
    private void MoveEvent(float force) => player.rigid.AddForce(player.transform.forward * force,ForceMode.Impulse);
}
