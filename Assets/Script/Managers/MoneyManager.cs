using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public int money = 50; // 현재 돈
    public int diamonds = 0;  // 현재 다이아몬드
    public Text moneyText;
    public Text diamondsText;
    private int monstersDefeated = 0;
    private int charactersSummoned = 0;
    private int charactersEnhanced = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 다른 씬에서도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        UpdateMoneyUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
        CheckQuestCompletion();
        Debug.Log("Money: " + money);
    }

    public void PayMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateMoneyUI();
            Debug.Log("Money: " + money);
        }
        else
        {
            Debug.LogWarning("돈이 부족합니다.");
        }
    }
    public int GetCurrentMoney()
    {
        return money;
    }

    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        UpdateDiamondsUI();
        Debug.Log("Diamonds: " + diamonds);
    }

    public bool PayDiamonds(int amount)
    {
        bool success = false; // 성공 여부를 저장할 변수를 초기화합니다.

        if (diamonds >= amount)
        {
            diamonds -= amount;
            UpdateDiamondsUI();
            Debug.Log("Diamonds: " + diamonds);
            success = true; // 다이아몬드 지불 성공
        }
        else
        {
            Debug.LogWarning("돈이 부족합니다.");
            success = false; // 다이아몬드 지불 실패
        }
        return success; // 성공 여부를 반환합니다.
    }

    public void AddMonstersDefeated(int count)
    {
        monstersDefeated += count;
        CheckQuestCompletion();
    }

    public void AddCharactersSummoned(int count)
    {
        charactersSummoned += count;
        CheckQuestCompletion();
    }

    public void AddCharactersEnhanced(int count)
    {
        charactersEnhanced += count;
        CheckQuestCompletion();
    }

    private void UpdateDiamondsUI()
    {
        if (diamondsText != null)
        {
            diamondsText.text = "Diamonds: " + diamonds.ToString();
        }
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: " + money.ToString();
        }
    }

    public void CheckQuestCompletion()
    {
        if (money >= 500)
        {
            QuestManager.Instance.CompleteQuest("골드 획득");
            RewardManager.Instance.GiveReward("QuestGold"); // 퀘스트 완료 보상 주기
        }
        if (monstersDefeated >= 10)
        {
            QuestManager.Instance.CompleteQuest("몬스터 처치");
            RewardManager.Instance.GiveReward("QuestGold"); // 퀘스트 완료 보상 주기
        }
        if (charactersSummoned >= 10)
        {
            QuestManager.Instance.CompleteQuest("소환 10회 진행");
            RewardManager.Instance.GiveReward("QuestGold"); // 퀘스트 완료 보상 주기
        }
        if (charactersEnhanced >= 10)
        {
            QuestManager.Instance.CompleteQuest("강화 10회 진행");
            RewardManager.Instance.GiveReward("QuestGold"); // 퀘스트 완료 보상 주기
        }
    }
}