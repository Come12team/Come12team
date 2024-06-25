using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMover : MonoBehaviour
{
    public Button playButton;
    public Button stageButton;
    public Button exitButton;
    public Button lobbyButton;
    public Button backButton;
    public Button backButton2;

    // Start is called before the first frame update
    void Start()
    {
        if(playButton != null)
        {
            playButton.onClick.AddListener(OnPlayButtonClick);
        }

        if(stageButton != null)
        {
            stageButton.onClick.AddListener(OnStageButtonClick);
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitButtonClick);
        }

        if (lobbyButton != null)
        {
            lobbyButton.onClick.AddListener(OnLobbyButtonClick);
        }
        // 스테이지 클리어 성공 시 돌아가기 Button
        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackButtonClick);
        }
        // 스테이지 클리어 실패 시 돌아가기 Button
        if (backButton2 != null)
        {
            backButton2.onClick.AddListener(OnBackButtonClick);
        }
    }

    void OnPlayButtonClick()
    {
        SceneManager.LoadScene("LobbyScene_UITest");
    }

    void OnStageButtonClick()
    {
        SceneManager.LoadScene("PlayScene_UITest");
    }

    void OnLobbyButtonClick()
    {
        SceneManager.LoadScene("LobbyScene_UITest");
    }

    void OnBackButtonClick()
    {
        SceneManager.LoadScene("LobbyScene_UITest");
    }

    // 게임 종료
    public void OnExitButtonClick()
    {
        // 구동 환경이 유니티 에디터일 경우
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        // 구동 환경이 응용프로그램일 경우
#else
            Application.Quit();
#endif
    }
}
