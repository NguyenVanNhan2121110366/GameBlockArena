using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveDailyGift : MonoBehaviour
{
    public static ReceiveDailyGift Instance;
    private int currentDate;
    [SerializeField] private bool isCheckDailyGift;

    private UIReceiveDailyGiftManager uIReceiveDailyGiftManager;
    private string lastLoginKey = "LastLogin";
    private string currentDayKey = "CurrentDay";
    private string receiveGiftKey = "ReceiveGift";
    [SerializeField] private GameObject tableDailyGift;
    [SerializeField] private Button bntReceiveNormally;
    [SerializeField] private Button bntReceiveAds;
    [SerializeField] private RectTransform rectTableDailyGift;

    public bool IsCheckDailyGift { get => isCheckDailyGift; set => isCheckDailyGift = value; }
    public int CurrentDate { get => currentDate; set => currentDate = value; }

    private void Awake()
    {
        if (rectTableDailyGift == null) rectTableDailyGift = GameObject.Find("TableReceiveGiftNormallyOrAds").GetComponent<RectTransform>();
        if (tableDailyGift == null) tableDailyGift = GameObject.Find("TableReceiveGiftNormallyOrAds");
        if (bntReceiveNormally == null) bntReceiveNormally = GameObject.Find("bntNormally").GetComponent<Button>();
        if (bntReceiveAds == null) bntReceiveAds = GameObject.Find("bntAdsDiamon").GetComponent<Button>();
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
        this.uIReceiveDailyGiftManager = FindFirstObjectByType<UIReceiveDailyGiftManager>();
        this.CheckReceiveGift();
        bntReceiveNormally.onClick.AddListener(OnClickNormally);
        bntReceiveAds.onClick.AddListener(OnClickWatchAds);
    }

    private void Start()
    {
        isCheckDailyGift = false;
        tableDailyGift.SetActive(false);
        Debug.Log(PlayerPrefs.GetInt(currentDayKey));
    }

    private void Update()
    {

    }

    private void CheckReceiveGift()
    {
        var lastLogin = PlayerPrefs.GetString(lastLoginKey, "");
        currentDate = PlayerPrefs.GetInt(currentDayKey, 1);
        var receiveGift = PlayerPrefs.GetInt(receiveGiftKey, 0) == 1;
        Debug.Log(receiveGift);
        var today = DateTime.Now.Date;
        if (!string.IsNullOrEmpty(lastLogin))
        {
            var lastLoginToday = DateTime.Parse(lastLogin);
            if (today > lastLoginToday)
            {
                receiveGift = false;
                if ((today - lastLoginToday).Days == 1)
                {
                    currentDate = Mathf.Clamp(currentDate + 1, 1, 7);
                }
                else
                {
                    currentDate = 1;
                }
            }
        }
        if (receiveGift)
        {
            this.CheckWasReceiveDailyGift(currentDate);
            Debug.Log("Vao day");
        }
        PlayerPrefs.SetString(lastLoginKey, today.ToString("yyyy-MM-dd"));
        PlayerPrefs.SetInt(currentDayKey, currentDate);
        PlayerPrefs.SetInt(receiveGiftKey, receiveGift ? 1 : 0);
        PlayerPrefs.Save();
        // Ngay bao nhieu thi no se bat len
        // this.CheckDayToReceiveGift(currenDate);
        if (!receiveGift)
            this.UpdateDayToReceiveGift(currentDate);
    }

    public void DailyGift()
    {
        currentDate = PlayerPrefs.GetInt(currentDayKey, 1);
        Debug.Log(currentDate);
        var receiveGift = PlayerPrefs.GetInt(receiveGiftKey, 0) == 1;
        Debug.Log(receiveGift);
        if (!receiveGift)
        {
            PlayerPrefs.Save();
            this.TableReceiveGiftNormallyOrAds();
            //this.CheckDayToReceiveGift(currenDate);
        }
        else
        {
            Debug.Log("you was receive gift today");
            // It check according current date 
        }
    }

    private void TableReceiveGiftNormallyOrAds()
    {
        tableDailyGift.SetActive(true);
        rectTableDailyGift.DOAnchorPosY(-10, 1f).OnComplete(() =>
        {

        });
    }

    private void OnClickNormally()
    {
        PlayerPrefs.SetInt(receiveGiftKey, 1);
        this.CheckDayToReceiveGift(currentDate);
        rectTableDailyGift.DOAnchorPosY(1800, 1f).OnComplete(() =>
        {
            this.tableDailyGift.SetActive(false);

            this.UpdateUINextDayToReceiveGift(currentDate);
        });
    }

    private void OnClickWatchAds()
    {
        isCheckDailyGift = true;
        RewardedAdManager.Instance.LoadRewardedAd();
        rectTableDailyGift.DOAnchorPosY(1800, 1f).OnComplete(() =>
        {
            PlayerPrefs.SetInt(receiveGiftKey, 1);
            this.tableDailyGift.SetActive(false);
        });
    }

    private void UpdateDayToReceiveGift(int currentDate)
    {
        switch (currentDate)
        {
            case 1:
                //this.uIReceiveDailyGiftManager.ObjFill[0].SetActive(false);
                this.uIReceiveDailyGiftManager.FillToReceiveGift[0].SetActive(true);
                this.uIReceiveDailyGiftManager.BntReceiveDailyGifts[0].GetComponent<Button>().enabled = true;
                break;
            case 2:
                //this.uIReceiveDailyGiftManager.ObjFill[1].SetActive(false);
                this.uIReceiveDailyGiftManager.FillToReceiveGift[1].SetActive(true);
                this.uIReceiveDailyGiftManager.BntReceiveDailyGifts[1].GetComponent<Button>().enabled = true;
                break;
            case 3:
                //this.uIReceiveDailyGiftManager.ObjFill[2].SetActive(false);
                this.uIReceiveDailyGiftManager.FillToReceiveGift[2].SetActive(true);
                this.uIReceiveDailyGiftManager.BntReceiveDailyGifts[2].GetComponent<Button>().enabled = true;
                break;
            case 4:
                this.uIReceiveDailyGiftManager.FillToReceiveGift[3].SetActive(true);
                this.uIReceiveDailyGiftManager.BntReceiveDailyGifts[3].GetComponent<Button>().enabled = true;
                break;
            default:
                this.uIReceiveDailyGiftManager.FillToReceiveGift[4].SetActive(true);
                this.uIReceiveDailyGiftManager.BntReceiveDailyGifts[4].GetComponent<Button>().enabled = true;
                break;
        }

        for (var i = currentDate - 2; i >= 0; i--)
        {
            this.uIReceiveDailyGiftManager.ObjFill[i].SetActive(true);
        }
    }

    private void UpdateUINextDayToReceiveGift(int currentDate)
    {
        switch (currentDate + 1)
        {
            case 2:
                this.uIReceiveDailyGiftManager.FillToReceiveGift[1].SetActive(true);
                break;
            case 3:
                this.uIReceiveDailyGiftManager.FillToReceiveGift[2].SetActive(true);
                break;
            case 4:
                this.uIReceiveDailyGiftManager.FillToReceiveGift[3].SetActive(true);
                break;
            default:
                this.uIReceiveDailyGiftManager.FillToReceiveGift[4].SetActive(true);
                break;
        }
    }

    public int CheckDiamonByDate()
    {
        var diamonPlus = 0;
        switch (currentDate)
        {
            case 1:
                diamonPlus = 200;
                this.uIReceiveDailyGiftManager.ObjFill[0].SetActive(true);
                this.uIReceiveDailyGiftManager.FillToReceiveGift[0].SetActive(false);
                AnimationDaillyRewarded.Instance.AnimatePlus(diamonPlus);
                AnimationDaillyRewarded.Instance.FillDiamonPlus.SetActive(true);
                break;
            case 2:
                diamonPlus = 300;
                this.uIReceiveDailyGiftManager.ObjFill[1].SetActive(true);
                this.uIReceiveDailyGiftManager.FillToReceiveGift[1].SetActive(false);
                AnimationDaillyRewarded.Instance.AnimatePlus(diamonPlus);
                AnimationDaillyRewarded.Instance.FillDiamonPlus.SetActive(true);
                break;
            case 3:
                diamonPlus = 400;
                this.uIReceiveDailyGiftManager.ObjFill[2].SetActive(true);
                this.uIReceiveDailyGiftManager.FillToReceiveGift[2].SetActive(false);
                AnimationDaillyRewarded.Instance.AnimatePlus(diamonPlus);
                AnimationDaillyRewarded.Instance.FillDiamonPlus.SetActive(true);
                break;
            case 4:
                diamonPlus = 600;
                this.uIReceiveDailyGiftManager.ObjFill[3].SetActive(true);
                this.uIReceiveDailyGiftManager.FillToReceiveGift[3].SetActive(false);
                AnimationDaillyRewarded.Instance.AnimatePlus(diamonPlus);
                AnimationDaillyRewarded.Instance.FillDiamonPlus.SetActive(true);
                break;
            default:
                diamonPlus = 1000;
                this.uIReceiveDailyGiftManager.ObjFill[4].SetActive(true);
                this.uIReceiveDailyGiftManager.FillToReceiveGift[4].SetActive(false);
                AnimationDaillyRewarded.Instance.AnimatePlus(diamonPlus);
                AnimationDaillyRewarded.Instance.FillDiamonPlus.SetActive(true);
                break;
        }
        return diamonPlus;
    }


    public void CheckDayToReceiveGift(int currentDate)
    {
        switch (currentDate)
        {
            case 1:
                this.uIReceiveDailyGiftManager.ObjFill[0].SetActive(true);
                this.uIReceiveDailyGiftManager.FillToReceiveGift[0].SetActive(false);
                AnimationDaillyRewarded.Instance.AnimatePlus(100);
                AnimationDaillyRewarded.Instance.FillDiamonPlus.SetActive(true);
                Debug.Log("Vao day 1");
                break;
            case 2:
                this.uIReceiveDailyGiftManager.ObjFill[1].SetActive(true);
                this.uIReceiveDailyGiftManager.FillToReceiveGift[1].SetActive(false);
                AnimationDaillyRewarded.Instance.AnimatePlus(150);
                AnimationDaillyRewarded.Instance.FillDiamonPlus.SetActive(true);
                break;
            case 3:
                this.uIReceiveDailyGiftManager.ObjFill[2].SetActive(true);
                this.uIReceiveDailyGiftManager.FillToReceiveGift[2].SetActive(false);
                AnimationDaillyRewarded.Instance.AnimatePlus(200);
                AnimationDaillyRewarded.Instance.FillDiamonPlus.SetActive(true);
                break;
            case 4:
                this.uIReceiveDailyGiftManager.ObjFill[3].SetActive(true);
                this.uIReceiveDailyGiftManager.FillToReceiveGift[3].SetActive(false);
                AnimationDaillyRewarded.Instance.AnimatePlus(300);
                AnimationDaillyRewarded.Instance.FillDiamonPlus.SetActive(true);
                break;
            default:
                this.uIReceiveDailyGiftManager.ObjFill[4].SetActive(true);
                this.uIReceiveDailyGiftManager.FillToReceiveGift[4].SetActive(false);
                AnimationDaillyRewarded.Instance.AnimatePlus(500);
                AnimationDaillyRewarded.Instance.FillDiamonPlus.SetActive(true);
                break;
        }
    }

    private void CheckWasReceiveDailyGift(int currentDate)
    {
        for (var i = currentDate - 1; i >= 0; i--)
        {
            this.uIReceiveDailyGiftManager.ObjFill[i].SetActive(true);
            this.uIReceiveDailyGiftManager.FillToReceiveGift[i].SetActive(false);
            Debug.Log("Kiem tra");
        }
        //Current = 2 day , but in array is 3 {0,1,2}
        if (this.uIReceiveDailyGiftManager.FillToReceiveGift[currentDate])
            this.uIReceiveDailyGiftManager.FillToReceiveGift[currentDate].SetActive(true);
    }


    public void EnableBntDayReceiveGift()
    {
        if (PlayerPrefs.GetInt(currentDayKey) == 1)
        {
            uIReceiveDailyGiftManager.BntReceiveDailyGifts[0].onClick.AddListener(DailyGift);
        }
        else if (PlayerPrefs.GetInt(currentDayKey) == 2)
        {
            uIReceiveDailyGiftManager.BntReceiveDailyGifts[1].onClick.AddListener(DailyGift);
        }
        else if (PlayerPrefs.GetInt(currentDayKey) == 3)
        {
            uIReceiveDailyGiftManager.BntReceiveDailyGifts[2].onClick.AddListener(DailyGift);
        }
        else if (PlayerPrefs.GetInt(currentDayKey) == 4)
        {
            uIReceiveDailyGiftManager.BntReceiveDailyGifts[3].onClick.AddListener(DailyGift);
        }
        else if (PlayerPrefs.GetInt(currentDayKey) == 5)
        {
            uIReceiveDailyGiftManager.BntReceiveDailyGifts[4].onClick.AddListener(DailyGift);
        }
        else if (PlayerPrefs.GetInt(currentDayKey) == 6)
        {
            uIReceiveDailyGiftManager.BntReceiveDailyGifts[5].onClick.AddListener(DailyGift);
        }
        else
        {
            uIReceiveDailyGiftManager.BntReceiveDailyGifts[6].onClick.AddListener(DailyGift);
        }
    }

}
