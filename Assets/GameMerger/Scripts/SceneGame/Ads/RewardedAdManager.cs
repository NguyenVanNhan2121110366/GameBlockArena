using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using DG.Tweening;
using UnityEngine.SocialPlatforms.Impl;
public class RewardedAdManager : MonoBehaviour
{
    public static RewardedAdManager Instance;
#if UNITY_ANDROID
    private string rewardedId = "ca-app-pub-3940256099942544/5224354917";
#endif
    private RewardedAd rewardedAd;

    private void Awake()
    {
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
    }

    public void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            this.DestroyAd();
        }

        var adRequest = new AdRequest();
        RewardedAd.Load(rewardedId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                Debug.Log("Rewarded ad load fail with error : " + error);
                return;
            }

            if (ad == null)
            {
                Debug.Log("Rewarded ad null");
                return;
            }
            Debug.Log("Load ad is success");
            rewardedAd = ad;
            //Need check again
            if (ClickAdsController.Instance.IsCheckClickRewarded)
            {
                this.ShowAdWatchVideoPlusDiamon(100);
                ClickAdsController.Instance.IsCheckClickRewarded = false;
            }
            if (NextGame.Instance != null)
            {
                if (NextGame.Instance.IsCheckRewardedAd)
                    this.ShowManyTimesReward();
            }
            //You need test
            if (ReceiveDailyGift.Instance.IsCheckDailyGift)
            {
                this.ShowAdWatchVideoPlusDiamon(ReceiveDailyGift.Instance.CheckDiamonByDate());
                Debug.Log(ReceiveDailyGift.Instance.CheckDiamonByDate());
                // ReceiveDailyGift.Instance.CheckDayToReceiveGift(ReceiveDailyGift.Instance.CurrentDate);
                ReceiveDailyGift.Instance.IsCheckDailyGift = false;
            }
            this.RegisterRewardedAd(ad);
        });
    }

    private void ShowAdWatchVideoPlusDiamon(int amount)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            Debug.Log("Rewarded ad showing");
            rewardedAd.Show((Reward reward) =>
            {
                reward.Amount = amount;
                HandleRewardedAd(reward);

            });
        }

    }

    private void ShowManyTimesReward()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                reward.Amount = LuckySpinManager.Instance.ManyTimes();
                ScoreUIPlayGame.Instance.IntermediateDiamon += (int)reward.Amount;
                this.HandleManyTimeRewardedAd(reward);
                StartCoroutine(NextGame.Instance.DelayClickNextGame());
            });
        }
        NextGame.Instance.IsCheckRewardedAd = false;
    }

    private void HandleManyTimeRewardedAd(Reward reward)
    {
        var numberDemo = (int)reward.Amount;
        var originScoreDiamon = numberDemo - ScoreUIPlayGame.Instance.CountDiamon;
        DataGame.Instance.dataSave.Diamon[0] += (int)reward.Amount;
        StartCoroutine(this.AnimateCountTimeReward(originScoreDiamon));
    }

    private IEnumerator AnimateCountTimeReward(int originScoreDiamon)
    {
        var count = ScoreUIPlayGame.Instance.CountDiamon;
        for (var i = 0; i < originScoreDiamon; i++)
        {
            count++;
            ScoreUIPlayGame.Instance.TxtCountDiamon.text = "+ " + count.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void HandleRewardedAd(Reward reward)
    {
        DataGame.Instance.dataSave.Diamon[0] += (int)reward.Amount;
        DataGame.Instance.SaveData();
        if (ScoreUIPlayGame.Instance != null)
        {
            ScoreUIPlayGame.Instance.TxtDiamonPlayGame.text = DataGame.Instance.dataSave.Diamon[0].ToString();
            ScoreUIPlayGame.Instance.IntermediateDiamon = DataGame.Instance.dataSave.Diamon[0];
        }
        if (UIHomeController.Instance != null)
            UIHomeController.Instance.TxtDiamon.text = DataGame.Instance.dataSave.Diamon[0].ToString();
    }

    private void DestroyAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
    }

    private void RegisterRewardedAd(RewardedAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("OnadPaid {0} {1}", adValue.Value, adValue.CurrencyCode));
        };

        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded was clicked");
        };

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded OnAdFullScreenContentOpened");
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.Log("Rewarded load fail with error : " + error);
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded OnAdFullScreenContentClosed");
        };

        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded OnAdImpressionRecorded");
        };
    }
}
