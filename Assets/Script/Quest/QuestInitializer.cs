using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        QuestManager.Instance.AddQuest(new Quest(
            "��� ȹ��",
            "��� 500��� ������",
            500
        ));

        QuestManager.Instance.AddQuest(new Quest(
            "���� óġ",
            "���� 10���� óġ",
            500
        ));

        QuestManager.Instance.AddQuest(new Quest(
            "��ȯ 10ȸ ����",
            "ĳ���� 10ȸ ��ȯ",
            100
        ));

        QuestManager.Instance.AddQuest(new Quest(
            "��ȭ 10ȸ ����",
            "ĳ���� 10ȸ ��ȭ",
            100
        ));
    }
}
