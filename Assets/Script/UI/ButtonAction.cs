using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    [SerializeField] private GameObject StopPanel;

    // ���� ���� ��ư
    public void GameStart()
    {
        GameManager.Instance.GameStart();
    }

    // ���� ���� ��ư
    public void EndGame()
    {
        GameManager.Instance.EndGame();
    }

    // �Ͻ����� ��ư
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

    // �ǳ� Ȱ��ȭ (panel = Ȱ��ȭ�� �ǳ� ������Ʈ)
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    // ���� ����
    public void QuitGame()
    {
        // ���� ȯ���� ����Ƽ �������� ���
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        // ���� ȯ���� �������α׷��� ���
        #else
            Application.Quit();
        #endif
    }
}