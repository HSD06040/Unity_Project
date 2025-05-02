using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CrossSlash_State : PlayerState
{
    public Player_CrossSlash_State(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

    public override void Transition()
    {
        base.Transition();
    }

    public override void Update()
    {
        base.Update();
        
        if (!player.isSkillActive)
            stateMachine.ChangeState(stateCon.idleState);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
