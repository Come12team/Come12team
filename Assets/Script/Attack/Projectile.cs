using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage; // 적에게 들어갈 데미지
    public float speed = 3; // 투사체 속도
    private GameObject target; // 추적할 대상
    public GameObject impactEffectPrefab; // 충돌 효과 프리팹
    [SerializeField] private bool m_bIsOnLife; // 투사체 생존 여부
    private Coroutine m_LifeCoroutine = null; // 투사체 생존 코루틴
    private const string EnemyTag = "Enemy"; // 적 태그
    private const string BossTag = "Boss"; // 보스 태그
    
    private void OnDisable()
    {
        m_bIsOnLife = false;
        if (m_LifeCoroutine != null)
        {
            StopCoroutine(m_LifeCoroutine);
            m_LifeCoroutine = null;
        }
    }

    public void Initialize(Vector3 Pos, GameObject target, int damage)
    {
        gameObject.transform.position = Pos;
        this.target = target;
        this.damage = damage;
        m_bIsOnLife = true;
        m_LifeCoroutine = null;
        gameObject.SetActive(true);
        m_LifeCoroutine = StartCoroutine(UpdateProjectile());
    }

    IEnumerator UpdateProjectile()
    {
        while (m_bIsOnLife)
        {
            if (target == null)
            {
                m_bIsOnLife = false;
                yield break;
            }
            
            Vector3 direction = (target.transform.position - transform.position).normalized;
            float distance = speed * Time.deltaTime;
            transform.Translate(direction * distance);
            yield return null;
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(EnemyTag) || other.CompareTag(BossTag))
        {
            switch (target.gameObject.tag)
            {
                case EnemyTag:
                    Monster monster = target.GetComponent<Monster>();
                    if (monster != null)
                    {
                        monster.TakeDamage(damage);
                    }

                    break;
                case BossTag:
                    BossMonster bossMonster = target.GetComponent<BossMonster>();
                    if (bossMonster != null)
                    {
                        bossMonster.TakeDamage(damage);
                    }

                    break;
            }
            Hit();
        }
    }


    private void Hit()
    {
        if (impactEffectPrefab != null)
        {
            Instantiate(impactEffectPrefab, transform.position, transform.rotation);
        }
        m_bIsOnLife = false;
        gameObject.SetActive(false);
    }
}
