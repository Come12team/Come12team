using System.Collections;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class BossMonster : MonoBehaviour
{
    private int wayPointCount;
    private Transform[] wayPoints;
    private int currentIndex = 0;
    private MonsterManager monsterManager;
    //private Player player; // �÷��̾�(��ȯ��)?
    //private System.Random random = new System.Random();
    public int health = 100;
    public float timeLimit = 30f; // 30������

    private void Start()
    {
        StartCoroutine(StartTimer()); // Ÿ�̸�
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
        //if (random.NextDouble() <= 0.03) //3�ۼ�ƮȮ���� ���ݹ�ȿ
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
        //    Debug.Log("��ȯ�� �÷��̾� ���ݼӵ�?");
        //    player.���ݼӵ�?(0.4f, 3.0f); // 40% �������� ���ݼӵ���
        //}
    }

    private void Die()
    {
        StopAllCoroutines(); // ��� �ڷ�ƾ ����
        Destroy(gameObject);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeLimit); // 30�� ���
        if (health > 0)
        {
            //GameOver(); // ���� ���� ó��
        }
    }

    //private void StunPlayer(float duration) //�÷��̾�?���������ϰ�
    //{
    //    if (player != null)
    //    {
    //        player.Stun(duration);
    //    }
    //}

    
    //private void AttackPlayer()
    //{
    //    //1�ʰ� ����
    //    StunPlayer(1.0f);
    //}

    //private IEnumerator IncreaseHealthPeriodically() //�������Ͱ� 10�ʸ��� ü��20ȸ��
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(10f);
    //        health += 20;
    //    }
    //}

    //private void GameOver()
    //{
    //    SceneManager.LoadScene("Lobby"); // �κ� �̵�
    //}
}
