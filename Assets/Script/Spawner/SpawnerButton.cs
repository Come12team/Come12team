using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerButton : MonoBehaviour
{
    public SpawnerUIManager SpawnerUIManager;

    public void OnSpawnerButtonClicked()
    {
        bool success = SpawnerSystem.Instance.RollSpawnerWithDiamond(); // 가챠 실행 및 성공 여부 확인

        if (success)
        {
            CharacterData character = SpawnerSystem.Instance.RollSpawner(SpawnerSystem.Instance.diamondGradeProbabilities); // 가챠 결과 캐릭터 가져오기
            SpawnerUIManager.ShowSpawnerResult(character); // UI 매니저를 통해 결과 표시
        }
    }
}
