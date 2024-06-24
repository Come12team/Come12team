using System.Collections;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class BossMonster : MonoBehaviour
{
    private int wayPointCount;
    private Transform[] wayPoints;
    private int currentIndex = 0;
    private MonsterManager monsterManager;
    //private Player player; // 플레이어(소환수)?
    //private System.Random random = new System.Random();
    public int health = 100;
    public float timeLimit = 30f; // 30초제한

    private void Start()
    {
        StartCoroutine(StartTimer()); // 타이머
        //StartCoroutine(IncreaseHealthPeriodically());
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

    private IEnumerator OnMove()
    {
        NextMoveTo();

        while (true)
        {
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * monsterManager.MoveSpeed)
            {
                NextMoveTo();
            }

            yield return null;
        }
    }

    private void NextMoveTo()
    {
        if (currentIndex < wayPointCount - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            monsterManager.MoveTo(direction);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        //if (random.NextDouble() <= 0.03) //3퍼센트확률로 공격무효
        //{
        //    Debug.Log("Attack blocked!");
        //    return;
        //}
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        //if (random.NextDouble() <= 0.05)
        //{
        //    Debug.Log("소환사 플레이어 공격속도?");
        //    player.공격속도 ? (0.4f, 3.0f); // 40% 느려지게 공격속도가
        //}
    }

    private void Die()
    {
        StopAllCoroutines(); // 모든 코루틴 중지
        Destroy(gameObject);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeLimit); // 30초 기달
        if (health > 0)
        {
            //GameOver(); //오버 처리
        }
    }

    //private void StunPlayer(float duration) //플레이어?스턴적용하게
    //{
    //    if (player != null)
    //    {
    //        player.Stun(duration);
    //    }
    //}


    //private void AttackPlayer()
    //{
    //    //1초간 스턴
    //    StunPlayer(1.0f);
    //}

    //private IEnumerator IncreaseHealthPeriodically() //보스몬스터가 10초마다 체력20회복
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(10f);
    //        health += 20;
    //    }
    //}

    //private void GameOver()
    //{
    //    SceneManager.LoadScene("Lobby"); // 로비 이동
    //}
}