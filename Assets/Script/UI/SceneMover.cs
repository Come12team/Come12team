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
