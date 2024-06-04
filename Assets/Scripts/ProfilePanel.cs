using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class ProfilePanel : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject imageSelector;

    public float[] selectorXValues;

    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsHelper.CurrentDisplayImage))
        {
            PlayerPrefs.SetInt(PlayerPrefsHelper.CurrentDisplayImage, 0);
        }

        var currentImageSelected = PlayerPrefs.GetInt(PlayerPrefsHelper.CurrentDisplayImage);
        imageSelector.transform.DOLocalMoveX(selectorXValues[currentImageSelected], 0.25f);

    }

    public void OnCloseButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        this.gameObject.SetActive(false);
    }

    public void OnSaveButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();


        if (inputField.text != "")
        {
            //set the name in player prefs
            PlayerPrefs.SetString(PlayerPrefsHelper.PlayerName, inputField.text);

            MainMenuManager.Instance.playerNameText.text = inputField.text;
        }

        Debug.Log(inputField.text);

        string stringWithoutSpaces = Regex.Replace(inputField.text, @"\s+", "");

        this.gameObject.SetActive(false);

        if (FirebaseInit.Instance == null) return;
        //log firebase event
        FirebaseInit.Instance.LogFirebaseEvent($"player_name_{stringWithoutSpaces.ToLower()}");

        FirebaseInit.Instance.LogFirebaseEventWithParam("player_name", "name", stringWithoutSpaces.ToLower());
    }

    public void UpdateSelector(int index)
    {
        imageSelector.transform.DOLocalMoveX(selectorXValues[index], 0.25f);
    }
}
