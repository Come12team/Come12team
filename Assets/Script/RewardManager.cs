using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance { get; private set; }

    private Dictionary<string, int> rewards;
    private int WaveCompletionReward = 100;  // ���̺� Ŭ���� ����
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
            { "NormalEnemy", 50 }, //��� 
            { "BossEnemy", 100 }   //����
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

/* public string enemyType;  // ���� ���� ("NormalEnemy" �Ǵ� "BossEnemy") // ���͸Ŵ����� �߰�
 * .
 * .
 * .
 * RewardManager.Instance.GiveReward(enemyType); // ���͸Ŵ����� ���� �״·����� �߰�
 * 
 * 
 * 
 * public void OnWaveComplete()
    {
        RewardManager.Instance.GiveWaveCompletionReward();  // ���ӸŴ����� �߰�
    }
*/