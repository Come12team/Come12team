using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string QuestName { get; private set; }
    public string CompletionCondition { get; private set; }
    public int Reward { get; private set; }
    public bool IsCompleted { get; private set; }

    public Quest(string questName, string completionCondition, int reward)
    {
        QuestName = questName;
        CompletionCondition = completionCondition;
        Reward = reward;
        IsCompleted = false;
    }

    public void CompleteQuest()
    {
        IsCompleted = true;
        MoneyManager.Instance.AddMoney(Reward);
        Debug.Log($"{QuestName} ����Ʈ �Ϸ�! Reward: {Reward} gold.");
    }
}
