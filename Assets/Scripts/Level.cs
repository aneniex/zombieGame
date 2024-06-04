using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int levelNumber = 0;
    public int numberOfEnemiesInThisLevel = 2;
    public bool isLevelCompleted = false;
    public bool isLevelFailed = false;

    public static Level Instance;

    public float totalTime = 2f; // Total time for the animation
    public float intervalTime = 2f; // Time interval for lerping
    private float startTime;

    public int gemsRequiredForReviving = 20;
    public int coinsForThisLevel = 100;
    public int gemsForThisLevel = 25;

    public int enemiesKilled = 0;

    private void Awake()
    {
        if (Instance == null)
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
        if (FirebaseInit.Instance != null)
            FirebaseInit.Instance.LogFirebaseEvent($"level_started_{levelNumber}");
    }

    private void Update()
    {
        if (isLevelCompleted)
        {
            Time.timeScale = Mathf.Lerp(1f, 0.5f, 2f);
        }

        if (isLevelFailed)
        {
            Time.timeScale = Mathf.Lerp(1f, 0.5f, 2f);
        }

    }


    public void OnLevelCompletedEvent()
    {
        isLevelCompleted = true;
        Debug.Log("LEVEL COMPLETED");
        Invoke(nameof(DelayInLevelCompeleted), 2f);

        // Use DoTween to lerp the field of view
        Camera.main.DOFieldOfView(45, 1.5f)
            .SetEase(Ease.Linear);
    }

    public void OnLevelFailedEvent()
    {
        isLevelFailed = true;

        // Use DoTween to lerp the field of view
        Camera.main.DOFieldOfView(45, 1.5f)
            .SetEase(Ease.Linear).OnComplete(() =>
            {
                // Use DoTween to lerp the field of view
                Camera.main.DOFieldOfView(60, 0.25f)
                    .SetEase(Ease.Linear).OnComplete(() =>
                    {
                        //sshow level failed panel
                        UIManager.Instance.levelFailedPanel.SetActive(true);

                        if (AdsManager.Instance)
                        {
                            AdsManager.Instance.ShowInterstitialAd();
                        }
                    });

                isLevelFailed = false;

                Time.timeScale = 1f;
            });
    }

    private void DelayInLevelCompeleted()
    {
        isLevelCompleted = false;
        Time.timeScale = 1f;

        // Use DoTween to lerp the field of view
        Camera.main.DOFieldOfView(60, 0.25f)
            .SetEase(Ease.Linear).OnComplete(() =>
            {
                //show level complete panel
                UIManager.Instance.levelCompletedPanel.SetActive(true);

                if (AdsManager.Instance)
                {
                    AdsManager.Instance.ShowInterstitialAd();
                }
            });
    }
}
