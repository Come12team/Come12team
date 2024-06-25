using System.Collections;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    private int wayPointCount;
    private Transform[] wayPoints;
    private int currentIndex = 0;
    private MonsterManager monsterManager;
    private System.Random random = new System.Random();
    public int health = 100;
    private float moveThreshold = 0.02f;
    public float timeLimit = 30f; // 30초 제한
    public string enemyType = "BossMonster";

    private EnemyAttack enemyAttack;
    private void Start()
    {
        StartCoroutine(StartTimer()); // 타이머 시작
        SetupAttack(); // 공격 설정 초기화
        // StartCoroutine(IncreaseHealthPeriodically()); // 체력회복
    }

    public void Setup(Transform[] wayPoints)
    {
        monsterManager = GetComponent<MonsterManager>();
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;
        transform.position = wayPoints[currentIndex].position;
        StartCoroutine(OnMove());
    }

    private void SetupAttack()
    {
        if (enemyAttack == null)
        {
            enemyAttack = gameObject.AddComponent<EnemyAttack>();
        }

        CharacterData bossData = ScriptableObject.CreateInstance<CharacterData>();
        bossData.attackPower = 30; 
        bossData.attackSpeed = 1f;
        enemyAttack.SetCharacterData(bossData);

        enemyAttack.SetTarget(transform, EnemyType.BossMonster);

        enemyAttack.StartAttacking();
    }

    private IEnumerator OnMove()
    {
        NextMoveTo(); // 다음 이동 방향 설정

        while (true)
        {
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < moveThreshold * monsterManager.MoveSpeed)
            {
                NextMoveTo(); // 다음 이동 방향 설정
            }

            yield return null;
        }
    }

    private void NextMoveTo()
    {
        Vector3 targetPosition = wayPoints[currentIndex].position;
        if (Vector3.Distance(transform.position, targetPosition) < moveThreshold * monsterManager.MoveSpeed)
        {
            currentIndex = (currentIndex + 1) % wayPoints.Length;
            targetPosition = wayPoints[currentIndex].position;
            monsterManager.MoveTo((targetPosition - transform.position).normalized);
        }

        // 방향에 따라 회전을 적용하여 좌우 반전 구현
        if (targetPosition.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // 기본 방향 (오른쪽을 보도록 회전)
        }
        else if (targetPosition.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // 좌우 반전 (왼쪽을 보도록 회전)
        }
    }

    public void TakeDamage(int damage)
    {
        if (random.NextDouble() <= 0.03) // 50퍼센트 확률로 공격 무효
        {
            Debug.Log("Attack blocked!");
            return;
        }
        health -= damage;
        Debug.Log($"Boss is under attack! Took {damage} damage. Current health: {health}");
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Boss has died.");
        StopAllCoroutines(); // 모든 코루틴 중지
        RewardManager.Instance.GiveReward(enemyType);
        MoneyManager.Instance.AddMonstersDefeated(1);
        Destroy(gameObject);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeLimit); // 타이머 기다림
        if (health > 0)
        {

        }
    }
}
