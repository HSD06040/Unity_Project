using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    private Player player;
    public StateMachine stateMachine {  get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerDieState dieState { get; private set; }

    private void Awake()
    {
        player = GetComponent<Player>();

        stateMachine = new StateMachine();
                
        moveState = new PlayerMoveState(player, stateMachine, "Move");
        jumpState = new PlayerJumpState(player, stateMachine, "Jump");
        idleState = new PlayerIdleState(player, stateMachine, "Idle");
        dieState = new PlayerDieState(player, stateMachine, "Die");
        // �׷��� ��ü���� stateMachine�� ���� ������ �־����
    }

    private void Start()
    {
        stateMachine.InitState(idleState); // ó���� ���� �ʱ�ȭ
    }

    private void Update()
    {
        stateMachine.UpdateStateMachine(); // state��ü���� Update�� ȣ���� �� ��� Controller���� ����
    }
}
