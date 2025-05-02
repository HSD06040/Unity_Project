using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Roll_State : PlayerState
{
    private float invincibilityTime = 0.15f;
    
    public Player_Roll_State(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.invincibility = true;

        stateTimer = invincibilityTime;

        Vector3 inputDir = player.camDir.normalized;

        if(inputDir.sqrMagnitude < 0.01f)
        inputDir = player.transform.forward;


        rb.AddForce(inputDir * player.roolForce, ForceMode.Impulse);
        Vector3 localInput = player.transform.InverseTransformDirection(inputDir);

        anim.SetFloat("RollZ", localInput.z);
        anim.SetFloat("RollX", localInput.x);
    }

    public override void Exit()
    {
        base.Exit();

        player.invincibility = false;

        rb.velocity = Vector3.zero;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Transition()
    {
        if (isFinishAnim)
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

        if (stateTimer <= 0)
            player.invincibility = false;
    }
}
