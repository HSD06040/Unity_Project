using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ������ ����, ������ �����ϴ� Ŭ����
public class StateMachine
{
    public PlayerState currentState;

    /// <summary>
    /// ���� �ʱ�ȭ �Լ�
    /// </summary>
    /// <param name="newState"></param>
    public void InitState(PlayerState newState)
    {
        currentState = newState;
        currentState.Enter();
    }

    /// <summary>
    /// ���� ���� �Լ�
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(PlayerState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    /// <summary>
    /// ������ ���� ������Ʈ �� �Լ��� state�� Update�� Transition�� ȣ���Ű�� �Լ�
    /// </summary>
    public void UpdateStateMachine()
    {
        currentState.Update();
        currentState.Transition();
    }
}
