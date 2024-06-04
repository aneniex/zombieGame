using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI gemsText;

    [SerializeField]
    private GameObject claimButton;
    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private GameObject claim2xButton;

    public int currentUnlockedLevels;
    public int nextLevelNumber;
    public int currentLevelCompleted;

    private void Start()
    {
        nextButton.SetActive(false);

        coinsText.text = Level.Instance.coinsForThisLevel.ToString();
        gemsText.text = Level.Instance.gemsForThisLevel.ToString();

        currentLevelCompleted = AdditiveSceneManager.levelNumber;
        currentUnlockedLevels = PlayerPrefs.GetInt(PlayerPrefsHelper.CurrentLevel, 0);

        var tempNextLevel = currentLevelCompleted;
        tempNextLevel++;
        nextLevelNumber = tempNextLevel;

        if(nextLevelNumber > currentUnlockedLevels)
        {
            PlayerPrefs.SetInt(PlayerPrefsHelper.CurrentLevel, nextLevelNumber);
        }

        var nextLevelToLoad = nextLevelNumber;
        AdditiveSceneManager.levelNumber = nextLevelToLoad;
    }


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

    public void OnHomeButtonPresed()
    {
        //set time scale to 1
        Time.timeScale = 1.0f;

        //restart the current level
        SceneManager.LoadScene(1);

        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();
    }

    public void OnNextButtonPressed()
    {
        //set time scale to 1
        Time.timeScale = 1.0f;

        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();


        if(nextLevelNumber > 9)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            //restart the current level
            SceneManager.LoadScene(2);
        }

    }

    public void OnClaimButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        //hide claim button and 2x claim button
        claimButton.SetActive(false);
        claim2xButton.SetActive(false);

        //enable next button
        nextButton.SetActive(true);

        //add the coins and gems in player prefs
        PlayerPrefs.SetInt(PlayerPrefsHelper.Gold, PlayerPrefs.GetInt(PlayerPrefsHelper.Gold) + Level.Instance.coinsForThisLevel);
        PlayerPrefs.SetInt(PlayerPrefsHelper.Gems, PlayerPrefs.GetInt(PlayerPrefsHelper.Gems) + Level.Instance.gemsForThisLevel);

    }

    public void OnClaim2xButtonPressed()
    {


        //show rewarded ad
        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.ShowRewardedAd(
                () =>
                {
                    //double the reward of gems and coins
                    Level.Instance.coinsForThisLevel *= 2;
                    Level.Instance.gemsForThisLevel *= 2;

                    coinsText.text = Level.Instance.coinsForThisLevel.ToString();
                    gemsText.text = Level.Instance.gemsForThisLevel.ToString();
                });
        }
        else
        {
            //hide claim button and 2x claim button
            claimButton.SetActive(false);
            claim2xButton.SetActive(false);
            //TODO: SHOW REWARDED VIDEO AD AND DOUBLE THE AMOUNT OF COINS AND GEMS COLLECTED

            //enable next button
            nextButton.SetActive(true);
        }

        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();
    }
}
