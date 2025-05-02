using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, ISkillOwner
{
    [SerializeField] private Transform groundCheckPoint;

    public Animator anim;
    public Rigidbody rigid;
    public StateController stateCon;
    public PlayerStatusController statusCon;
    public GameObject attackHitbox;
    public PlayerStatusData status => Manager.Data.playerStatus;
    public float moveSpeed;

    public float rotateSpeed;
    public float roolForce = 10;

    public Vector3 moveDir {  get; private set; }
    public Vector3 camDir {  get; private set; }
    private bool _isSkillActive;
    public bool isSkillActive { get; set; }

    public Vector3 lastMoveDir;
    public float[] attackMoveForce;
    public float[] attackForce;

    public bool invincibility;
                
    // 유니티에서 Ground 레이어 추가하기
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.2f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        stateCon = GetComponent<StateController>();
    }

    private void Update()
    {
        //if (stateCon.stateMachine.currentState == stateCon.dieState)
        //{
        //    return;
        //}

        HandleInput();

        //if(status.curHP <= 0)
        //{
        //    stateCon.stateMachine.ChangeState(stateCon.dieState);
        //}
    }
    public bool IsGrounded()
    {
        return Physics.Raycast(groundCheckPoint.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    private void HandleInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveDir = new Vector3(x, 0, z);

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        camDir = camForward * moveDir.z + camRight * moveDir.x;  
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public int GetDamage()
    {
        return status.damage.GetValue();
    }
}