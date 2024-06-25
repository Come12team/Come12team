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
            // 공격 대상이 존재하는 경우에만 공격
            Debug.Log($"Enemy attacked target: {target.name}, Damage: {attackDamage}");

            // 보스 몬스터 또는 다른 타겟에게 데미지를 줄 수 있는 메서드를 호출
            var bossMonster = target.GetComponent<BossMonster>();
            if (bossMonster != null)
            {
                bossMonster.TakeDamage(attackDamage);
            }
            // 다른 몬스터나 플레이어에 대해서도 비슷하게 처리할 수 있습니다.
        }
    }
}
