using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ state
public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        Move();
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Transition()
    {
        base.Transition();

        // ex) �����̽��ٸ� ������ ���� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.stateCon.jumpState);
        }
        else if (player.moveDir.sqrMagnitude == 0)
        {
            stateMachine.ChangeState(player.stateCon.idleState);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(player.stateCon.attackState);
        }
    }

    // ������
    private void Move()
    {
        player.transform.Translate(player.moveDir*player.moveSpeed*Time.deltaTime);
    }
}
