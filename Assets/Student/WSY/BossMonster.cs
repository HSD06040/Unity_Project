using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Monster
{
    void Update()
    {
        Move();
        Attack();
        BossAttack();
        BossDie();
    }

    private void BossAttack()
    {
        // �ݶ��̴��� ���� ���� ������ ���� ���� �����ϰ�, (���� ���� �� ��ü���� �迭�� �����, 2�� ���� ���� ����)
        Collider[] others = Physics.OverlapSphere(transform.position, detectRadius * 2, playerLayer);

        foreach (var other in others)
        {
            if (other.CompareTag("Player"))
            {
                // �������� ���� �ִϸ��̼� (��: "BossAttack"�̶�� Ʈ����)
                animator.SetTrigger("BossAttack");

                // �÷��̾��� ü���� ���ҽ�Ű��(�Ʒ��� ���� ����, Ȱ��ȭ �ڵ�� IDamagable ���)
                // Manager.Data.playerStatus.curHP -= damage;
                IDamagable target = other.GetComponent<IDamagable>();
                target.TakeDamage(damage*2);

                break;
            }
        }
    }

    private void BossDie()
    {
        if (hp == 0 && !isDead)
        {
            // �׾����� ���θ� true�� �ٲ��ְ� 
            isDead = true;

            // ��� �ִϸ��̼� �ߵ��ϱ�(Ʈ���� �̸� BossDie�� ����)
            animator.SetTrigger("BossDie");

            // �÷��̾��� ��� ������Ű��
            //Manager.Data.playerStatus.(��� �߰� ��) += gold * Random.Range(0.8f, 1.1f);

            // �÷��̾��� ����ġ ������Ű�� (������ �Ŵ��� ���ؼ�)
            Manager.Data.playerStatus.curExp += exp;

            // ���� ������Ʈ�� 2�� �ڿ� �ٽ� Ǯ�� ����������.
            Manager.Resources.Destroy(gameObject, 2f);

        }
    }

}
