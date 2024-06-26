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

    private int gridWidth = 4; // 가로 칸 수
    private int gridHeight = 4; // 세로 칸 수
    private int[,] grid; // 인벤토리 그리드

    void Start()
    {
        // 그리드 초기화
        grid = new int[gridWidth, gridHeight];
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                grid[x, y] = 0; // 0은 빈 칸을 의미
            }
        }

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
        string unitName = selectedPrefab.GetComponent<Character>().characterData.characterName; // 예시로 프리팹 이름을 사용 (실제로는 데이터에서 이름을 가져와야 함)

        // 유닛 데이터 가져오기
        CharacterData characterData = characterManager.GetCharacterDataByName(unitName);
        if (characterData == null)
        {
            Debug.Log("유닛 데이터를 찾을 수 없습니다.");
            return;
        }

        // 그리드에서 빈 위치 찾기 (위에서 아래로 검색)
        Vector3 spawnPosition = Vector3.zero;
        bool positionFound = false;

        for (int y = gridHeight - 1; y >= 0; y--)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if (grid[x, y] < 1)
                {
                    // 빈 칸을 찾음
                    spawnPosition = GetPositionFromGrid(x, y);
                    grid[x, y] ++; // 그리드 업데이트
                    positionFound = true;
                    break;
                }
            }
            if (positionFound) break;
        }

        if (!positionFound)
        {
            Debug.LogWarning("빈 위치를 찾지 못했습니다.");
            return;
        }

        // 유닛 생성 및 캐릭터 매니저에 추가
        GameObject newUnit = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
        Character newCharacter = newUnit.GetComponent<Character>();
        newCharacter.characterData = characterData; // 유닛 데이터 설정
        characterManager.AddCharacter(newCharacter);
    }

    Vector3 GetPositionFromGrid(int x, int y)
    {
        // 스폰 영역의 경계를 가져와서 그리드 위치를 계산
        Bounds bounds = spawnArea.bounds;
        float cellWidth = bounds.size.x / gridWidth;
        float cellHeight = bounds.size.y / gridHeight;

        float posX = bounds.min.x + x * cellWidth + cellWidth / 2;
        float posY = bounds.min.y + y * cellHeight + cellHeight / 2;

        return new Vector3(posX, posY, 0);
    }
}