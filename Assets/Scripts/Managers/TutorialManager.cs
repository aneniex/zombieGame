using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public bool isTutorialStarted = false;

    public GameObject joystickImage;
    public GameObject attackImage;

    public static TutorialManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        else
        {
            Destroy(this.gameObject
                );
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        joystickImage.SetActive(false);
        attackImage.SetActive(false);

        StartCoroutine(StartTutorial());
    }

    private IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
        isTutorialStarted = true;
        joystickImage.SetActive(true);
    }

    public void OnNextFromJoystickButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        joystickImage.SetActive(false);

        Time.timeScale = 1;

        StartCoroutine(EnableAttackButton());
    }

    public void OnNextFromAttackButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        joystickImage.SetActive(false);
        attackImage.SetActive(false);

        Time.timeScale = 1;
    }

    public IEnumerator EnableAttackButton()
    {
        yield return new WaitForSecondsRealtime(1f);
        attackImage.SetActive(true);
        Time.timeScale = 0;
    }
}
