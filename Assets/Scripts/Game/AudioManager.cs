using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance => instance;
    private static AudioManager instance;
    [SerializeField] AudioStorage _audioStorage;
    [SerializeField] AudioSource _bgm;
    [SerializeField] AudioSource _sfx;
    [SerializeField] AudioMixer _audioMixer;


    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }       
        
    }

    void Start()
    {
        bool isBGM = PlayerPrefs.GetInt("isBGM", 1) == 1; // 데이터 읽기
        ChangeVolume(isBGM); // true/false 그대로 전달
    }

    public void PlayBGM(AudioID id)
    {
        if (_bgm.isPlaying)
        {
            _bgm.Stop();
        }

        // Play the new sound
        _bgm.PlayOneShot(_audioStorage.GetAudio(id));
    }

    public void PlaySFX(AudioID id)
    {
        if (_sfx.isPlaying)
        {
            _sfx.Stop();
        }

        // Play the new sound
        _sfx.PlayOneShot(_audioStorage.GetAudio(id));
    }


    public void ChangeVolume(bool isOn)
    {
        _audioMixer.SetFloat("Master Volume", isOn?1:-80);
        
    }


}
