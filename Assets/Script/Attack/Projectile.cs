using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    private Transform target;
    public GameObject impactEffectPrefab; // 충돌 효과 프리팹

    public void Initialize(Transform target, int damage)
    {
        this.target = target;
        this.damage = damage;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            // 타겟에 도달했을 때
            BossMonster bossMonster = target.GetComponent<BossMonster>();
            if (bossMonster != null)
            {
                bossMonster.TakeDamage(damage);
            }

            if (impactEffectPrefab != null)
            {
                Instantiate(impactEffectPrefab, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
