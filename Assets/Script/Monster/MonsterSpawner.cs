using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; // 적 프리팹
    [SerializeField]
    private float spwnTime; // 적 생성 주기
    [SerializeField]
    private Transform[] wayPoints; // 현재 스테이지의 이동 경로

    private void Awake()
    {
        //적 생성 코루틴 함수 호출
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject clone = Instantiate(enemyPrefab);     // 적 오브젝트 생성
            Monster monster = clone.GetComponent<Monster>(); // 방금 생성된 적의 Enemy 컴포넌트

            monster.Setup(wayPoints);                          // wayPoint 정보를 매개변수로 Setup() 호출

            yield return new WaitForSeconds(spwnTime);      // spawnTime 시간 동안 대기
        }
    }

}
