using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonManager : MonoBehaviour
{
    public GameObject unitPrefab; // 생성할 유닛의 프리팹
    public BoxCollider2D spawnArea; // 유닛이 생성될 영역
    public Button summonButton; // 소환 버튼
    public int cost = 10; // 유닛 소환 비용
    public int playerGold = 100; // 플레이어의 초기 재화
    public CharacterManager characterManager; // 캐릭터 매니저 참조

    void Start()
    {
        // 버튼 클릭 이벤트 설정
        summonButton.onClick.AddListener(SummonUnit);
    }

    void SummonUnit()
    {
        // 소환 비용이 충분하지 않은 경우를 먼저 처리
        if (playerGold < cost)
        {
            Debug.Log("골드가 부족합니다."); // 골드가 부족할 때 출력되는 한글 메시지
            return; // 조건이 만족되지 않는 경우 함수 종료
        }

        // 재화 차감
        playerGold -= cost;

        // 랜덤 위치 계산
        Vector3 randomPosition = GetRandomPositionInSpawnArea();

        // 유닛 생성 및 캐릭터 매니저에 추가
        Character newCharacter = Instantiate(unitPrefab, randomPosition, Quaternion.identity).GetComponent<Character>();
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