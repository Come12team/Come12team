using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FusionManager : MonoBehaviour
{
    public GameObject fusionPanel; // 합성 UI 패널
    public Image characterImage; // 캐릭터 이미지
    public Text fusionText; // 합성 관련 텍스트
    public Button fusionButton; // 합성 버튼
    private Character selectedCharacter; // 선택된 캐릭터
    public int charactersNeededForFusion = 3; // 합성에 필요한 캐릭터 수

    void Start()
    {
        // 합성 UI 패널을 비활성화
        fusionPanel.SetActive(false);
    }

    public void ShowFusionUI(Character character)
    {
        selectedCharacter = character;
        characterImage.sprite = character.characterData.characterSprite; // 캐릭터 스프라이트 설정
        int characterCount = FindObjectOfType<CharacterManager>().CountCharacters(character.characterData);
        fusionText.text = $"{character.characterData.characterName} 합성 가능 ({characterCount}/{charactersNeededForFusion})";
        fusionPanel.SetActive(true);

        // 합성 버튼 활성화 여부 설정
        fusionButton.interactable = characterCount >= charactersNeededForFusion;
    }

    public void OnFusionButtonClicked()
    {
        // 합성 로직을 처리
        if (selectedCharacter != null)
        {
            FindObjectOfType<CharacterManager>().CheckForFusion(selectedCharacter);
        }
        // 합성 UI 패널을 비활성화
        fusionPanel.SetActive(false);
    }

    public void OnCancelButtonClicked()
    {
        // 합성 UI 패널을 비활성화
        fusionPanel.SetActive(false);
    }
}