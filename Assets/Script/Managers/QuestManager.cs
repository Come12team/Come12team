using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    private List<Quest> quests = new List<Quest>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �ٸ� �������� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddQuest(Quest quest)
    {
        quests.Add(quest);
        Debug.Log($"Quest added: {quest.QuestName}");
    }

    public void CompleteQuest(string questName)
    {
        Quest quest = quests.Find(q => q.QuestName == questName);
        if (quest != null && !quest.IsCompleted)
        {
            quest.CompleteQuest();
        }
        else
        {
            Debug.LogWarning("Quest not found or already completed: " + questName);
        }
    }

    public List<Quest> GetQuests()
    {
        return quests;
    }
}
