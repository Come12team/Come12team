using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerUIManager : MonoBehaviour
{
    public Image SpawnerResultImage;
    public Text SpawnerResultText;

    public void ShowSpawnerResult(SpawnerCharacter character)
    {
        if (SpawnerResultImage != null && SpawnerResultText != null)
        {
            SpawnerResultImage.sprite = character.Image;
            SpawnerResultText.text = $"{character.Grade} character: {character.Name} 획득!";
        }
    }
}
