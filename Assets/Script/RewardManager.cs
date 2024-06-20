using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance { get; private set; }

    private Player player;
    private Dictionary<string, int> rewards;
    private int baseStageCompletionReward = 100;  // 기본 스테이지 클리어 보상
    private int currentStage = 1;                 // 현재 스테이지

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

        // 보상 초기화
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
