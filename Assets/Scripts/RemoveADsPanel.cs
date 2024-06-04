using RapturePlay;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemoveADsPanel : MonoBehaviour
{
    public TextMeshProUGUI removeAdsPriceText;

    private void Start()
    {
        if (InAppManager.Instance != null)
            removeAdsPriceText.text = InAppManager.Instance.GetProductPriceFromStore(IAPItems.remove_ads);
    }

    public void OnRemoveAdsButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        InAppManager.Instance.OnPurchaseClicked(IAPItems.remove_ads);
    }

    public void OnLaterButtonPressed()
    {
        this.gameObject.SetActive(false);

        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();
    }
}
