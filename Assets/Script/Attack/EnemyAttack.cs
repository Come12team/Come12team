using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public enum EnemyType
{
    BossMonster,
    Monster
}
public class EnemyAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // 투사체 프리팹
    public Transform firePoint; // 투사체 생성 위치
    private Transform target; // 공격 대상
    private bool isAttacking; // 공격 중 여부
    private CharacterData characterData;
    private int attackDamage; // 공격력
    private float attackInterval; // 공격 주기

    public interface IDamageable
    {
        void TakeDamage(int damage);
    }

    public void SetCharacterData(CharacterData characterData)
    {
        attackDamage = characterData.attackPower;
        attackInterval = characterData.attackSpeed;
    }

    public void SetTarget(Transform target, EnemyType targetType)
    {
        this.target = target; // 공격 대상을 설정
    }

    public void StartAttacking()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            InvokeRepeating(nameof(AttackTarget), 0f, attackInterval);
        }
    }

    public void StopAttacking()
    {
        if (isAttacking)
        {
            isAttacking = false;
            CancelInvoke(nameof(AttackTarget));
        }
    }

    private void AttackTarget()
    {
        if (target != null)
        {
            Debug.Log($"Enemy attacked target: {target.name}, Damage: {attackDamage}");

            // IDamageable 인터페이스를 사용하여 피해를 줍니다.
            var damageable = target.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }
            else
            {
                Debug.Log("Target is not damageable.");
            }
        }
    }
}
