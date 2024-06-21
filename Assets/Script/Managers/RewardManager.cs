using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance { get; private set; }

    private Dictionary<string, int> rewards;
    private int WaveCompletionReward = 100;  // 웨이브 클리어 보상
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
            { "NormalEnemy", 50 }, //잡몹 
            { "BossEnemy", 100 }   //보스
        };
    }

    public void GiveReward(string enemyType)
    {
        if (rewards.ContainsKey(enemyType))
        {
            MoneyManager.Instance.AddMoney(rewards[enemyType]);
        }
        else
        {
            Debug.LogWarning("Unknown enemy type: " + enemyType);
        }
    }

    public void GiveWaveCompletionReward()
    {
        int waveCompletionPoints = WaveCompletionReward * currentStage;
        MoneyManager.Instance.AddMoney(waveCompletionPoints);
        currentStage++;
    }
}

/* public string enemyType;  // 적의 종류 ("NormalEnemy" 또는 "BossEnemy") // 몬스터매니저에 추가
 * .
 * .
 * .
 * RewardManager.Instance.GiveReward(enemyType); // 몬스터매니저에 몬스터 죽는로직에 추가
 * 
 * 
 * 
 * public void OnWaveComplete()
    {
        RewardManager.Instance.GiveWaveCompletionReward();  // 게임매니저에 추가
    }
*/