using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;
using TMPro;
public class GameOver : ClickButtonManager
{
    [SerializeField] private Button bntYes;
    [SerializeField] private Button bntNo;
    private RectTransform rectGameOver;
    private RectTransform rectAttention;
    private GameObject objAttention;
    public static GameOver Instance;
    private Button bntAds;
    [SerializeField] private Image igmCountSeconds;
    [SerializeField] private TextMeshProUGUI txtSecond;
    [SerializeField] private int seconds;
    private void Awake()
    {
        if (bntAds == null) bntAds = GameObject.Find("bntAdsGameOver").GetComponent<Button>();
        if (rectGameOver == null) rectGameOver = GameObject.Find("GameOver").GetComponent<RectTransform>();
        if (objAttention == null) objAttention = GameObject.Find("Attention");
        if (rectAttention == null) rectAttention = GameObject.Find("Attention").GetComponent<RectTransform>();
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
        this.SceneTransition = FindFirstObjectByType<SceneTransition>();
        //this.CheckButtonHome("bntLoseHome");
        this.CheckButtonContinue();
        this.CheckButtonNoThank();
        this.CheckButtonNo();
        this.CheckButtonYes();
        //this.BntHome.onClick.AddListener(OnClickHome);
        this.BntContinue.onClick.AddListener(OnClickContinue);
        this.BntNoThank.onClick.AddListener(OnClickNoThank);
        this.bntYes.onClick.AddListener(OnClickYes);
        this.bntNo.onClick.AddListener(OnClickNo);
        bntAds.onClick.AddListener(OnClickAds);

    }
    private void Start()
    {
        this.objAttention.SetActive(false);
    }

    public override void CheckButtonHome(string name)
    {
        if (BntHome == null) BntHome = GameObject.Find(name).GetComponent<Button>();
        else Debug.Log("Not null");
    }

    private void CheckButtonNoThank()
    {
        if (BntNoThank == null) BntNoThank = GameObject.Find("bntNoThanks").GetComponent<Button>();
    }

    private void CheckButtonContinue()
    {
        if (BntContinue == null) BntContinue = GameObject.Find("bntContinue").GetComponent<Button>(); else return;
    }

    private void CheckButtonYes()
    {
        if (bntYes == null) bntYes = GameObject.Find("bntYes").GetComponent<Button>(); else Debug.LogWarning("bntYes not null");
    }

    private void CheckButtonNo()
    {
        if (bntNo == null) bntNo = GameObject.Find("bntNo").GetComponent<Button>(); else Debug.LogWarning("bntNo not null");
    }


    public override void OnClickNoThank()
    {
        //SettingGame.Instance.HandleVibrate();
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
        objAttention.SetActive(true);
        EndGameController.Instance.ObjGameOver.SetActive(false);
        rectAttention.DOAnchorPosY(-10, 0.5f).OnComplete(() =>
        {

        });
    }


    public override void OnClickContinue()
    {
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
        this.rectGameOver.DOAnchorPosY(-1660, 0.5f).OnComplete(() =>
        {
            ScoreWinGame.Instance.FillWinAndLose.SetActive(false);
            if (ScoreUIPlayGame.Instance.IntermediateDiamon >= 200)
            {
                PlayerPrefs.SetInt("ClickContinue", 1);
                PlayerPrefs.Save();
                ScoreUIPlayGame.Instance.IntermediateDiamon -= 200;
            }
            else
                Debug.Log("Khong du diamon");

            SceneTransition.RestartLevel();
            GameStateController.Instance.CurrentGameState = GameState.Dragging;
            EndGameController.Instance.ObjGameOver.SetActive(false);
            //RewardedInterstialAdManager.Instance.LoadAd();
        });
    }

    private void OnClickYes()
    {
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
        rectAttention.DOAnchorPosY(-1500, 0.5f).OnComplete(() =>
        {
            ScoreWinGame.Instance.FillWinAndLose.SetActive(false);
            GameStateController.Instance.CurrentGameState = GameState.Dragging;
            SceneTransition.ReturnToLevel1();
            objAttention.SetActive(false);
        });
    }

    private void OnClickNo()
    {
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
        rectAttention.DOAnchorPosY(-1500, 0.5f).OnComplete(() =>
       {
           EndGameController.Instance.ObjGameOver.SetActive(true);
           objAttention.SetActive(false);
       });
    }

    public override void OnClickAds()
    {
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
        base.OnClickAds();
        PlayerPrefs.SetInt("ClickContinue", 1);
        PlayerPrefs.Save();
        RewardedInterstialAdManager.Instance.LoadAd();

        rectGameOver.DOAnchorPosY(-1600, 0.5f).OnComplete(() =>
        {
            ScoreWinGame.Instance.FillWinAndLose.SetActive(false);
            EndGameController.Instance.ObjGameOver.SetActive(false);
        });
    }
}
