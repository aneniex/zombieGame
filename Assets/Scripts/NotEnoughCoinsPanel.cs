using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoughCoinsPanel : MonoBehaviour
{
    [SerializeField] private GameObject safeAreaCanvas;

    private void OnEnable()
    {
        safeAreaCanvas.SetActive(false);
    }

    private void OnDisable()
    {
        safeAreaCanvas.SetActive(true);
    }
}
