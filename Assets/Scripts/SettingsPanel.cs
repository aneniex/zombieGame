using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    public GameObject soundOnButton;
    public GameObject soundOffButton;
    public GameObject musicOnButton;
    public GameObject musicOffButton;
    public GameObject vibrationOnButton;
    public GameObject vibrationOffButton;


    // Start is called before the first frame update
    void Start()
    {
        InitButtonsState();
    }

    private void OnEnable()
    {
        if(AdsManager.Instance)
            AdsManager.Instance.ShowBannerAd();
    }

    private void OnDisable()
    {
        if (AdsManager.Instance)
            AdsManager.Instance.HideBannerAd();
    }

    private void InitButtonsState()
    {
        if (SFXManager.Instance == null) return;

        if (SFXManager.Instance.isSoundEnabled)
        {
            soundOnButton.SetActive(true);
            soundOffButton.SetActive(false);
        }
        else
        {
            soundOnButton.SetActive(false);
            soundOffButton.SetActive(true);
        }


        if (SFXManager.Instance.isMusicEnabled)
        {
            musicOnButton.SetActive(true);
            musicOffButton.SetActive(false);
        }
        else
        {
            musicOnButton.SetActive(false);
            musicOffButton.SetActive(true);
        }

        if (SFXManager.Instance.isVibrationEnabled)
        {
            vibrationOnButton.SetActive(true);
            vibrationOffButton.SetActive(false);
        }
        else
        {
            vibrationOnButton.SetActive(false);
            vibrationOffButton.SetActive(true);
        }
        
    }

    public void OnSoundOnButtonPressed()
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayClickSfx();
            SFXManager.Instance.isSoundEnabled = false;
        }

        PlayerPrefs.SetInt(PlayerPrefsHelper.Sound, 0);
        soundOnButton.SetActive(false);
        soundOffButton.SetActive(true);

    }


    public void OnSoundOffButtonPressed()
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayClickSfx();
            SFXManager.Instance.isSoundEnabled = true;
        }

        PlayerPrefs.SetInt(PlayerPrefsHelper.Sound, 1);
        soundOnButton.SetActive(true);
        soundOffButton.SetActive(false);
    }


    public void OnMusicOnButtonPressed()
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayClickSfx();
            SFXManager.Instance.isMusicEnabled = false;
            SFXManager.Instance.StopBgMusic();
        }

        PlayerPrefs.SetInt(PlayerPrefsHelper.Music, 0);
        musicOnButton.SetActive(false);
        musicOffButton.SetActive(true);


    }

    public void OnMusicOffButtonPressed()
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayClickSfx();
            SFXManager.Instance.isMusicEnabled = true;
            SFXManager.Instance.PlayBGMusic();
        }

        PlayerPrefs.SetInt(PlayerPrefsHelper.Music, 1);
        musicOnButton.SetActive(true);
        musicOffButton.SetActive(false);
    }

    public void OnVibrationOnButtonPressed()
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayClickSfx();
            SFXManager.Instance.isVibrationEnabled = false;
        }

        PlayerPrefs.SetInt(PlayerPrefsHelper.Vibration, 0);
        vibrationOnButton.SetActive(false);
        vibrationOffButton.SetActive(true);
    }

    public void OnVibrationOffButtonPressed()
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayClickSfx();
            SFXManager.Instance.isVibrationEnabled = true;
        }

        PlayerPrefs.SetInt(PlayerPrefsHelper.Vibration,1);
        vibrationOnButton.SetActive(true);
        vibrationOffButton.SetActive(false);
    }


    public void OnCloseSettingsButtonPressed()
    {
        this.gameObject.SetActive(false);

        if (SFXManager.Instance != null)
            SFXManager.Instance.PlayClickSfx();
    }

    public void OnInstagramButtonPressed()
    {
        if (SFXManager.Instance != null)
            SFXManager.Instance.PlayClickSfx();

        Application.OpenURL("https://www.instagram.com/rapture_play/");
    }

    public void OnFacebookButtonPressed()
    {
        if (SFXManager.Instance != null)
            SFXManager.Instance.PlayClickSfx();

        Application.OpenURL("https://www.facebook.com/gaming/raptureplayy");
    }
}
