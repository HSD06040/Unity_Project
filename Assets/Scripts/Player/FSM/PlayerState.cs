using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// state���� ��ӹ��� Ŭ����
public class PlayerState
{
    protected Player player;              // owner�� ���� ������ ������ ����
    protected StateMachine stateMachine;    // ���� ���̸� ���� stateMachine�� ������ ����
    private string animBoolName;

    public PlayerState(Player _player, StateMachine _stateMachine, string _animBoolName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        // ���·� ������ �� ����Ǵ� �Լ�
    }

    public virtual void Update()
    {
        // ���°� ����Ǵ� ���� ����Ǵ� �Լ�
    }

    public virtual void Exit()
    {
        // ���¿��� ������ ����Ǵ� �Լ�
    }

    public virtual void Transition()
    {
        // ���� �� ���ǰ� ���� �� ���¸� �����ϴ� �Լ�

        //ex if(�÷��̾ ������ �����̽��ٸ� ������)
        //      stateMacine.ChangeState(���� ����);
    }
}
