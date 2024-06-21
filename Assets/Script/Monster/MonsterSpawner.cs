using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; // �� ������
    [SerializeField]
    private float spwnTime; // �� ���� �ֱ�
    [SerializeField]
    private Transform[] wayPoints; // ���� ���������� �̵� ���

    private void Awake()
    {
        //�� ���� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine("SpawnEnemy");
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

}
