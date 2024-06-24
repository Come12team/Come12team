using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerButton : MonoBehaviour
{
    public SpawnerUIManager SpawnerUIManager;

    public void OnSpawnerButtonClicked()
    {
        SpawnerCharacter character = SpawnerSystem.Instance.RollSpawner();
        SpawnerUIManager.ShowSpawnerResult(character);
    }
}
