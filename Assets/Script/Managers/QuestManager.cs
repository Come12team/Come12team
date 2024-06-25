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
            DontDestroyOnLoad(gameObject); // 다른 씬에서도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
        InitializeQuests();
    }

    private void InitializeQuests()
    {
        quests.Add(new Quest("골드 획득", "골드 500골드 모으기", 500));
        quests.Add(new Quest("몬스터 처치", "몬스터 10마리 처치", 500));
        quests.Add(new Quest("소환 10회 진행", "캐릭터 10회 소환", 100));
        quests.Add(new Quest("강화 10회 진행", "캐릭터 10회 강화", 100));
    }

    public void AddQuest(Quest quest)
    {
        quests.Add(quest);
        Debug.Log($"Quest added: {quest.QuestName}");
    }
    public List<Quest> GetQuests()
    {
        return quests;
    }

    public void StartQuest(string questName)
    {
        Quest quest = quests.Find(q => q.QuestName == questName);
        if (quest != null && !quest.IsStarted)
        {
            quest.StartQuest();
        }
    }

    public void CompleteQuest(string questName)
    {
        Quest quest = quests.Find(q => q.QuestName == questName);
        if (quest != null && !quest.IsCompleted)
        {
            quest.CompleteQuest();
        }
    }

}
