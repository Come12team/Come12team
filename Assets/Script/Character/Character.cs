 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterData characterData; // 캐릭터 데이터

    public void InitializeCharacter()
    {
        name = characterData.characterName;
        // 기타 초기화 로직
    }

    // 캐릭터를 클릭했을 때 호출되는 메서드
    public void OnMouseDown()
    {
        FindObjectOfType<FusionManager>().OnCharacterClicked(this);
    }
}