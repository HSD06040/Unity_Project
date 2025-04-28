using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    private Player player;
    public StateMachine stateMachine {  get; private set; }
    public PlayerMoveState moveState { get; private set; }

    private void Awake()
    {
        player = GetComponent<Player>();

        stateMachine = new StateMachine();
                
        moveState = new PlayerMoveState(player, stateMachine, "Move");
        // �׷��� ��ü���� stateMachine�� ���� ������ �־����
    }

    private void Start()
    {
        stateMachine.InitState(moveState); // ó���� ���� �ʱ�ȭ
    }

    private void Update()
    {
        stateMachine.UpdateStateMachine(); // state��ü���� Update�� ȣ���� �� ��� Controller���� ����
    }
}
