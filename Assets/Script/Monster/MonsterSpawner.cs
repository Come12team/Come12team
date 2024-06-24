using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; // 적 프리팹
    [SerializeField]
    private GameObject bossPrefab; // 적 프리팹 !!
    [SerializeField]
    private float spawnerTime; // 적 생성 주기
    [SerializeField]
    private float bossSpawnDelay = 20f;   // 보스 생성 !!
    [SerializeField]
    private Transform[] wayPoints; // 현재 스테이지의 이동 경로

    private bool bossSpawned = false; // 보스가 생성되었는지 !!

    private int enemyCount = 0; // 생성된 적의 개수


    private void Awake()
    {
        //적 생성 코루틴 함수 호출
        StartCoroutine("SpawnEnemy");
        StartCoroutine("SpawnBoss"); //!!
    }

    private IEnumerator SpawnEnemy()
    {
        while (enemyCount < 10)
        {
            GameObject clone = Instantiate(enemyPrefab);     // 적 오브젝트 생성
            Monster monster = clone.GetComponent<Monster>(); // 방금 생성된 적의 Enemy 컴포넌트

            monster.Setup(wayPoints);                          // wayPoint 정보를 매개변수로 Setup() 호출

            enemyCount++;                                      // 생성된 적 개수 증가

            yield return new WaitForSeconds(spawnerTime);      // spawnTime 시간 동안 대기
        }
    }

    private IEnumerator SpawnBoss() // 보스가 생성 되고 생성이 되었다면 한번만생성하게 됨!!
    {
        yield return new WaitForSeconds(bossSpawnDelay);

        GameObject bossClone = Instantiate(bossPrefab);
        BossMonster boss = bossClone.GetComponent<BossMonster>();
        boss.Setup(wayPoints); 

        bossSpawned = true;       
        
    }

}
