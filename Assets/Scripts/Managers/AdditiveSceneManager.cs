using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneManager : MonoBehaviour
{
    [SerializeField]
    private string[] sceneNames;
    private int currentSceneNumber = 0;

    public GameObject gameCompletedPanel;

    public static int levelNumber;

    public static AdditiveSceneManager Instance;

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

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(sceneNames[levelNumber]);
    }

    public void OnLoadLevelEvent()
    {
        currentSceneNumber = PlayerPrefs.GetInt(PlayerPrefsHelper.CurrentLevel, 0);

        if(currentSceneNumber >= sceneNames.Length)
        {
            Debug.Log("GAME COMPLETED");
            //load this scene additively
            gameCompletedPanel.SetActive(true);
        }
        else
        {
            //load this scene additively
            SceneManager.LoadScene(sceneNames[currentSceneNumber]);
        }
    }

}
