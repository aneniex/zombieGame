using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{

    private void OnEnable()
    {
        if (AdsManager.Instance)
            AdsManager.Instance.ShowBannerAd();
    }

    private void OnDisable()
    {
        if (AdsManager.Instance)
            AdsManager.Instance.HideBannerAd();
    }

    public void OnContinueButtonPressed()
    {
        //set time scale to 1
        Time.timeScale = 1.0f;

        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        //disable pause panel
        this.gameObject.SetActive(false);

        //enable gameplay panel

        UIManager.Instance.gameplayPanel.SetActive(true);
    }

    public void OnRestartButtonPressed()
    {
        //set time scale to 1
        Time.timeScale = 1.0f;

        //restart the current level
        SceneManager.LoadScene(2);

        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();
    }

    public void OnQuitButtonPressed()
    {
        //set time scale to 1
        Time.timeScale = 1.0f;

        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        //go to main menu
        SceneManager.LoadScene(1);
    }

}
