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

    void OnMouseDown()
    {
        // 유닛 클릭 시 합성 UI를 띄우는 로직 호출
        FindObjectOfType<CharacterManager>().ShowFusionUI(this);
    }
}