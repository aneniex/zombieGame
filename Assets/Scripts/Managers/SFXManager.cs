using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public bool isSoundEnabled = false;
    public bool isMusicEnabled = false;
    public bool isVibrationEnabled = false;

    public AudioSources audioSources;

    public static SFXManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        InitAudioStates();

        //Initialize Vibration
        Vibration.Init();
    }

    private void InitAudioStates()
    {
        SoundState();
        MusicState();
        VibrationState();
    }

    private void SoundState()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsHelper.Sound))
        {
            PlayerPrefs.SetInt(PlayerPrefsHelper.Sound, 1);
            isSoundEnabled = true;
        }
        else
        {
            var soundState = PlayerPrefs.GetInt(PlayerPrefsHelper.Sound);
            if (soundState == 1)
            {
                isSoundEnabled = true;
            }
            else
            {
                isSoundEnabled = false;
            }
        }
    }

    private void MusicState()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsHelper.Music))
        {
            PlayerPrefs.SetInt(PlayerPrefsHelper.Music, 1);
            isMusicEnabled = true;
        }
        else
        {
            var musicState = PlayerPrefs.GetInt(PlayerPrefsHelper.Music);
            if (musicState == 1)
            {
                isMusicEnabled = true;
            }
            else
            {
                isMusicEnabled = false;
            }
        }


        PlayBGMusic();
    }

    private void VibrationState()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsHelper.Vibration))
        {
            PlayerPrefs.SetInt(PlayerPrefsHelper.Vibration, 1);
            isVibrationEnabled = true;
        }
        else
        {
            var vibrationState = PlayerPrefs.GetInt(PlayerPrefsHelper.Vibration);
            if (vibrationState == 1)
            {
                isVibrationEnabled = true;
            }
            else
            {
                isVibrationEnabled = false;
            }
        }
    }

    public void OnVibrateEvent()
    {
        if (isVibrationEnabled)
            Vibration.VibratePop();
    }

    public void PlayClickSfx()
    {
        if (isSoundEnabled)
            audioSources.clickFx.Play();
    }

    public void StopBgMusic()
    {
        audioSources.bgMusic.Stop();
    }

    public void PlayBGMusic()
    {
        if (isMusicEnabled)
            audioSources.bgMusic.Play();
    }

    public void PlaySwordSwingSound()
    {
        if (isSoundEnabled)
        {
            if (audioSources.swordSwing.isPlaying) return;
            audioSources.swordSwing.Play();
        }

    }

    public void PlayZombieDeathSound()
    {
        if (isSoundEnabled)
            audioSources.zombieDeathSound.Play();
    }

    public void PlaySwordHitSound()
    {
        if (isSoundEnabled)
        {
            //if (audioSources.swordHitSound.isPlaying) return;
            audioSources.swordHitSound.PlayOneShot(audioSources.swordHitSound.clip);
        }
    }

    public void PlayExplosionFlashSound()
    {
        if(isSoundEnabled)
            audioSources.explosionFlash.Play();
    }
}

[Serializable]
public class AudioSources
{
    public AudioSource clickFx;
    public AudioSource swordSwing;
    public AudioSource bgMusic;
    public AudioSource zombieDeathSound;
    public AudioSource swordHitSound;
    public AudioSource explosionFlash;
}