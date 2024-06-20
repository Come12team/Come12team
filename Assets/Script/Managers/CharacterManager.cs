using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<CharacterData> availableCharacters; // 가챠에서 사용할 캐릭터 목록
    public List<Character> ownedCharacters; // 소유한 캐릭터 목록
    public int gachaCost;
    public int gachaTickets;

    // 캐릭터를 소유 목록에 추가하는 메서드
    public void AddCharacter(Character character)
    {
        ownedCharacters.Add(character);
    }

    // 가챠를 통해 캐릭터를 뽑는 메서드
    public CharacterData RollGacha()
    {
        if (!UseGachaTicket())
        {
            Debug.Log("Not enough gacha tickets.");
            return null;
        }

        // 랜덤으로 캐릭터 선택
        int randomIndex = Random.Range(0, availableCharacters.Count);
        CharacterData newCharacter = availableCharacters[randomIndex];
        AddCharacter(CreateCharacter(newCharacter));

        return newCharacter;
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

    // 가챠 티켓을 사용하는 메서드
    public bool UseGachaTicket()
    {
        if (gachaTickets > 0)
        {
            gachaTickets--;
            return true;
        }
        return false;
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
    int CountCharacters(CharacterData characterData)
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

        // 새로운 캐릭터 생성 및 추가
        Character newCharacter = CreateCharacter(characterData);
        newCharacter.Upgrade();
        AddCharacter(newCharacter);
    }
}