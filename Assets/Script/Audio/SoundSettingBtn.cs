using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingBtn : MonoBehaviour
{
    private Button Btn;

    private void Awake()
    {
        Btn = GetComponent<Button>();
    }

    private void Start()
    {
        Btn.onClick.AddListener(PNOpen);
    }

    public void PNOpen()
    {
        AudioManager.Instance.PNOnOff();
    }
}
