using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFailed : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalGemsText;
    [SerializeField] private TextMeshProUGUI gemsRequiredForReviving;


    public void OnEnable()
    {
        Time.timeScale = 0f;

        totalGemsText.text = PlayerPrefs.GetInt(PlayerPrefsHelper.Gems).ToString();

        gemsRequiredForReviving.text = Level.Instance.gemsRequiredForReviving.ToString();

        if (AdsManager.Instance)
            AdsManager.Instance.ShowBannerAd();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;

        if (AdsManager.Instance)
            AdsManager.Instance.HideBannerAd();
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

    public void OnHomeButtonPresed()
    {
        //set time scale to 1
        Time.timeScale = 1.0f;

        //restart the current level
        SceneManager.LoadScene(1);

        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();
    }

    public void OnReviveButtonPressed(bool useGems)
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        if (useGems)
        {
            var totalGems =PlayerPrefs.GetInt(PlayerPrefsHelper.Gems);

            if (totalGems < Level.Instance.gemsRequiredForReviving) return;

            totalGems -= Level.Instance.gemsRequiredForReviving;
            PlayerPrefs.SetInt(PlayerPrefsHelper.Gems, totalGems);

            //set the text for total available gems
            totalGemsText.text = totalGems.ToString();

            CustomCharacterController.Instance.OnPlayerReviveEvent();

            this.gameObject.SetActive(false);

            //set time scale to 1
            Time.timeScale = 1.0f;
        }
        else
        {
            if (AdsManager.Instance)
                AdsManager.Instance.ShowRewardedAd(() =>
                {
                    CustomCharacterController.Instance.OnPlayerReviveEvent();

                    this.gameObject.SetActive(false);

                    //set time scale to 1
                    Time.timeScale = 1.0f;
                });
        }
    }
}
