using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MonsterFactory : MonoBehaviour
{
    [Header("Monster Factory Fields")]
    [SerializeField] private int spawnNum;
    [SerializeField] private float spawnTime;
    [SerializeField] private int initialMonsterNum;
    [SerializeField] private int cumulativeMonsterNum;
    [SerializeField] private int targetcumMonsterNum;
    [SerializeField] private GameObject spawnpoint;

    [Header("Prefabs")]
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private GameObject orcPrefab;
    [SerializeField] private GameObject goblinPrefab;


    [SerializeField] private PoolManager poolManager;
    private float spawnTimer;
    private bool isWaitingToSpawn;


    void Start()
    {
        // ������ ������ ������ �޶����Ƿ� ���� �� �̸� Ȯ��, �׿� ���� ������ ���� ����
        
        
        // ���� ������ ��,
        // ������Ʈ Ǯ �Ŵ����� Slime/Orc/Goblin �� ������ �������� ������Ʈ Ǯ�� ������ֱ�.
        poolManager.Get(slimePrefab, Vector3.zero, Quaternion.identity);
        poolManager.Get(orcPrefab, Vector3.zero, Quaternion.identity);
        poolManager.Get(goblinPrefab, Vector3.zero, Quaternion.identity);

        // �ʱ� ���� ����(�� ������, � ������ ������ ���� �ʿ� + �ƴϸ� �׳� ��ġ�ϱ�?)
        for (int i = 0; i < initialMonsterNum; i++)
        {
            // SpawnMonster("Slime");
        }


    }

    void Update()
    {
        
        
    }


    public Monster Create(string name)
    {
        // ��������� ���� ���ͼ��� ��ǥ ���ͼ����� ���ٸ� 
        if (cumulativeMonsterNum < targetcumMonsterNum + 1)
        {
            GameObject detectedMonster = GameObject.FindWithTag("Monster");
            
            // �� �ȿ� ���Ͱ� �� ������ ���ٸ�
            if (detectedMonster == null)
            {
                // ���� Ÿ�̸� ����,
                // Ÿ�̸Ӱ� �� �Ǹ� ���͸� spawnNum��ŭ ����.(������Ʈ Ǯ����.)


                
                // spawnpoint�� spawnTime �Ŀ� spawnNum ���� ����
                // (������Ʈ�� Ǯ ��� -> ���ӿ�����Ʈ ���� = Instantiate(����� ������,��������Ʈ, ���ʹϾ�.���̵�ƼƼ)
                // �� �Ʒ� ������ ������Ʈ Ǯ�� ����ؼ� ��� �Ұ��ΰ�.
                // �� �Ʒ� �ֵ��� ������ �������� �����? 
                // �׸��� �� ���� �������� ������ �ν���Ƽ��Ʈ�� ������ֱ�?
                Monster monster;
                switch (name)
                {
                    // TODO: ���� ������ �Ŵ����� Ȱ���ؼ� ���� �� ������ �� �� ������?(���� ���� �־��ִ� �� �ƴ϶�, ������ �Ŵ�����  
                    case "Slime": monster = new Monster("Slime", 100, 10, 10, exp, detectRadius); break;
                    case "Orc": monster = new Monster("Orc", 231, 15, 8, exp, detectRadius); break;
                    case "Goblin": monster = new Monster("Goblin", 70, 13, 15, exp, detectRadius); break;
                    default: return null;
                }

                return monster;
            }
        }
    }

}
