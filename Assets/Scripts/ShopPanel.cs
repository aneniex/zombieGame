using RapturePlay;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    public static bool isCoinsSelected = false;
    public static bool isGemsSelected = false;

    [Header("Texts")]
    public TextMeshProUGUI coinsDisplayText;
    public TextMeshProUGUI gemsDisplayText;

    [Header("Prices Texts")]
    public TextMeshProUGUI coins500Text;
    public TextMeshProUGUI coins800Text;
    public TextMeshProUGUI coins1000Text;
    public TextMeshProUGUI coins1500Text;
    public TextMeshProUGUI gems200Text;
    public TextMeshProUGUI gems300Text;
    public TextMeshProUGUI gems500Text;
    public TextMeshProUGUI gems1000Text;
    public TextMeshProUGUI masterBundle;
    public TextMeshProUGUI megaBundle;
    public TextMeshProUGUI legendaryBundle;
    public TextMeshProUGUI unlockAllCharacters;

    [Header("Panels")]
    public GameObject coinsPanel;
    public GameObject gemsPanel;
    public GameObject bundlesPanel;

    [Header("Buttons")]
    public GameObject coinUnselectedButton;
    public GameObject coinSelectedButton;
    public GameObject gemsUnselectedButton;
    public GameObject gemsSelectedButton;
    public GameObject bundlesUnselectedButton;
    public GameObject bundlesSelectedButton;

    private void Start()
    {
        coins500Text.text = InAppManager.Instance.GetProductPriceFromStore(IAPItems.coins_500);
        coins800Text.text = InAppManager.Instance.GetProductPriceFromStore(IAPItems.coins_800);
        coins1000Text.text = InAppManager.Instance.GetProductPriceFromStore(IAPItems.coins_1000);
        coins1500Text.text = InAppManager.Instance.GetProductPriceFromStore(IAPItems.coins_1500);

        gems200Text.text = InAppManager.Instance.GetProductPriceFromStore(IAPItems.gems_200);
        gems300Text.text = InAppManager.Instance.GetProductPriceFromStore(IAPItems.gems_300);
        gems500Text.text = InAppManager.Instance.GetProductPriceFromStore(IAPItems.gems_500);
        gems1000Text.text = InAppManager.Instance.GetProductPriceFromStore(IAPItems.gems_1000);

        masterBundle.text = InAppManager.Instance.GetProductPriceFromStore(IAPItems.master_bundle);
        megaBundle.text = InAppManager.Instance.GetProductPriceFromStore(IAPItems.mega_bundle);
        legendaryBundle.text = InAppManager.Instance.GetProductPriceFromStore(IAPItems.legendary_bundle);

        unlockAllCharacters.text = InAppManager.Instance.GetProductPriceFromStore(IAPItems.unlock_characters);
    }

    private void OnEnable()
    {
        var totalCoins = PlayerPrefs.GetInt(PlayerPrefsHelper.Gold, 0);
        coinsDisplayText.text = totalCoins.ToString();

        var totalGems = PlayerPrefs.GetInt(PlayerPrefsHelper.Gems, 0);
        gemsDisplayText.text = totalGems.ToString();

        if (isCoinsSelected)
        {
            //toggle panels
            coinsPanel.SetActive(true);
            gemsPanel.SetActive(false);
            bundlesPanel.SetActive(false);

            //toggle buttons
            coinSelectedButton.SetActive(true);
            coinUnselectedButton.SetActive(false);
            gemsSelectedButton.SetActive(false);
            gemsUnselectedButton.SetActive(true);
            bundlesUnselectedButton.SetActive(true);
            bundlesSelectedButton.SetActive(false);
        }

        if (isGemsSelected)
        {
            coinsPanel.SetActive(false);
            gemsPanel.SetActive(true);
            bundlesPanel.SetActive(false);

            //toggle buttons
            coinSelectedButton.SetActive(false);
            coinUnselectedButton.SetActive(true);
            gemsSelectedButton.SetActive(true);
            gemsUnselectedButton.SetActive(false);
            bundlesUnselectedButton.SetActive(true);
            bundlesSelectedButton.SetActive(false);
        }
    }

    

    private void OnDisable()
    {
        isCoinsSelected = false;
        isGemsSelected = false;
    }

    public void OnBackButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        this.gameObject.SetActive(false);
    }

    public void ToggleCoinsPanel()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        coinSelectedButton.SetActive(true);
        coinUnselectedButton.SetActive(false);
        gemsUnselectedButton.SetActive(true);
        gemsSelectedButton.SetActive(false);
        bundlesSelectedButton.SetActive(false);
        bundlesUnselectedButton.SetActive(true);

        coinsPanel.SetActive(true);
        gemsPanel.SetActive(false);
        bundlesPanel.SetActive(false);
    }

    public void ToggleGemsPanel()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        coinSelectedButton.SetActive(false);
        coinUnselectedButton.SetActive(true);
        gemsUnselectedButton.SetActive(false);
        gemsSelectedButton.SetActive(true);
        bundlesSelectedButton.SetActive(false);
        bundlesUnselectedButton.SetActive(true);

        coinsPanel.SetActive(false);
        gemsPanel.SetActive(true);
        bundlesPanel.SetActive(false);
    }

    public void ToggleBundlePanel()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        coinSelectedButton.SetActive(false);
        coinUnselectedButton.SetActive(true);
        gemsUnselectedButton.SetActive(true);
        gemsSelectedButton.SetActive(false);
        bundlesSelectedButton.SetActive(true);
        bundlesUnselectedButton.SetActive(false);

        coinsPanel.SetActive(false);
        gemsPanel.SetActive(false);
        bundlesPanel.SetActive(true);
    }

    public void OnPurchaseCoin(int coinsAmount)
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();


        switch (coinsAmount)
        {
            case 500:
                InAppManager.Instance.OnPurchaseClicked(IAPItems.coins_500);
                break;
            case 800:
                InAppManager.Instance.OnPurchaseClicked(IAPItems.coins_800);
                break;
            case 1000:
                InAppManager.Instance.OnPurchaseClicked(IAPItems.coins_1000);
                break;
            case 1500:
                InAppManager.Instance.OnPurchaseClicked(IAPItems.coins_1500);
                break;
        }
    }

    public void OnPurchaseGems(int gemsAmount)
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        switch (gemsAmount)
        {
            case 200:
                InAppManager.Instance.OnPurchaseClicked(IAPItems.gems_200);
                break;
            case 300:
                InAppManager.Instance.OnPurchaseClicked(IAPItems.gems_300);
                break;
            case 500:
                InAppManager.Instance.OnPurchaseClicked(IAPItems.gems_500);
                break;
            case 1000:
                InAppManager.Instance.OnPurchaseClicked(IAPItems.gems_1000);
                break;
        }
    }

    public void OnPurchaseBundle(string bundleName)
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();


        switch (bundleName)
        {
            case "master":
                InAppManager.Instance.OnPurchaseClicked(IAPItems.master_bundle);
                break;
            case "mega":
                InAppManager.Instance.OnPurchaseClicked(IAPItems.mega_bundle);
                break;
            case "legendary":
                InAppManager.Instance.OnPurchaseClicked(IAPItems.legendary_bundle);
                break;
            case "characters":
                InAppManager.Instance.OnPurchaseClicked(IAPItems.unlock_characters);
                break;
            default:
                break;
        }
    }

    public void OnRestoreButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        InAppManager.Instance.OnRestorPurhcases();
    }
}
