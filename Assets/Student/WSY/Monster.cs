using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private MonsterData monsterData;

    [Header("Monster Fields")]
    [SerializeField] protected string name;
    [SerializeField] protected int hp;
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    [SerializeField] protected int gold;
    [SerializeField] protected int exp;
    [SerializeField] protected float detectRadius;
    [SerializeField] protected EnumType.MonsterType monsterType;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected Animator animator;
    protected bool isDead = false;

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


    protected void Move()
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

    protected void Attack()
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
            Manager.Resources.Destroy(gameObject, 2f);

        }
    }

}
