using UnityEngine;
public class UIManager : MonoBehaviour
{
    public void OnEnhanceAllCharactersButtonClicked()
    {
        if (CharacterManager.Instance == null)
        {
            Debug.LogError("CharacterManager instance is null.");
            return;
        }
        CharacterManager.Instance.EnhanceAllCharacters();
    }
}