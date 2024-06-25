using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using static CharacterData;
public class CharacterManager : MonoBehaviour
{
    public List<CharacterData> availableCharacters; // 가챠에서 사용할 캐릭터 목록
    public List<Character> ownedCharacters; // 소유한 캐릭터 목록
    public FusionManager fusionManager; // 합성 UI 매니저
    public CharacterData characterdata;
    public EnhancementManager enhancementManager;

    // 캐릭터를 소유 목록에 추가하는 메서드
    public void AddCharacter(Character character)
    {
        ownedCharacters.Add(character);
    }

    // 유닛 이름으로 캐릭터 데이터 가져오기
    public CharacterData GetCharacterDataByName(string unitName)
    {
        foreach (var characterData in availableCharacters)
        {
            if (characterData.characterName == unitName)
            {
                return characterData;
            }
        }
        return null;
    }

    // 동일한 캐릭터가 특정 수 이상일 때 합성하는 메서드
    public void CheckForFusion(Character character)
    {
        int characterCount = CountCharacters(character.characterData);
        if (characterCount >= 3)
        {
            FuseCharacters(character.characterData);
        }
    }

    // 특정 캐릭터 데이터의 동일한 캐릭터 수를 세는 메서드
    public int CountCharacters(CharacterData characterData)
    {
        int count = 0;
        foreach (Character character in ownedCharacters)
        {
            if (character.characterData == characterData)
            {
                count++;
            }
        }
        return count;
    }

    // 캐릭터 합성 메서드
    void FuseCharacters(CharacterData characterData)
    {
        int count = 0;
        List<Character> charactersToRemove = new List<Character>();

        foreach (Character character in ownedCharacters)
        {
            if (character.characterData == characterData)
            {
                charactersToRemove.Add(character);
                count++;
                if (count >= 3)
                {
                    break;
                }
            }
        }

        // 기존 캐릭터 3개 삭제
        foreach (Character character in charactersToRemove)
        {
            ownedCharacters.Remove(character);
            Destroy(character.gameObject);
        }

        // 한 단계 높은 등급의 캐릭터 선택
        CharacterData newCharacterData = GetRandomHigherGradeCharacter(characterData.grade);
        if (newCharacterData != null)
        {
            // 새로운 캐릭터 생성 및 추가
            Character newCharacter = CreateCharacter(newCharacterData);
            AddCharacter(newCharacter);

            // 합성 UI를 표시
            fusionManager.OnCharacterClicked(newCharacter);
        }
    }

    // 한 단계 높은 등급의 캐릭터를 랜덤으로 선택하는 메서드
    CharacterData GetRandomHigherGradeCharacter(CharacterGrade currentGrade)
    {
        CharacterGrade nextGrade = GetNextGrade(currentGrade);
        var higherGradeCharacters = availableCharacters.Where(c => c.grade == nextGrade).ToList();
        if (higherGradeCharacters.Count > 0)
        {
            int randomIndex = Random.Range(0, higherGradeCharacters.Count);
            return higherGradeCharacters[randomIndex];
        }
        return null;
    }

    // 현재 등급의 다음 등급을 반환하는 메서드
    CharacterGrade GetNextGrade(CharacterGrade grade)
    {
        switch (grade)
        {
            case CharacterGrade.Normal:
                return CharacterGrade.Magic;
            case CharacterGrade.Magic:
                return CharacterGrade.Hero;
            case CharacterGrade.Hero:
                return CharacterGrade.Legendary;
            case CharacterGrade.Legendary:
                return CharacterGrade.Mythic;
            default:
                return CharacterGrade.Mythic; // 신화 등급 이상은 신화 등급 유지
        }
    }

    // 캐릭터 데이터로 새 캐릭터를 생성하는 메서드
    public Character CreateCharacter(CharacterData characterData)
    {
        GameObject characterObject = new GameObject(characterData.characterName);
        Character newCharacter = characterObject.AddComponent<Character>();
        newCharacter.characterData = characterData;
        newCharacter.InitializeCharacter();
        return newCharacter;
    }

    // 모든 캐릭터 강화를 하는 메서
    public void EnhanceAllCharacters()
    {
        foreach (Character character in ownedCharacters.ToList()) // ToList()를 사용하여 반복 중 컬렉션 수정을 방지
        {
            enhancementManager.EnhanceCharacter(character);
        }
    }
}