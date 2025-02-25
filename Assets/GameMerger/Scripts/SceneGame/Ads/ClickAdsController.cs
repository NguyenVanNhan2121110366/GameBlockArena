using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickAdsController : MonoBehaviour
{
    #region Variable
    [SerializeField] private Button bntClickAds;
    private bool isCheckClickRewarded;
    #endregion
    #region Public
    public static ClickAdsController Instance;
    public bool IsCheckClickRewarded { get => isCheckClickRewarded; set => isCheckClickRewarded = value; }
    #endregion
    private void Awake()
    {
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
        if (bntClickAds == null) bntClickAds = GameObject.Find("bntAds").GetComponent<Button>();
        bntClickAds.onClick.AddListener(ClickRewarded);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ClickRewarded()
    {
        isCheckClickRewarded = true;
        RewardedAdManager.Instance.LoadRewardedAd();
    }


}
