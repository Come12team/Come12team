using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSystem : MonoBehaviour
{
    public static SpawnerSystem Instance { get; private set; }

    private Dictionary<CharacterGrade, float> gradeProbabilities;
    private List<SpawnerCharacter> characters;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 다른 씬에서도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeSpawnerSystem();
    }

    private void InitializeSpawnerSystem()
    {
        // 등급별 확률 설정
        gradeProbabilities = new Dictionary<CharacterGrade, float>
        {
            { CharacterGrade.Normal, 0.5f },
            { CharacterGrade.Magic, 0.3f },
            { CharacterGrade.Hero, 0.1f },
            { CharacterGrade.Legendary, 0.08f },
            { CharacterGrade.Mythic, 0.02f }
        };

        // 캐릭터 리스트 초기화 (예시)
        characters = new List<SpawnerCharacter>
        {
            new SpawnerCharacter("고양이", CharacterGrade.Normal, Resources.Load<Sprite>("고양이")),
            new SpawnerCharacter("개", CharacterGrade.Magic, Resources.Load<Sprite>("개")),
            new SpawnerCharacter("너구리", CharacterGrade.Hero, Resources.Load<Sprite>("너구리")),
            new SpawnerCharacter("곰", CharacterGrade.Legendary, Resources.Load<Sprite>("곰")),
            new SpawnerCharacter("돌고래", CharacterGrade.Mythic, Resources.Load<Sprite>("돌고래"))
        };
    }

    public SpawnerCharacter RollSpawner()
    {
        float randomValue = Random.value;
        float cumulativeProbability = 0f;

        foreach (var grade in gradeProbabilities)
        {
            cumulativeProbability += grade.Value;
            if (randomValue <= cumulativeProbability)
            {
                return GetRandomCharacterByGrade(grade.Key);
            }
        }

        return GetRandomCharacterByGrade(CharacterGrade.Normal); // 기본 값 (오류 방지용)
    }

    private SpawnerCharacter GetRandomCharacterByGrade(CharacterGrade grade)
    {
        List<SpawnerCharacter> filteredCharacters = characters.FindAll(character => character.Grade == grade);
        int randomIndex = Random.Range(0, filteredCharacters.Count);
        return filteredCharacters[randomIndex];
    }
}