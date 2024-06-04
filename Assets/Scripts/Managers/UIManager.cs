using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameplayPanel;
    public GameObject pausePanel;
    public GameObject quitPanel;
    public GameObject levelFailedPanel;
    public GameObject levelCompletedPanel;
    public GameObject upgradesPanel;
    public GameObject gameCompletedPanel;

    public GameObject enemyCounter;

    [Space(10)]
    [Header("Texts")]
    public TextMeshProUGUI stageNumberText;
    public TextMeshProUGUI enemyCounterText;

    public Image healthBarFillImage;

    public static UIManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        SetStageNumber();

        if (TutorialManager.Instance)
            enemyCounter.SetActive(false);
    }

    private void SetStageNumber()
    {
        stageNumberText.text = $"Stage {Level.Instance.levelNumber}";
    }

    public void OnPauseButtonPressed()
    {
        if (Level.Instance.numberOfEnemiesInThisLevel <= 0) return;

        //turn off gameplay panel
        gameplayPanel.SetActive(false);

        //show pause panel when this button is clicked
        //set time scale to 0
        Time.timeScale = 0f;

        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        pausePanel.SetActive(true);
    }
}
