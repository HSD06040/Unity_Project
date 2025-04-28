using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [Header("Monster Fields")]
    [SerializeField] private string name;
    [SerializeField] private int hp;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float gold;
    [SerializeField] private float exp;
    [SerializeField] private float detectRadius;

    [SerializeField] private Rigidbody rigid;
    [SerializeField] private LayerMask playerLayer;

    void Update()
    {
        Move();
        Attack();
    }


    private void Move()
    {
        // �� �� ���� �÷��̾� �±׸� ���� ������Ʈ�� �ִ��� ���ǰ�
        GameObject player = GameObject.FindWithTag("Player");

        // �� �ȿ� �÷��̾ �ִٸ�
        if (player != null)
        {
            // �÷��̾ �ٶ󺸰� ȸ���ϰ�
            transform.LookAt(player.transform.position * Time.deltaTime);

            // �÷��̾�� �ٰ�����
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        }
    }

    private void Attack()
    {
        // �ݶ��̴��� ���� ���� ������ ���� ���� �����ϰ�, (���� ���� �� ��ü���� �迭�� �����)
        Collider[] others = Physics.OverlapSphere(transform.position, detectRadius, playerLayer);

        foreach (var other in others)
        {
            if (other.CompareTag("Player"))
            {
                // ���� �ִϸ��̼��� �Ҵ��ϰ� 
                // TODO: ���� �ִϸ��̼� �Ҵ��ϱ� 
                // �÷��̾��� ü���� ���ҽ�Ű�� (������ �Ŵ��� ���ؼ�)
                // TODO: �÷��̾��� ü�� ���� ��Ű�� 
                // �÷��̾ ã�����Ƿ� ���� ������
                break; 
            }
        }
    

        //���� ���� ����� �׷��ֱ� 
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
    
    private void Die()
    {
        if(hp == 0)
        {
            // ��� �ִϸ��̼� �ߵ��ϱ�
            // TODO: ��� �ִϸ��̼� �Ҵ��ϱ� 
            // �÷��̾��� ��� ������Ű�� (������ �Ŵ��� ���ؼ�)
            // TODO: �÷��̾��� ��� ������Ű�� (������ �Ŵ��� ���ؼ�)
            // Ȱ���� ��: gold x Random.Range(0.8f, 1.1f)
            // �÷��̾��� ����ġ ������Ű�� (������ �Ŵ��� ���ؼ�)
            // TODO: �÷��̾��� ����ġ ������Ű�� (������ �Ŵ��� ���ؼ�)
        }
    }


}
