using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompletePanel : MonoBehaviour
{
    public void OnHomeButtonPressed()
    {
        //set time scale to 1
        Time.timeScale = 1.0f;

        //restart the current level
        SceneManager.LoadScene(1);

        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();
    }

    public void OnResetButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        PlayerPrefs.DeleteKey(PlayerPrefsHelper.CurrentLevel);

        //set time scale to 1
        Time.timeScale = 1.0f;

        //restart the current level
        SceneManager.LoadScene(1);
    }

}
