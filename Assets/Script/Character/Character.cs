using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    
public class Character : MonoBehaviour
{
    public CharacterData characterData; // 캐릭터 데이터
    private float AttackTimer = 0; // 공격 타이머
    private Coroutine m_LifeCoroutine = null; // 캐릭터 생존 코루틴
 
    public void InitializeCharacter()
    {
        name = characterData.characterName;
        // 기타 초기화 로직
    }

    //  탐지 범위 내에 가장 가까운 적을 탐지


    // 캐릭터를 클릭했을 때 호출되는 메서드
    public void OnMouseDown()
    {
        FindObjectOfType<FusionManager>().OnCharacterClicked(this);
    }

    private void OnEnable()
    {
        m_LifeCoroutine = StartCoroutine(IsOnLife());
    }

    [SerializeField] private float m_fDetectRange = 2;
    private const string EnemyTag = "Enemy";
    private const string BossTag = "Boss";
    
    private GameObject DetectEnemy()
    {
        // 탐지 범위 내에 가장 가까운 적을 탐지
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, m_fDetectRange);
        float CloseDistance = 0;
        GameObject CloseEnemy = null;
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag(EnemyTag) || collider.gameObject.CompareTag(BossTag))
            {
                if(CloseDistance == 0)
                {
                    CloseDistance = Vector3.Distance(transform.position, collider.transform.position);
                    CloseEnemy = collider.gameObject;
                }
                else
                {
                    if (CloseDistance > Vector3.Distance(transform.position, collider.transform.position))
                    {
                        CloseDistance = Vector3.Distance(transform.position, collider.transform.position);
                        CloseEnemy = collider.gameObject;
                    }
                }
            }
        }
        
        if(CloseEnemy != null)
        {
            return CloseEnemy;
        }
        
        return null;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_fDetectRange);
    }


    IEnumerator IsOnLife()
    {
        while (true)
        {
            AttackTimer += Time.deltaTime;
            if (AttackTimer >= characterData.attackSpeed)
            {
                if (PoolingManager.instance)
                {
                    GameObject enemy = DetectEnemy();
                    if (enemy != null)
                    {
                        PoolingManager.instance.CreateProjectile(gameObject.transform.position, enemy,
                            characterData.attackPower);
                        AttackTimer = 0;
                    }
                }
            }
            
            
            yield return null;
        }
    }
    
}