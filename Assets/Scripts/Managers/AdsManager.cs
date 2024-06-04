using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private GameObject adNotAvailableCanvas;

    private string testBannerAdID = "ca-app-pub-3940256099942544/6300978111";
    private string testInterstitialAdID = "ca-app-pub-3940256099942544/1033173712";
    private string testRewardedAdID = "ca-app-pub-3940256099942544/5224354917";

    public bool isTestAds;
    public static AdsManager Instance;

    [Space(10)]
    [Header("Android Ad ID")]
    public string androidBannerAdID;
    public string androidInterstitialAdID;
    public string androidRewardedAdID;

    [Space(10)]
    [Header("iOS AD ID")]
    public string iosBannerAdID;
    public string iosInterstitialAdID;
    public string iosRewardedAdID;

    private string myBannerAdID;
    private string myInterstitialAdID;
    private string myRewardedAdID;

    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    private UnityEvent OnAdLoadedEvent;
    private UnityEvent OnAdFailedToLoadEvent;
    private UnityEvent OnAdOpeningEvent;
    private UnityEvent OnAdFailedToShowEvent;
    private UnityEvent OnUserEarnedRewardEvent;
    private UnityEvent OnAdClosedEvent;

    private static bool isRewardedAdLoaded = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
    }

    private void Start()
    {
        CheckForTestAds();

        RequestBannerAd();

        RequestAndLoadInterstitialAd();

        RequestAndLoadRewardedAd();
    }



    private void CheckForTestAds()
    {
        if (isTestAds)
        {
            myBannerAdID = testBannerAdID;
            myInterstitialAdID = testInterstitialAdID;
            myRewardedAdID = testRewardedAdID;
        }
        else
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                myBannerAdID = iosBannerAdID;
                myInterstitialAdID = iosInterstitialAdID;
                myRewardedAdID = iosRewardedAdID;
            }
            else
            {
                myBannerAdID = androidBannerAdID;
                myInterstitialAdID = androidInterstitialAdID;
                myRewardedAdID = androidRewardedAdID;
            }
        }
    }


    #region BannerAdRegion

    public void RequestBannerAd()
    {
        string adUnitId = myBannerAdID;

        // Clean up banner before reusing
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        // Create a 320x50 banner at top of the screen
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.TopLeft);

        // Load a banner ad
        bannerView.LoadAd(CreateAdRequest());

        bannerView.Hide();
    }

    public void ShowBannerAd()
    {
        bannerView.Show();
    }

    public void HideBannerAd()
    {
        bannerView.Hide();
    }

    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }

    #endregion


    #region InterstitialAdRegion

    public void RequestAndLoadInterstitialAd()
    {
        string adUnitId = myInterstitialAdID;

        // Clean up interstitial before using it
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }

        // Load an interstitial ad
        InterstitialAd.Load(adUnitId, CreateAdRequest(),
            (InterstitialAd ad, LoadAdError loadError) =>
            {
                if (loadError != null)
                {
                    return;
                }
                else if (ad == null)
                {
                    return;
                }

                interstitialAd = ad;

                ad.OnAdFullScreenContentOpened += () =>
                {
                    Debug.Log("Interstitial Ad Opened");
                };
                ad.OnAdFullScreenContentClosed += () =>
                {
                    Debug.Log("Interstitial Ad Closed");
                    RequestAndLoadInterstitialAd();
                };
                ad.OnAdImpressionRecorded += () =>
                {
                };
                ad.OnAdClicked += () =>
                {
                };
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {

                };
                ad.OnAdPaid += (AdValue adValue) =>
                {
                    string msg = string.Format("{0} (currency: {1}, value: {2}",
                                               "Interstitial ad received a paid event.",
                                               adValue.CurrencyCode,
                                               adValue.Value);
                };
            });
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
        else
        {
            RequestAndLoadInterstitialAd();
        }
    }

    public void DestroyInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }

    #endregion


    #region RewardedAdRegion

    public void RequestAndLoadRewardedAd()
    {
        if (isRewardedAdLoaded) return;
        string adUnitId = myRewardedAdID;

        Debug.Log("Loading rewarded ad.");

        // Load an interstitial ad
        RewardedAd.Load(adUnitId, CreateAdRequest(),
            (RewardedAd ad, LoadAdError loadError) =>
            {
                if (loadError != null)
                {
                    Debug.Log("Loading success");
                    return;
                }
                else if (ad == null)
                {
                    return;
                }

                rewardedAd = ad;
                isRewardedAdLoaded = true;
                RegisterEventHandlers(ad);
            });

    }

    public void ShowRewardedAd(UnityAction rewardAction)
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
                rewardAction?.Invoke();
            });
        }
        else
        {
            isRewardedAdLoaded = false;
            adNotAvailableCanvas.SetActive(true);
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when the ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            isRewardedAdLoaded = false;
            Debug.Log("Rewarded ad full screen content closed.");
            RequestAndLoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content with error : "
                + error);
        };
    }

    #endregion


    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();
    }
}
