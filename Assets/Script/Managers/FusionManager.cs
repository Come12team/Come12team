using UnityEngine;
using UnityEngine.UI;

public class FusionManager : MonoBehaviour
{
    public CharacterManager characterManager;
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

    // 캐릭터를 클릭했을 때 호출되는 메서드
    public void OnCharacterClicked(Character character)
    {
        // 선택된 캐릭터 설정
        selectedCharacter = character;

        // 캐릭터 이미지 설정
        //characterImage.sprite = character.characterData.characterSprite;

        // 캐릭터 합성 가능 여부 텍스트 설정
        int characterCount = characterManager.CountCharacters(character.characterData);
        fusionText.text = $"{character.characterData.characterName} 합성 가능 ({characterCount}/{charactersNeededForFusion})";

        // 합성 UI 패널 활성화
        //fusionPanel.transform.position = selectedCharacter.transform.position;
        fusionPanel.SetActive(true);

        // 합성 버튼 활성화 여부 설정
        fusionButton.interactable = characterCount >= charactersNeededForFusion;
    }

    public void OnFusionButtonClicked()
    {
        // 합성 로직을 처리
        if (selectedCharacter != null)
        {
            characterManager.CheckForFusion(selectedCharacter);
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