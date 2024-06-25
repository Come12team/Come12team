using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance { get; private set; }

    private Dictionary<string, int> rewards;
    private int WaveCompletionReward = 100;  // 웨이브 클리어 보상
    private int currentStage = 1;                 // 현재 스테이지
    public int monstersPerDiamond = 3;       // 다이아몬드를 획득하기 위한 몬스터 처치 수
    public int monstersDefeatedForDiamond = 0; // 몬스터마다 체크 할때마다 증가

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
        // 다이아몬드 조건 체크
        if (monstersDefeatedForDiamond % monstersPerDiamond == 0)
        {
            MoneyManager.Instance.AddDiamonds(1); // 몬스터 일정 수 처치마다 다이아몬드 1개 지급
        }
    }

    public void GiveWaveCompletionReward()
    {
        int waveCompletionPoints = WaveCompletionReward * currentStage;
        MoneyManager.Instance.AddMoney(waveCompletionPoints);
        currentStage++;
    }
}