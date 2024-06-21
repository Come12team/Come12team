using System.Collections;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class BossMonster : MonoBehaviour
{
    private int wayPointCount;
    private Transform[] wayPoints;
    private int currentIndex = 0;
    private MonsterManager monsterManager;

    //public int health = 100;
    //public float timeLimit = 30f; // 30초제한

    private void Start()
    {
        //StartCoroutine(StartTimer()); // 타이머 ㄱㄱ
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

    //public void TakeDamage(int damage)
    //{
        //health -= damage;
        //if (health <= 0)
        //{
            //Die();
        //}
    //}

    //private void Die()
    //{
        // StopAllCoroutines(); // 모든 코루틴 중지
        //Destroy(gameObject);
    //}

    //private IEnumerator StartTimer()
    //{
        //yield return new WaitForSeconds(timeLimit); // 30초 기달
        //if (health > 0)
        //{
            //GameOver(); // 게임 오버 처리
        //}
    //}

   // private void GameOver()
    //{
        //SceneManager.LoadScene("Lobby"); // 로비 이동
    //}
}
