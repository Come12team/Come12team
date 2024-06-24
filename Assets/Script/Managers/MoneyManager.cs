using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public int money = 0; // 현재 돈
    public Text moneyText;

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

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: " + money.ToString();
        }
    }

    private void CheckQuestCompletion()
    {
        if (money >= 500)
        {
            QuestManager.Instance.CompleteQuest("골드 획득");
        }
    }
}