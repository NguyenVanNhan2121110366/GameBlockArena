using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
public class InterStialAdManager : MonoBehaviour
{
    public static InterStialAdManager Instance;

#if UNITY_ANDROID
    private string idInterstialAd = "ca-app-pub-3940256099942544/1033173712";
#endif
    private InterstitialAd interstitialAd;

    private void Awake()
    {
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
    }

    public void LoadAd()
    {
        if (interstitialAd != null)
        {
            this.DestroyAd();
        }

        var request = new AdRequest();
        InterstitialAd.Load(idInterstialAd, request, (InterstitialAd ad, LoadAdError adError) =>
        {
            if (ad == null)
            {
                Debug.Log("InterstialAd failed to load");
                return;
            }

            if (adError != null)
            {
                Debug.Log("Interstial Ad failed to load with error : " + adError);
                return;
            }
            interstitialAd = ad;
            this.ShowAd();
            this.RegisterAdEvents(ad);

        });
    }

    private void DestroyAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }
    }

    private void ShowAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
    }

    private void RegisterAdEvents(InterstitialAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("InterstitialAd OnAdPaid {0} {1}", adValue.Value, adValue.CurrencyCode));
        };

        ad.OnAdClicked += () =>
        {
            Debug.Log("You was clicked InterstitialAd");
        };

        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("InterstitialAd OnAdImpressionRecorded");
        };

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("InterstitialAd OnAdFullScreenContentOpened");
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.Log("InteerstitialAd failed to show with error : " + error);
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("InterstitialAd OnAdFullScreenContentClosed");
            NextGame.Instance.OnClickNextGame();
        };
    }
}
