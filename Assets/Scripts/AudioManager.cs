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
    [SerializeField] Slider _bgmSlider;
    [SerializeField] Slider _sfxSlider;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        
    }

    public void PlayBGM()
    {
        _bgm.Play();
    }

    public void PlaySFX(AudioID id)
    {
        if (_sfx.isPlaying)
        {
            // If it's playing, stop it before playing the new sound
            _sfx.Stop();
        }

        // Play the new sound
        _sfx.PlayOneShot(_audioStorage.GetAudio(id));
    }

    public void SetBGM()
    {
        _audioMixer.SetFloat("BGM Volume", _bgmSlider.value);
    }

    public void SetSFX()
    {
        _audioMixer.SetFloat("SFX Volume", _sfxSlider.value);
    }

}
