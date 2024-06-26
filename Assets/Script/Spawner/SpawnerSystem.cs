using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using static CharacterData;

public class SpawnerSystem : MonoBehaviour
{
    public static SpawnerSystem Instance { get; private set; }

    private Dictionary<SpawnerSystem.CharacterGrade, List<CharacterData>> characterDatasByGrade; // 각 등급별로 CharacterData 객체 리스트를 관리하는 딕셔너리
    public Dictionary<CharacterGrade, float> moneyGradeProbabilities { get; private set; }
    public Dictionary<CharacterGrade, float> diamondGradeProbabilities { get; private set; } 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 자신을 파괴
        }

        InitializeSpawnerSystem(); // 스포너 시스템 초기화 메서드 호출
    }

    private void InitializeSpawnerSystem()
    {
        // 딕셔너리 초기화
        characterDatasByGrade = new Dictionary<CharacterGrade, List<CharacterData>>();
        moneyGradeProbabilities = new Dictionary<CharacterGrade, float>
        {
            { CharacterGrade.Normal, 0.5f },    // 일반 등급 확률 설정
            { CharacterGrade.Magic, 0.3f },     // 마법 등급 확률 설정
            { CharacterGrade.Hero, 0.1f },      // 영웅 등급 확률 설정
            { CharacterGrade.Legendary, 0.05f },// 전설 등급 확률 설정
            { CharacterGrade.Mythic, 0.01f }    // 신화 등급 확률 설정
        };

        diamondGradeProbabilities = new Dictionary<CharacterGrade, float>
        {
            { CharacterGrade.Normal, 0.1f },    // 일반 등급 확률 설정
            { CharacterGrade.Magic, 0.3f },     // 마법 등급 확률 설정
            { CharacterGrade.Hero, 0.5f },      // 영웅 등급 확률 설정
            { CharacterGrade.Legendary, 0.3f }, // 전설 등급 확률 설정
            { CharacterGrade.Mythic, 0.1f }     // 신화 등급 확률 설정
        };

        // 각 등급에 따른 캐릭터 데이터 로드 및 추가
        LoadCharacterData(CharacterGrade.Normal, new string[] { "고양이", "강아지", "토끼", "고슴도치" }, Resources.Load<Sprite>("normal_character_sprite"));
        LoadCharacterData(CharacterGrade.Magic, new string[] { "마법사", "요정", "드래곤", "페어리" }, Resources.Load<Sprite>("magic_character_sprite"));
        LoadCharacterData(CharacterGrade.Hero, new string[] { "용사", "전사", "기사", "도적" }, Resources.Load<Sprite>("hero_character_sprite"));
        LoadCharacterData(CharacterGrade.Legendary, new string[] { "전설의 생물", "악마", "천사", "선인" }, Resources.Load<Sprite>("legendary_character_sprite"));
        LoadCharacterData(CharacterGrade.Mythic, new string[] { "신화적인 생물", "용", "티탄", "신" }, Resources.Load<Sprite>("mythic_character_sprite"));
    }

    // 특정 등급에 해당하는 캐릭터 데이터 로드 및 리스트에 추가하는 메서드
    private void LoadCharacterData(CharacterGrade grade, string[] names, Sprite sprite)
    {
        List<CharacterData> characters = new List<CharacterData>();
        foreach (string name in names)
        {
            CharacterData data = CreateCharacterData(name, grade, sprite);
            characters.Add(data);
        }
        characterDatasByGrade.Add(grade, characters); // 등급별 캐릭터 데이터 리스트를 딕셔너리에 추가
    }

    // 캐릭터 데이터를 생성하고 초기화하는 메서드
    private CharacterData CreateCharacterData(string name, CharacterGrade grade, Sprite sprite)
    {
        CharacterData data = ScriptableObject.CreateInstance<CharacterData>();
        data.characterName = name; // 캐릭터 이름 설정
        data.grade = (CharacterData.CharacterGrade)grade; // 캐릭터 등급 설정
        data.characterSprite = sprite; // 캐릭터 스프라이트 설정
        // 추가적으로 필요한 초기화 작업 수행
        return data;
    }

    // 돈을 사용한 가챠 실행 메서드
    public void RollSpawnerWithMoney()
    {
        int moneyCost = 100; // 가챠에 필요한 돈 비용
        MoneyManager.Instance.PayMoney(moneyCost); // 돈 지불

        CharacterData result = RollSpawner(moneyGradeProbabilities); // 가챠 실행 및 결과 반환
        if (result != null)
        {
            MoneyManager.Instance.CheckQuestCompletion(); // 퀘스트 상황을 확인하여 보상 지급
        }
    }

    // 다이아몬드를 사용한 가챠 실행 메서드
    public bool RollSpawnerWithDiamond()
    {
        int diamondCost = 1; // 가챠에 필요한 다이아몬드 비용
        bool success = MoneyManager.Instance.PayDiamonds(diamondCost); // 다이아몬드 지불

        if (success)
        {
            CharacterData result = RollSpawner(diamondGradeProbabilities); // 가챠 실행 및 결과 반환
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
            Debug.LogWarning("다이아몬드가 부족합니다."); // 다이아몬드가 부족한 경우 경고 메시지 출력
            return false; // 가챠 실행 실패
        }
    }

    // 등급별 확률을 기반으로 가챠 실행 및 결과 반환하는 메서드
    public CharacterData RollSpawner(Dictionary<CharacterGrade, float> gradeProbabilities)
    {
        float randomValue = Random.value; // 0에서 1 사이의 랜덤 값 생성
        float cumulativeProbability = 0f; // 누적 확률 초기화

        foreach (var grade in gradeProbabilities)
        {
            cumulativeProbability += grade.Value; // 누적 확률 증가
            if (randomValue <= cumulativeProbability)
            {
                return GetRandomCharacterByGrade(grade.Key); // 누적 확률에 따라 등급별 캐릭터 선택 및 반환
            }
        }

        return GetRandomCharacterByGrade(CharacterGrade.Normal); // 기본 등급 (오류 방지용)
    }

    // 특정 등급에 해당하는 랜덤 캐릭터 데이터 반환 메서드
    private CharacterData GetRandomCharacterByGrade(CharacterGrade grade)
    {
        if (characterDatasByGrade.ContainsKey(grade)) // 해당 등급의 캐릭터 데이터 리스트가 존재하는지 확인
        {
            List<CharacterData> filteredCharacters = characterDatasByGrade[grade]; // 해당 등급의 캐릭터 데이터 리스트 가져오기
            int randomIndex = Random.Range(0, filteredCharacters.Count); // 랜덤 인덱스 선택
            return filteredCharacters[randomIndex]; // 랜덤 캐릭터 데이터 반환
        }
        else
        {
            Debug.LogWarning("해당 등급에 대한 캐릭터가 없습니다: " + grade.ToString()); // 등급에 대한 캐릭터가 없는 경우 경고 메시지 출력
            return null;
        }
    }

    // 캐릭터 등급 열거형
    public enum CharacterGrade
    {
        Normal,     // 일반
        Magic,      // 마법
        Hero,       // 영웅
        Legendary,  // 전설
        Mythic      // 신화
    }

    // 기타 메서드와 속성들은 생략함
}