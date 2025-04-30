using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private float attackDuration = 0.5f; // ���� ���ӽð�
    private float attackTimer;

    public PlayerAttackState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        attackTimer = attackDuration;

        // �����ϸ� ���� ��Ʈ �ڽ� �ѱ�
        if (player.attackHitbox != null)
            player.attackHitbox.SetActive(true);

    }

    public override void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            if (player.moveDir.sqrMagnitude > 0)
            {
                stateMachine.ChangeState(player.stateCon.moveState);
            }
            else
            {
                stateMachine.ChangeState(player.stateCon.idleState);
            }

        }
    }

    public override void Exit()
    {
        base.Exit();

        // ���� ��Ʈ�ڽ� ����
        if (player.attackHitbox != null)
        {
            player.attackHitbox.SetActive(false);
        }
    }
}
