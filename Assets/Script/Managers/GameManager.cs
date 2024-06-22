using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int playerScore;
    public int playerHealth;

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

    private void EndGame()
    {
        Debug.Log("Game Over!");
        // 게임 종료 UI 활성화
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
        // 추가적인 게임 종료 로직
    }
}