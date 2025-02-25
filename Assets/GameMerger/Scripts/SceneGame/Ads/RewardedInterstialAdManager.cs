using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class RewardedInterstialAdManager : MonoBehaviour
{
    public static RewardedInterstialAdManager Instance;
#if UNITY_ANDROID
    private string idAdRewarded = "ca-app-pub-3940256099942544/5354046379";
#endif
    private RewardedInterstitialAd rewardedAd;

    private void Awake()
    {
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
    }

    public void LoadAd()
    {
        if (rewardedAd != null)
        {
            this.DestroyAd();
        }
        var adRequest = new AdRequest();
        RewardedInterstitialAd.Load(idAdRewarded, adRequest, (RewardedInterstitialAd ad, LoadAdError adError) =>
        {
            if (ad == null)
            {
                Debug.LogError("ad null");
            }
            if (adError != null)
            {
                Debug.LogError("Rewarded ad load fail with error " + adError);
            }

            rewardedAd = ad;
            this.RegisterEventAd(ad);
            this.ShowAd();
        });
    }

    private void ShowAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                Debug.Log("ok");
                GameOver.Instance.SceneTransition.RestartLevel();
                GameStateController.Instance.CurrentGameState = GameState.Dragging;
            });
        }
    }

    private void DestroyAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
    }

    private void RegisterEventAd(RewardedInterstitialAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded Ad Interstial {0} {1}", adValue.Value, adValue.CurrencyCode));
        };

        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded Ad Interstial was Clicked");
        };

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded Ad Interstial Opened");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded Ad Interstial Closed");
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.Log("Rewarded Ad Interstial load fail with error " + error);
        };

        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded Ad Interstial Recorded");
        };
    }
}
