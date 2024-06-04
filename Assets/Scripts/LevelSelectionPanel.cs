using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionPanel : MonoBehaviour
{
    [SerializeField] public ScrollRect scrollRect;

    [SerializeField]
    private Button[] levelButtons;

    private void OnEnable()
    {
        var currentLevelUnlocked = PlayerPrefs.GetInt(PlayerPrefsHelper.CurrentLevel);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i <= currentLevelUnlocked)
            {
                levelButtons[i].interactable = true;
                levelButtons[i].gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                levelButtons[i].interactable = false;
                levelButtons[i].gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
        }

        if(currentLevelUnlocked >=2 && currentLevelUnlocked < 6)
        {
            scrollRect.horizontalNormalizedPosition = 0.35f;
        }

        else if(currentLevelUnlocked >= 6)
        {
            scrollRect.horizontalNormalizedPosition = 1f;
        }
        else
        {
            scrollRect.horizontalNormalizedPosition = 0f;

        }
    }

    public void OnBackButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        this.gameObject.SetActive(false);
        MainMenuManager.Instance.mainMenuPanel.SetActive(true);
    }

    public void OnLevelButtonPressed(int levelIndex)
    {
        AdditiveSceneManager.levelNumber = levelIndex;

        SceneManager.LoadScene(2);
    }
}
