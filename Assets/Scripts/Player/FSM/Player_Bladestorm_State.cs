using UnityEngine;

public class Player_Bladestorm_State : PlayerState
{
    public Player_Bladestorm_State(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Transition()
    {
        base.Transition();

        if (!player.isSkillActive)
        {
            if (player.moveDir.sqrMagnitude > 0)
                stateMachine.ChangeState(stateCon.moveState);
            else
                stateMachine.ChangeState(stateCon.idleState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
