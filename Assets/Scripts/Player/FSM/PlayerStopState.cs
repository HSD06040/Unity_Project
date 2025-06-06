using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopState : PlayerState
{
    public PlayerStopState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        player.rigid.velocity = input.lastMoveDir * (player.moveSpeed / 2);
    }

    public override void Transition()
    {
        base.Transition();
    }

    public override void Update()
    {
        base.Update();
        if (isFinishAnim)
            stateMachine.SetupState(stateCon.idleState);
        else if (input.camDir.sqrMagnitude > 0)
            stateMachine.SetupState(stateCon.moveState);
    }
}
