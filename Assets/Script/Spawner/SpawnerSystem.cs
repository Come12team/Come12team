using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterData;

public class SpawnerSystem : MonoBehaviour
{
    public static SpawnerSystem Instance { get; private set; }

    private Dictionary<CharacterGrade, float> moneyGradeProbabilities;
    private Dictionary<CharacterGrade, float> diamondGradeProbabilities;
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
        // 머니로 가챠할 때의 등급별 확률 설정
        moneyGradeProbabilities = new Dictionary<CharacterGrade, float>
        {
            { CharacterGrade.Normal, 0.5f },
            { CharacterGrade.Magic, 0.3f },
            { CharacterGrade.Hero, 0.1f },
            { CharacterGrade.Legendary, 0.05f },
            { CharacterGrade.Mythic, 0.01f }
        };

        // 다이아몬드로 가챠할 때의 등급별 확률 설정
        diamondGradeProbabilities = new Dictionary<CharacterGrade, float>
        {
            { CharacterGrade.Normal, 0.1f },
            { CharacterGrade.Magic, 0.3f },
            { CharacterGrade.Hero, 0.5f },
            { CharacterGrade.Legendary, 0.3f },
            { CharacterGrade.Mythic, 0.1f }
        };

        // 캐릭터 리스트 초기화 (예시)
        //characters = new List<SpawnerCharacter>
        //{
        //    new SpawnerCharacter("고양이", CharacterGrade.Normal, Resources.Load<Sprite>("고양이")),
        //    new SpawnerCharacter("개", CharacterGrade.Magic, Resources.Load<Sprite>("개")),
        //    new SpawnerCharacter("너구리", CharacterGrade.Hero, Resources.Load<Sprite>("너구리")),
        //    new SpawnerCharacter("곰", CharacterGrade.Legendary, Resources.Load<Sprite>("곰")),
        //    new SpawnerCharacter("돌고래", CharacterGrade.Mythic, Resources.Load<Sprite>("돌고래"))
        //};
    }

    public void RollSpawnerWithMoney()
    {
        int moneyCost = 100; // 가챠에 필요한 돈 비용

        MoneyManager.Instance.PayMoney(moneyCost);

        // 돈을 사용하여 가챠를 실행하고 결과를 반환합니다.
        SpawnerCharacter result = RollSpawner(moneyGradeProbabilities);
        if (result != null)
        {
            MoneyManager.Instance.CheckQuestCompletion(); // 퀘스트 상황을 확인하여 보상 지급
        }
    }

    public bool RollSpawnerWithDiamond()
    {
        int diamondCost = 1; // 가챠에 필요한 다이아몬드 비용

        bool success = MoneyManager.Instance.PayDiamonds(diamondCost);

        if (success)
        {
            // 다이아몬드를 사용하여 가챠를 실행하고 결과를 반환합니다.
            SpawnerCharacter result = RollSpawner(diamondGradeProbabilities);
            if (result != null)
            {
                MoneyManager.Instance.CheckQuestCompletion(); // 퀘스트 상황을 확인하여 보상 지급
                return true; // 가챠 성공 및 결과 반환
            }
            else
            {
                return false; // 가챠 실패 (결과 없음)
            }
        }
        else
        {
            // 다이아몬드가 부족하여 가챠 실행에 실패한 경우
            Debug.LogWarning("다이아몬드가 부족합니다.");
            return false;
        }
    }

    private SpawnerCharacter RollSpawner(Dictionary<CharacterGrade, float> gradeProbabilities)
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

    public SpawnerCharacter RollSpawnerWithCurrentDiamondProbabilities()
    {
        Dictionary<CharacterGrade, float> diamondGradeProbabilities = GetDiamondGradeProbabilities();
        return RollSpawner(diamondGradeProbabilities);
    }

    public Dictionary<CharacterGrade, float> GetDiamondGradeProbabilities()
    {
        return diamondGradeProbabilities;
    }
}
