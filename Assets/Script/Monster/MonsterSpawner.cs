using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    private TMP_Text waveUIText; // Wave UI 텍스트
    [SerializeField]
    private Transform[] wayPoints; // 현재 스테이지의 이동 경로
    [SerializeField]
    private GameObject stageClearPN; 
    [SerializeField]
    private Button goButn; 

    private bool bossSpawned = false; // 보스가 생성되었는지 !!
    private int enemyCount = 0; // 생성된 적의 개수
    private int waveCount = 0; // 현재 웨이브 번호
    private int enemiesPerWave = 10; // 웨이브당 생성할 적의 기본 수
    private int totalEnemiesToSpawn = 10; // 현재 웨이브에서 생성할 총 적의 수
    private List<Monster> currentEnemies = new List<Monster>(); // 현재 웨이브에서 생성된 적의 리스트

    private void Awake()
    {
        StartCoroutine("StartNextWave");
    }
    private IEnumerator StartNextWave()
    {
        while (waveCount < 10)
        {
            waveCount++;
            enemyCount = 0;
            bossSpawned = false;
            totalEnemiesToSpawn = waveCount * enemiesPerWave;

            // 적 생성 시작
            StartCoroutine("SpawnEnemy");

            // 적이 모두 사망할 때까지 대기
            yield return new WaitUntil(() => currentEnemies.Count == 0);

            // Stage Clear UI 활성화
            stageClearPN.SetActive(true);

            // Go 버튼 클릭을 기다림
            yield return new WaitUntil(() => goButnClicked);

            // Go 버튼 클릭 후 Stage Clear UI 비활성화
            stageClearPN.SetActive(false);

            // 웨이브 UI 텍스트 업데이트 및 활성화
            waveUIText.text = "Wave " + waveCount;
            waveUIText.gameObject.SetActive(true);

            // 잠시 대기 후 텍스트 비활성화
            yield return new WaitForSeconds(3f);
            waveUIText.gameObject.SetActive(false);
        }

        // 모든 웨이브가 끝났을 때
        Debug.Log("All waves completed!");
    }

    private IEnumerator SpawnEnemy()
    {
        while (enemyCount < totalEnemiesToSpawn)
        {
            GameObject clone = Instantiate(enemyPrefab); // 적 오브젝트 생성
            Monster monster = clone.GetComponent<Monster>(); // 방금 생성된 적의 Monster 컴포넌트

            monster.Setup(wayPoints); // wayPoint 정보를 매개변수로 Setup() 호출
            monster.OnDeath += HandleEnemyDeath; // 적 사망 이벤트 핸들러 추가

            currentEnemies.Add(monster); // 생성된 적을 리스트에 추가
            enemyCount++; // 생성된 적 개수 증가

            yield return new WaitForSeconds(spawnerTime); // spawnerTime 시간 동안 대기
        }

        // 적이 모두 생성된 후 일정 시간 후 보스 생성
        if (!bossSpawned)
        {
            yield return new WaitForSeconds(bossSpawnDelay);

            GameObject bossClone = Instantiate(bossPrefab); // 보스 오브젝트 생성
            BossMonster boss = bossClone.GetComponent<BossMonster>(); // 방금 생성된 보스의 BossMonster 컴포넌트
            boss.Setup(wayPoints);

            bossSpawned = true;
        }
    }
    private void HandleEnemyDeath(Monster monster)
    {
        currentEnemies.Remove(monster);
    }

    private bool goButnClicked = false;

    private void OnGoButtonClick()
    {
        goButnClicked = true;
    }
}
