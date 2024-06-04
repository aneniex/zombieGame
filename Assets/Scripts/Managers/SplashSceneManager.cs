using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashSceneManager : MonoBehaviour
{
    public GameObject privacyPanel;
    public GameObject loadingPanel;

    [SerializeField]
    private TextMeshProUGUI loadingText;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsHelper.PrivacyPolicy) || PlayerPrefs.GetInt(PlayerPrefsHelper.PrivacyPolicy) == 0)
        {
            PlayerPrefs.SetInt(PlayerPrefsHelper.PrivacyPolicy, 0);
            privacyPanel.SetActive(true);
            loadingPanel.SetActive(false);
        }

        if (PlayerPrefs.GetInt(PlayerPrefsHelper.PrivacyPolicy) == 1)
        {
            loadingPanel.SetActive(true);
            AnimateLoadingText();
        }


        var numberOfTimesAppOpened = PlayerPrefs.GetInt(PlayerPrefsHelper.AppOpened, 0);
        numberOfTimesAppOpened++;
        PlayerPrefs.SetInt(PlayerPrefsHelper.AppOpened, numberOfTimesAppOpened);
    }

    public void AcceptPrivacyPanel()
    {
        PlayerPrefs.SetInt(PlayerPrefsHelper.PrivacyPolicy, 1);
        privacyPanel.SetActive(false);
        loadingPanel.SetActive(true);
        AnimateLoadingText();
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://sites.google.com/view/angrypanda/privacy-policy");
    }

    private void AnimateLoadingText()
    {
        DOTween.To(() => 0, x => loadingText.text = x.ToString() + "%", 100, 3.2f)
            .SetEase(Ease.Linear)
            .OnUpdate(() => loadingText.ForceMeshUpdate()).OnComplete(() =>
            {
                SceneManager.LoadScene(1);
            });
    }
}
