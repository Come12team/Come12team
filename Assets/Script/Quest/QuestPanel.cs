using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    public GameObject questPrefab; // 퀘스트를 표시할 프리팹
    public Transform[] questLists; // 퀘스트 목록을 표시할 부모 UI 요소 배열

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

        for (int i = 0; i < questLists.Length; i++)
        {
            if (i < quests.Count)
            {
                AssignQuestToPanel(questLists[i], quests[i]);
            }
        }
    }

    void AssignQuestToPanel(Transform panel, Quest quest)
    {
        // 이미 퀘스트가 표시된 경우 추가로 생성하지 않도록 확인
        if (panel.childCount > 0 || quest == null)
        {
            return;
        }

        // 퀘스트를 표시할 UI 요소 생성
        GameObject questObject = Instantiate(questPrefab, panel);
        questObject.transform.localScale = Vector3.one; // 스케일을 원래 크기로 설정

        // 퀘스트 정보를 텍스트 컴포넌트에 설정
        Text questText = questObject.GetComponentInChildren<Text>();
        if (questText != null)
        {
            questText.text = $"{quest.QuestName}\n{quest.CompletionCondition}\nReward: {quest.Reward} gold";
        }

        // 퀘스트 버튼에 클릭 리스너 추가
        Button button = questObject.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Debug.Log("Clicked on quest: " + quest.QuestName);
            // 추가적인 퀘스트 정보 표시나 완료 처리 등을 구현할 수 있음
        });
    }
}
