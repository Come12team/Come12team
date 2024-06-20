using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance { get; private set; }

    private Player player;
    private Dictionary<string, int> rewards;
    private int baseStageCompletionReward = 100;  // �⺻ �������� Ŭ���� ����
    private int currentStage = 1;                 // ���� ��������

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

        // ���� �ʱ�ȭ
        rewards = new Dictionary<string, int>
        {
            { "NormalEnemy", 50 },
            { "BossEnemy", 100 }
        };
    }

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    public void GiveReward(string enemyType)
    {
        if (rewards.ContainsKey(enemyType))
        {
            player.AddScore(rewards[enemyType]);
        }
        else
        {
            Debug.LogWarning("Unknown enemy type: " + enemyType);
        }
    }

    public void GiveStageCompletionReward()
    {
        int stageCompletionPoints = baseStageCompletionReward * currentStage;
        player.AddScore(stageCompletionPoints);
        currentStage++;
    }
}
