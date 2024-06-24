using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    [SerializeField] private GameObject StopPanel;

    // 게임 시작 버튼
    public void GameStart()
    {
        GameManager.Instance.GameStart();
    }

    // 게임 종료 버튼
    public void EndGame()
    {
        GameManager.Instance.EndGame();
    }

    // 일시정지 버튼
    public void GamePauseOrPlay()
    {
        if (StopPanel == null) return;
        if (!StopPanel.activeSelf)
        {
            GameManager.Instance.GamePause();
            OpenPanel(StopPanel);
        }
        else
        {
            GameManager.Instance.GamePlay();
            ClosePanel(StopPanel);
        }
    }

    // 판넬 활성화 (panel = 활성화할 판넬 오브젝트)
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}