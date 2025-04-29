using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamagable
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

    // 몬스터 생성자(와 동일한 역할)
    private void Awake()
    {
        // 여기를 특정 타입을 안 쓰고 할 수 있게끔 몬스터 타입을 변수로 두었음.
        monsterData = Manager.Data.monsterData.GetMonsterData(monsterType);

        // 몬스터 레벨을 플레이어의 레벨에 따라 조정.
        // 몬스터의 최대 레벨은 5로? (플레이어 레벨 보고 결정)
        //int playerLevel = Manager.Data.playerStatus.levelExpData.(플레이어 레벨);
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
        // 이 씬 내에 플레이어 태그를 가진 오브젝트가 있는지 살피고
        GameObject player = GameObject.FindWithTag("Player");

        // 씬 안에 플레이어가 있다면
        if (player != null)
        {
            // 플레이어를 바라보게 회전하고
            transform.LookAt(player.transform.position * Time.deltaTime);

            // 플레이어에게 다가가기
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        }
    }

    protected void Attack()
    {
        // 콜라이더로 원형 추적 범위에 들어온 것을 감지하고, (감지 범위 내 물체들은 배열로 저장됨)
        Collider[] others = Physics.OverlapSphere(transform.position, detectRadius, playerLayer);

        foreach (var other in others)
        {
            if (other.CompareTag("Player"))
            {
                // 공격 애니메이션을 할당하고 (트리거 이름 Attack이라고 가정)
                animator.SetTrigger("Attack");

                // 플레이어의 체력을 감소시키기(아래는 직접 수정, 활성화 코드는 IDamagable 사용)
                // Manager.Data.playerStatus.curHP -= damage;
                IDamagable target = other.GetComponent<IDamagable>();
                target.TakeDamage(damage);

                // 플레이어를 찾았으므로 루프 마무리
                break; 
            }
        }

        //추적 범위 기즈모도 그려주기 
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
    
    protected virtual void Die()
    {
        if (hp == 0 && !isDead)
        {
            // 죽었는지 여부를 true로 바꿔주고 
            isDead = true;

            // 사망 애니메이션 발동하기(트리거 이름 Die로 가정)
            animator.SetTrigger("Die");

            // 플레이어의 골드 증가시키기
            //Manager.Data.playerStatus.(골드 추가 시) += gold * Random.Range(0.8f, 1.1f);

            // 플레이어의 경험치 증가시키기 (데이터 매니저 통해서)
            Manager.Data.playerStatus.AddExp(exp);

            // 게임 오브젝트를 2초 뒤에 다시 풀로 돌려보내기.
            Manager.Resources.Destroy(gameObject, 2f);

        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;

        if(hp <= 0)
        {
            Die();
        }
    }
}
