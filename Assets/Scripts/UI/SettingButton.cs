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
        bool isSetting = PlayerPrefs.GetInt(settingType == SettingType.Sound ? "isBGM" : "isVib", 1) == 1;
        UpdateSetting(isSetting);       
    }

    public void ChangeSetting()
    {
        bool isSetting = PlayerPrefs.GetInt(settingType == SettingType.Sound ? "isBGM" : "isVib", 1) == 1;
        isSetting = !isSetting;
        PlayerPrefs.SetInt(settingType == SettingType.Sound ? "isBGM" : "isVib", isSetting ? 1 : 0);
        UpdateSetting(isSetting);

        AudioManager.Instance.PlaySFX(AudioID.Click);
    }

    private void UpdateSetting(bool isActive)
    {
        buttonImage.sprite = isActive ? activeImage : inActiveImage;

        if (buttonText != null)
        {
            buttonText.text = isActive ? "Yes!" : "Nope!";
        }

        if (settingType == SettingType.Sound)
        {
            AudioManager.Instance.ChangeVolume(isActive);
        }
    }
}
