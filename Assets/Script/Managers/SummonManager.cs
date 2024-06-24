using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonManager : MonoBehaviour
{
    public GameObject[] unitPrefabs; // 생성할 유닛 프리팹들을 배열로 저장
    public BoxCollider2D spawnArea; // 유닛이 생성될 영역
    public Button summonButton; // 소환 버튼
    public int cost = 10; // 유닛 소환 비용
    public int playerGold = 100; // 플레이어의 초기 재화
    public CharacterManager characterManager; // 캐릭터 매니저 참조

    // 유닛 이름별 소환된 위치를 저장하는 딕셔너리
    private Dictionary<string, Vector3> unitSpawnPositions = new Dictionary<string, Vector3>();

    public void Start()
    {
        // 버튼 클릭 이벤트 설정
        summonButton.onClick.AddListener(SummonUnit);
    }

    public void SummonUnit()
    {
        // 소환 비용이 충분하지 않은 경우를 먼저 처리
        if (playerGold < cost)
        {
            Debug.Log("골드가 부족합니다.");
            return;
        }

        // 재화 차감
        playerGold -= cost;

        // 가챠나 다른 방식으로 유닛 이름을 결정 (랜덤하게 선택)
        int randomIndex = Random.Range(0, unitPrefabs.Length);
        GameObject selectedPrefab = unitPrefabs[randomIndex];
        string unitName = selectedPrefab.name; // 예시로 프리팹 이름을 사용 (실제로는 데이터에서 이름을 가져와야 함)

        // 유닛 데이터 가져오기
        CharacterData characterData = characterManager.GetCharacterDataByName(unitName);
        if (characterData == null)
        {
            Debug.Log("유닛 데이터를 찾을 수 없습니다.");
            return;
        }

        // 유닛의 소환 위치 결정
        Vector3 spawnPosition;
        if (unitSpawnPositions.TryGetValue(unitName, out spawnPosition))
        {
            // 이미 소환된 유닛이면 저장된 위치 사용
            Debug.Log($"{unitName} 유닛이 기존 위치에 소환됩니다: {spawnPosition}");
        }
        else
        {
            // 처음 소환되는 유닛이면 랜덤 위치 계산
            spawnPosition = GetRandomPositionInSpawnArea();
            unitSpawnPositions[unitName] = spawnPosition; // 위치 저장
            Debug.Log($"{unitName} 유닛이 새로운 위치에 소환됩니다: {spawnPosition}");
        }

        // 유닛 생성 및 캐릭터 매니저에 추가
        GameObject newUnit = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
        Character newCharacter = newUnit.GetComponent<Character>();
        newCharacter.characterData = characterData; // 유닛 데이터 설정
        characterManager.AddCharacter(newCharacter);
    }

    Vector3 GetRandomPositionInSpawnArea()
    {
        // 스폰 영역의 경계를 가져와서 랜덤 위치 계산
        Bounds bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(x, y, 0);
    }
}