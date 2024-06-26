using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterData;

public class SpawnerCharacter : MonoBehaviour
{
    public string CharacterName { get; set; }
    public CharacterData.CharacterGrade Grade { get; set; }
    public Sprite CharacterSprite { get; set; }

    // 생성자
    public SpawnerCharacter(string characterName, CharacterData.CharacterGrade grade, Sprite characterSprite)
    {
        CharacterName = characterName;
        Grade = grade;
        CharacterSprite = characterSprite;
    }
}
