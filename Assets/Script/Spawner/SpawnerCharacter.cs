using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCharacter : MonoBehaviour
{
    public string Name { get; private set; }
    public CharacterGrade Grade { get; private set; }
    public Sprite Image { get; private set; } // 캐릭터 이미지

    public SpawnerCharacter(string name, CharacterGrade grade, Sprite image)
    {
        Name = name;
        Grade = grade;
        Image = image;
    }
}
