using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SettingType
{
    Sound,
    Haptic
}

public class SettingButton : MonoBehaviour
{
    public SettingType settingType = SettingType.Sound;

    [SerializeField] private Sprite activeImage;
    [SerializeField] private Sprite inActiveImage;

    private Image buttonImage;
    private Text buttonText;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        buttonText = GetComponentInChildren<Text>();
    }

    void OnEnable()
    {
        if (settingType == SettingType.Sound)
        {
            bool isBGM = PlayerPrefs.GetInt("isBGM", 1) == 1; 
            UpdateSetting(isBGM);
        }
        else if (settingType == SettingType.Haptic)
        {
            bool isVib = PlayerPrefs.GetInt("isVib", 1) == 1; 
            UpdateSetting(isVib);
        }
    }

    public void ChangeSetting()
    {
        if (settingType == SettingType.Sound)
        {
            bool isBGM = PlayerPrefs.GetInt("isBGM", 1) == 1;
            isBGM = !isBGM; // 반전
            PlayerPrefs.SetInt("isBGM", isBGM ? 1 : 0); 
            UpdateSetting(isBGM);
        }
        else if (settingType == SettingType.Haptic)
        {
            bool isVib = PlayerPrefs.GetInt("isVib", 1) == 1;
            isVib = !isVib; // 반전
            PlayerPrefs.SetInt("isVib", isVib ? 1 : 0); 
            UpdateSetting(isVib);
        }
    }

    private void UpdateSetting(bool isActive)
    {
        // 이미지 변경
        buttonImage.sprite = isActive ? activeImage : inActiveImage;

        // 텍스트 변경
        if (buttonText != null)
        {
            buttonText.text = isActive ? "Yes!" : "Nope!";
        }
    }
}
