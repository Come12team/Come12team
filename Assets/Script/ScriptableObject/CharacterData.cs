using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName; // 캐릭터 이름
    public int attackPower; // 공격력
    public float attackSpeed; // 공격 속도

}