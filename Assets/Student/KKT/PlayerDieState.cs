using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerState
{
    public PlayerDieState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.rigid.velocity = Vector3.zero; // �ӵ� �ʱ�ȭ
        player.rigid.isKinematic = true; // ���� ��Ȱ��ȭ

        player.moveDir = Vector3.zero;
        player.camDir = Vector3.zero;
    }
}
