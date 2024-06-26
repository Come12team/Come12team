using UnityEngine;
using System.Collections.Generic;
public class EnhancementManager : MonoBehaviour
{
    public static EnhancementManager Instance { get; private set; } // 싱글톤 인스턴스
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.root.gameObject); // 최상위 루트 게임 오브젝트에 적용
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int enhancementCost = 50;  // 기본 강화 비용
    // 개별 캐릭터 강화 메서드
    public void EnhanceCharacter(Character character)
    {
        if (MoneyManager.Instance.PayMoney(enhancementCost))
        {
            character.characterData.attackPower += 5;  // 공격력 증가량
            character.characterData.attackSpeed += 0.1f;  // 공격속도 증가량
            enhancementCost += 50;  // 강화 비용 증가량
        }
        else
        {
            Debug.Log("Not enough gold to enhance the character.");
        }
    }
}