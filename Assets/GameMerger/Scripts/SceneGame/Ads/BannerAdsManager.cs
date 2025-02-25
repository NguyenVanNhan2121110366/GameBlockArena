using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using TMPro;
public class BannerAdsManager : MonoBehaviour
{
    public static BannerAdsManager Instance;
#if UNITY_ANDROID
    //test
    private string bannerId = "ca-app-pub-3940256099942544/6300978111";
#endif
    private BannerView bannerView;

    private void Awake()
    {
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
    }

    private void Start()
    {
        this.LoadBannner();
    }
    private void CreateBanner()
    {
        if (bannerView != null)
        {
            this.DestroyBanner();
        }
        bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
        Debug.Log("Create banner view");
        this.RegisterEventBannerView();

    }

    private void LoadBannner()
    {
        if (bannerView == null)
        {
            CreateBanner();
        }
        var adRequest = new AdRequest();
        bannerView.LoadAd(adRequest);
        //StartCoroutine(ShowBanner());
    }

    private void ShowBanner()
    {
        if (bannerView != null)
        {
            bannerView.Show();
            Debug.Log("Load banner");
        }
    }

    public void DestroyBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }
    }

    private void RegisterEventBannerView()
    {
        bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked");
        };

        bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view OnAdFullScreenContentOpened");
        };

        bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view OnAdFullScreenContentClosed");
        };

        bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view OnAdImpressionRecorded");
        };

        bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("banner view {0} {1}", adValue.Value, adValue.CurrencyCode));
        };

        bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view load is success");
            this.ShowBanner();
        };

        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.Log("Banner view load fail with error : " + error);
        };
    }
}
