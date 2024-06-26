using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerUIManager : MonoBehaviour
{
    public Image SpawnerResultImage;
    public Text SpawnerResultText;

    public void ShowSpawnerResult(CharacterData character)
    {
        if (SpawnerResultImage != null && SpawnerResultText != null && character != null)
        {
            SpawnerResultImage.sprite = character.characterSprite; // 이미지 설정
            SpawnerResultText.text = $"{character.grade} character: {character.characterName} 획득!"; // 텍스트 설정
        }
    }
}
