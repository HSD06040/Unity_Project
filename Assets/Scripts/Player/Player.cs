using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    public Animator anim;
    public float moveSpeed;
    public float rotateSpeed;

    // ����Ƽ���� Ground ���̾� �߰��ϱ�
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.2f;

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    // ���߿� �ּ� ���ֵ� �˴ϴ�.
}
