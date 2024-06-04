using RapturePlay;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panel")]
    [Space(5)]
    public GameObject mainCanvas;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject rateUsPanel;
    public GameObject removeAdsPanel;
    public GameObject dailyRewardPanel;
    public GameObject profilePanel;
    public GameObject shopPanel;
    public GameObject customizationPanel;
    public GameObject levelSelectionPanel;

    [Space(10)]
    [Header("Texts")]
    [Space(5)]
    public TextMeshProUGUI coinsDisplayText;
    public TextMeshProUGUI bulletsDisplayText;
    public TextMeshProUGUI playerNameText;

    [Space(10)]
    [Header("Images")]
    [Space(5)]
    public GameObject[] displayImages;
    public GameObject removeAdsButton;

    public static MainMenuManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        InitEconomy();
        CheckForRemoveAds();

        Application.targetFrameRate = 300;

        var currentDisplayImage = PlayerPrefs.GetInt(PlayerPrefsHelper.CurrentDisplayImage, 0);
        displayImages[currentDisplayImage].SetActive(true);

        if (!PlayerPrefs.HasKey(PlayerPrefsHelper.PlayerName))
        {
            profilePanel.SetActive(true);
            PlayerPrefs.SetString(PlayerPrefsHelper.PlayerName, "Guest - 01");
            playerNameText.text = "Guest - 01";
        }
        else
        {
            playerNameText.text = PlayerPrefs.GetString(PlayerPrefsHelper.PlayerName, "Guest - 01");
        }


        PlayerPrefs.SetInt(PlayerPrefsHelper.Player_0, 1);
        PlayerPrefs.SetInt(PlayerPrefsHelper.Player_1, 1);
    }

    private void InitEconomy()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsHelper.Gold))
        {
            PlayerPrefs.SetInt(PlayerPrefsHelper.Gold, 50);
            coinsDisplayText.text = PlayerPrefs.GetInt(PlayerPrefsHelper.Gold).ToString();
        }
        else
        {
            coinsDisplayText.text = PlayerPrefs.GetInt(PlayerPrefsHelper.Gold).ToString();
        }


        if (!PlayerPrefs.HasKey(PlayerPrefsHelper.Gems))
        {
            PlayerPrefs.SetInt(PlayerPrefsHelper.Gems, 10);
            bulletsDisplayText.text = PlayerPrefs.GetInt(PlayerPrefsHelper.Gems).ToString();
        }
        else
        {
            bulletsDisplayText.text = PlayerPrefs.GetInt(PlayerPrefsHelper.Gems).ToString();
        }
    }

    public void OnSettingsButtonPressed()
    {
        settingsPanel.SetActive(true);

        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();
    }

    public void OnRateUsButtonPressed()
    {
        rateUsPanel.SetActive(true);

        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();
    }

    public void OnRemoveAdsButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        InAppManager.Instance.OnPurchaseClicked(IAPItems.remove_ads);
    }

    public void CheckForRemoveAds()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsHelper.RemoveAds) == 1)
        {
            removeAdsButton.SetActive(false);
        }
        else
        {
            if (PlayerPrefs.GetInt(PlayerPrefsHelper.AppOpened) % 2 == 0)
            {
                removeAdsPanel.SetActive(true);
            }
        }

        if (PlayerPrefs.GetInt(PlayerPrefsHelper.AppOpened) % 5 == 0)
        {
            rateUsPanel.SetActive(true);
        }
    }

    public void OnPlayButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(true);
    }

    public void OnRewardButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        dailyRewardPanel.SetActive(true);
    }

    public void ToggleDisplayImage(int index)
    {
        PlayerPrefs.SetInt(PlayerPrefsHelper.CurrentDisplayImage, index);

        for (int i = 0; i < displayImages.Length; i++)
        {
            displayImages[i].SetActive(false);
        }

        displayImages[index].SetActive(true);
    }

    public void OnProfileButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        profilePanel.SetActive(true);
    }

    public void OnShopButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();


        ShopPanel.isCoinsSelected = true;
        ShopPanel.isGemsSelected = false;

        shopPanel.SetActive(true);
    }

    public void OnGoldButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        ShopPanel.isCoinsSelected = true;
        ShopPanel.isGemsSelected = false;

        shopPanel.SetActive(true);
    }

    public void OnGemsButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        ShopPanel.isCoinsSelected = false;
        ShopPanel.isGemsSelected = true;

        shopPanel.SetActive(true);
    }

    public void OnCustomizationButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        customizationPanel.SetActive(true);
        mainCanvas.SetActive(false);
    }
}
