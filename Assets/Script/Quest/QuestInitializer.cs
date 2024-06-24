using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        QuestManager.Instance.AddQuest(new Quest(
            "골드 획득",
            "골드 500골드 모으기",
            500
        ));

        QuestManager.Instance.AddQuest(new Quest(
            "몬스터 처치",
            "몬스터 10마리 처치",
            500
        ));

        QuestManager.Instance.AddQuest(new Quest(
            "소환 10회 진행",
            "캐릭터 10회 소환",
            100
        ));

        QuestManager.Instance.AddQuest(new Quest(
            "강화 10회 진행",
            "캐릭터 10회 강화",
            100
        ));
    }
}
