using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CustomizationPanel : MonoBehaviour
{
    [Header("Texts")]
    public TextMeshProUGUI coinsDisplayText;

    [Header("Tab Buttons")]
    public GameObject characterSelectedButton;
    public GameObject characterUnselectedButton;
    public GameObject swordsSelectedButton;
    public GameObject swordsUnselectedButton;

    [Header("Panels")]
    public GameObject charactersPanel;
    public GameObject swordsPanel;
    public GameObject notEnoughCoinsPanel;

    public List<int> playerUnlockStatus = new List<int>();
    public List<GameObject> unlockButtons = new List<GameObject>();
    public List<GameObject> selectedButtons = new List<GameObject>();
    public List<GameObject> selectButtons = new List<GameObject>();

    [SerializeField]
    private List<int> charactersPrices = new List<int>();

    private void Start()
    {
        characterSelectedButton.SetActive(true);
        characterUnselectedButton.SetActive(false);
        swordsSelectedButton.SetActive(false);
        swordsUnselectedButton.SetActive(true);

        charactersPanel.SetActive(true);
        swordsPanel.SetActive(false);

        var player0Status = PlayerPrefs.GetInt(PlayerPrefsHelper.Player_0);
        playerUnlockStatus.Add(player0Status);
        var player1Status = PlayerPrefs.GetInt(PlayerPrefsHelper.Player_1);
        playerUnlockStatus.Add(player1Status);

        var player2Status = PlayerPrefs.GetInt(PlayerPrefsHelper.Player_2, 0);
        playerUnlockStatus.Add(player2Status);
        var player3Status = PlayerPrefs.GetInt(PlayerPrefsHelper.Player_3, 0);
        playerUnlockStatus.Add(player3Status);

        var player4Status = PlayerPrefs.GetInt(PlayerPrefsHelper.Player_4, 0);
        playerUnlockStatus.Add(player4Status);
        var player5Status = PlayerPrefs.GetInt(PlayerPrefsHelper.Player_5, 0);
        playerUnlockStatus.Add(player5Status);

        var player6Status = PlayerPrefs.GetInt(PlayerPrefsHelper.Player_6, 0);
        playerUnlockStatus.Add(player6Status);
        var player7Status = PlayerPrefs.GetInt(PlayerPrefsHelper.Player_7, 0);
        playerUnlockStatus.Add(player7Status);

        for (int i = 0; i < playerUnlockStatus.Count; i++)
        {
            if (playerUnlockStatus[i] == 0)
            {
                unlockButtons[i].SetActive(true);
            }
            else
            {
                unlockButtons[i].SetActive(false);
                selectButtons[i].SetActive(true);
            }
        }

        for (int i = 0; i < selectedButtons.Count; i++)
        {
            selectedButtons[i].SetActive(false);
        }

        var currentSelectedPlayer = PlayerPrefs.GetInt(PlayerPrefsHelper.SelectedPlayer, 0);
        selectedButtons[currentSelectedPlayer].SetActive(true);
    }

   

    private void OnEnable()
    {
        var totalCoins = PlayerPrefs.GetInt(PlayerPrefsHelper.Gold, 0);
        coinsDisplayText.text = totalCoins.ToString();
    }

    public void OnBackButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        this.gameObject.SetActive(false);
        MainMenuManager.Instance.mainCanvas.SetActive(true);
        notEnoughCoinsPanel.SetActive(false);
    }

    public void OnCharactersButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        charactersPanel.SetActive(true);
        swordsPanel.SetActive(false);

        characterSelectedButton.SetActive(true);
        characterUnselectedButton.SetActive(false);

        swordsUnselectedButton.SetActive(true);
        swordsSelectedButton.SetActive(false);
    }

    public void OnSwordsButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        charactersPanel.SetActive(false);
        swordsPanel.SetActive(true);

        characterSelectedButton.SetActive(false);
        characterUnselectedButton.SetActive(true);

        swordsSelectedButton.SetActive(true);
        swordsUnselectedButton.SetActive(false);
    }

    public void OnUnlockButtonPressed(int index)
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        var currentCoins = PlayerPrefs.GetInt(PlayerPrefsHelper.Gold);
        if (currentCoins< charactersPrices[index])
        {
            Debug.Log("NOT ENOUGH COINS");
            //TODO: Show Not Enough Coins Panel
            notEnoughCoinsPanel.SetActive(true);
        }
        else
        {
            playerUnlockStatus[index] = 1;
            var newPlayerUnlocked = "Player_" + index;
            PlayerPrefs.SetInt(newPlayerUnlocked, 1);

            for (int i = 0; i < playerUnlockStatus.Count; i++)
            {
                if (playerUnlockStatus[i] == 1)
                {
                    unlockButtons[i].SetActive(false);
                }

            }

            selectButtons[index].SetActive(true);
            unlockButtons[index].SetActive(false);

            currentCoins -= charactersPrices[index];
            PlayerPrefs.SetInt(PlayerPrefsHelper.Gold, currentCoins);

            //update coins text in customization and also in main menu
            coinsDisplayText.text = currentCoins.ToString();
            MainMenuManager.Instance.coinsDisplayText.text = currentCoins.ToString();
        }
    }

    public void OnSelectButtonPressed(int index)
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        for (int i = 0; i < selectButtons.Count; i++)
        {
            selectButtons[i].SetActive(true);
            selectedButtons[i].SetActive(false);
        }

        selectedButtons[index].SetActive(true);
        selectButtons[index].SetActive(false);

        PlayerPrefs.SetInt(PlayerPrefsHelper.SelectedPlayer, index);
    }

    public void OnCloseNotEnoughCoinsPanel()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        notEnoughCoinsPanel.SetActive(false);
    }

    public void OnClick100FreeCoins()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        if (AdsManager.Instance)
            AdsManager.Instance.ShowRewardedAd(() =>
            {
                var currentCoins = PlayerPrefs.GetInt(PlayerPrefsHelper.Gold);
                currentCoins += 100;
                PlayerPrefs.SetInt(PlayerPrefsHelper.Gold, currentCoins);

                //update coins text in customization and also in main menu
                coinsDisplayText.text = currentCoins.ToString();
                MainMenuManager.Instance.coinsDisplayText.text = currentCoins.ToString();
            });
    }
}
