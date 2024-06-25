using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private int wayPointCount; //이동 경로 개수
    private Transform[] wayPoints; // 이동 경로 정보
    private int currentIndex = 0; //현재 목표지점 인덱스
    private MonsterManager monsterManager; // 오브젝트 이동 제어 
    private Animator animator; // Animator 컴포넌트에 대한 참조
    private int health = 100; // 몬스터 체력
    private float moveThreshold = 0.02f; // 이동 거리 값
    public string enemyType = "Monster";

    public delegate void DeathDelegate(Monster monster);
    public event DeathDelegate OnDeath;

    public void Setup(Transform[] wayPoints)
    {
        monsterManager = GetComponent<MonsterManager>();

        // 적 이동 경로  WayPoints 정보 설정
        wayPointCount = wayPoints.Length;

        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        // 적의 위치를 첫번째 wayPoint 위치로 설정
        transform.position = wayPoints[currentIndex].position;

        //적 이동/ 목표 지점 설정 코루틴 함수 시작
        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        //다음 이동 방향 설정 
        NextMoveTo();

        while(true)
        {

            // 적의 현재 위치와 목표위치의 거리가 0.02 * movement2D.MoveSpeed보다 작을 때 if 조건문 실행
            // Tip.movement2D.MoveSpeed를 곱해주는 이유는 속도가 빠르면 한 프레임에 0.02보다 크게 움직이기 때문에
            // if 조건문에 걸리지 않고 경로를 탈주하는 오브젝트가 발생할 수 있다.
            if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * monsterManager.MoveSpeed)
            {
                //다음 이동 방향 설정
                NextMoveTo();
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
        health -= damage;

        // 히트 애니메이션 트리거 설정
        if(animator) 
            animator.SetTrigger("Hit");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // 몬스터 사망 처리 (예: 오브젝트 제거, 사망 애니메이션 등)
      //  RewardManager.Instance.GiveReward(enemyType);
      //  MoneyManager.Instance.AddMonstersDefeated(1);
        Destroy(gameObject);
    }

}
