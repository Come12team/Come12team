using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; // �� ������
    [SerializeField]
    private GameObject bossPrefab; // �� ������ !!
    [SerializeField]
    private float spwnTime; // �� ���� �ֱ�
    [SerializeField]
    private float bossSpawnDelay = 20f;   // ���� ���� !!
    [SerializeField]
    private Transform[] wayPoints; // ���� ���������� �̵� ���

    private bool bossSpawned = false; // ������ �����Ǿ����� !!


    private void Awake()
    {
        //�� ���� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine("SpawnEnemy");
        StartCoroutine("SpawnBoss"); //!!
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject clone = Instantiate(enemyPrefab);     // �� ������Ʈ ����
            Monster monster = clone.GetComponent<Monster>(); // ��� ������ ���� Enemy ������Ʈ

            monster.Setup(wayPoints);                          // wayPoint ������ �Ű������� Setup() ȣ��

            yield return new WaitForSeconds(spwnTime);      // spawnTime �ð� ���� ���
        }
    }

    private IEnumerator SpawnBoss() // ������ ���� �ǰ� ������ �Ǿ��ٸ� �ѹ��������ϰ� ��!!
    {
        yield return new WaitForSeconds(bossSpawnDelay);

        GameObject bossClone = Instantiate(bossPrefab);
        BossMonster boss = bossClone.GetComponent<BossMonster>();
        boss.Setup(wayPoints); 

        bossSpawned = true;         
    }

}
