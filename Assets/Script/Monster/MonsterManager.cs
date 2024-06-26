using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f; // 이동 속도를 저장하는 변수
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero; // 이동 방향을 저장하는 변수

    public float MoveSpeed => moveSpeed; // moveSpeed 변수의 프로퍼티 (읽기 전용)

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime; // 매 프레임마다 객체를 이동 방향과 속도에 맞춰 이동시킴
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction; // 이동 방향을 설정하는 메서드

    }
}
