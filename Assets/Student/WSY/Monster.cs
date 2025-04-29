using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private MonsterData monsterData;

    [Header("Monster Fields")]
    [SerializeField] private string name;
    [SerializeField] private int hp;
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private int gold;
    [SerializeField] private int exp;
    [SerializeField] private float detectRadius;
    [SerializeField] private EnumType.MonsterType monsterType;

    [SerializeField] private Rigidbody rigid;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Animator animator;
    private bool isDead = false;

    // ���� ������(�� ������ ����)
    private void Awake()
    {
        // ���⸦ Ư�� Ÿ���� �� ���� �� �� �ְԲ� ���� Ÿ���� ������ �ξ���.
        monsterData = Manager.Data.monsterData.GetMonsterData(monsterType);

        // ���� ������ �÷��̾��� ������ ���� ����.
        // ������ �ִ� ������ 5��? (�÷��̾� ���� ���� ����)
        //int playerLevel = Manager.Data.playerStatus.levelExpData.(�÷��̾� ����);
        //int monsterlevel = Mathf.Clamp(Random.Range(playerLevel - 2, playerLevel - 1), 1, 5);
    }

    void Update()
    {
        Move();
        Attack();
        Die();
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
                // ���� �ִϸ��̼��� �Ҵ��ϰ� (Ʈ���� �̸� Attack�̶�� ����)
                animator.SetTrigger("Attack");

                // �÷��̾��� ü���� ���ҽ�Ű��
                Manager.Data.playerStatus.curHP -= damage;

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
        if (hp == 0 && !isDead)
        {
            // �׾����� ���θ� true�� �ٲ��ְ� 
            isDead = true;

            // ��� �ִϸ��̼� �ߵ��ϱ�(Ʈ���� �̸� Die�� ����)
            animator.SetTrigger("Die");

            // �÷��̾��� ��� ������Ű��
            //Manager.Data.playerStatus.(��� �߰� ��) += gold * Random.Range(0.8f, 1.1f);

            // �÷��̾��� ����ġ ������Ű�� (������ �Ŵ��� ���ؼ�)
            Manager.Data.playerStatus.curExp += exp;

            // ���� ������Ʈ�� 2�� �ڿ� �ٽ� Ǯ�� ����������.
            Manager.Pool.Release(gameObject, 2f);

        }
    }

}
