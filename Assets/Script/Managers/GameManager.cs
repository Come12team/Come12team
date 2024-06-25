using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int playerScore;
    public int playerHealth;

    // 플레이 중 여부

    // 게임 종료 UI 캔버스
    public GameObject gameOverCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        playerScore = 0;
        playerHealth = 100;

        // 게임 종료 UI 비활성화
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    public void AddScore(int amount)
    {
        playerScore += amount;
        Debug.Log("Score: " + playerScore);
    }

    public void ReduceHealth(int amount)
    {
        playerHealth -= amount;
        Debug.Log("Health: " + playerHealth);
        if (playerHealth <= 0)
        {
            EndGame();
        }
    }

    // 게임 시작
    public void GameStart()
    {
        Time.timeScale = 1f;
    }

    // 게임 종료
    public void EndGame()
    {
        Debug.Log("Game Over!");
        // 게임 종료 UI 활성화
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
        // 추가적인 게임 종료 로직
    }

    // 게임 일시정지
    public void GamePause()
    {
        Time.timeScale = 0f;
    }

    // 게임 재개
    public void GamePlay()
    {
        Time.timeScale = 1f;
    }

    public void OnWaveComplete()
    {
        RewardManager.Instance.GiveWaveCompletionReward();
        // 웨이브가 완료될 때 RewardManager에게 웨이브 완료 보상을 주도록 호출
    }

}