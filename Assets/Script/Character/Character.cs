using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterData characterData; // 캐릭터 데이터
    public int level;
    private CharacterManager characterManager;

    void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        InitializeCharacter();
    }

    public void InitializeCharacter()
    {
        name = characterData.characterName;
        // 기타 초기화 로직
        // 예: 공격력과 공격 속도를 초기화
    }

    public void Upgrade()
    {
        level++;
        characterData.attackPower += 10; // 레벨업 시 공격력 증가
        characterData.attackSpeed += 0.1f; // 레벨업 시 공격 속도 증가
    }

    public void OnFusionCheck()
    {
        characterManager.CheckForFusion(this);
    }
}