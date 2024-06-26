using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    public Transform questListPN; // QuestListPN 프리팹
    public GameObject questPrefab; // 퀘스트를 표시할 프리팹
    public Transform[] quest; // Quest 배열

    private bool isInitialized = false; // 초기화 여부를 확인하는 변수

    void Start()
    {
        if (!isInitialized)
        {
            PopulateQuestList();
            isInitialized = true; // 초기화 완료로 설정
        }
    }

    void PopulateQuestList()
    {
        List<Quest> quests = QuestManager.Instance.GetQuests(); // QuestManager에서 퀘스트 목록 가져오기

        // quests 리스트에 있는 퀘스트 수를 최대 4개까지만 반영하도록 제한
        int numQuests = Mathf.Min(quests.Count, 4);

        for (int i = 0; i < numQuests; i++)
        {
            AssignQuestToPanel(quest[i], quests[i]);
        }
    }

    void AssignQuestToPanel(Transform questItem, Quest quest)
    {
        // 퀘스트를 표시할 UI 요소를 찾습니다.
        Text questText = questItem.GetComponentInChildren<Text>();
        if (questText != null)
        {
            questText.text = $"{quest.QuestName}\n{quest.CompletionCondition}\nReward: {quest.Reward} gold";
        }

        // 퀘스트 버튼에 클릭 리스너 추가 (있는 경우에만)
        Button button = questItem.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() =>
            {
                Debug.Log("Clicked on quest: " + quest.QuestName);
            });
        }
        else
        {
            Debug.LogError("Button component not found in quest item!");
        }
    }
}
