using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_IOS	
using UnityEngine.iOS;
#endif
public class RateusPanel : MonoBehaviour
{

    private void OnEnable()
    {
        if (AdsManager.Instance)
            AdsManager.Instance.ShowBannerAd();
    }

    private void OnDisable()
    {
        if (AdsManager.Instance)
            AdsManager.Instance.HideBannerAd();
    }

    public void OnRateusButtonPressed()
    {
        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();

        InGameReviewManager.Instance.RateAndReview();
    }

    public void OnLaterButtonPressed()
    {
        this.gameObject.SetActive(false);

        if (SFXManager.Instance)
            SFXManager.Instance.PlayClickSfx();
    }
}
