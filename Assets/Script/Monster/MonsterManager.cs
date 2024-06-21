using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f; // �̵� �ӵ��� �����ϴ� ����
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero; // �̵� ������ �����ϴ� ����

    public float MoveSpeed => moveSpeed; // moveSpeed ������ ������Ƽ (�б� ����)

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime; // �� �����Ӹ��� ��ü�� �̵� ����� �ӵ��� ���� �̵���Ŵ
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction; // �̵� ������ �����ϴ� �޼���
    }
}
