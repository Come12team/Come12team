using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName; // 유닛 이름
    public CharacterGrade grade; // 유닛 등급
    public Sprite characterSprite; // 유닛 이미지
    public int attackPower; //유닛 공격력
    public float attackSpeed; //유닛 공격속도
}

public enum CharacterGrade
{
    Normal,
    Magic,
    Hero,
    Legendary,
    Mythic
}