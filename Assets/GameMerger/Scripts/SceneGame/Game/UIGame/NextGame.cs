using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class NextGame : ClickButtonManager
{
    private bool isCheckRewardedAd;
    [SerializeField] private int count;
    [SerializeField] private RectTransform winGameUI;
    public bool IsCheckRewardedAd { get => isCheckRewardedAd; set => isCheckRewardedAd = value; }
    public static NextGame Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
        this.SceneTransition = FindFirstObjectByType<SceneTransition>();
        this.gridController = FindFirstObjectByType<GridController>();
        this.CheckButtonHome("bntWinHome");
        this.BntNoThank.onClick.AddListener(this.OnClickCheckAdNextGame);
        this.BntHome.onClick.AddListener(this.OnClickHome);
        this.BntRewarded.onClick.AddListener(this.OnClickAds);
    }

    private void Start()
    {
        count = 0;
    }

    public override void CheckButtonHome(string name)
    {
        if (BntHome == null) BntHome = GameObject.Find(name).GetComponent<Button>(); else return;
    }

    private void SetActiveFalseComponentLuckyManager()
    {
        var obj = GameObject.Find("LuckySpinManager");
        if (obj)
            obj.GetComponent<LuckySpinManager>().enabled = false;
    }

    public override void OnClickNextGame()
    {
        //SettingGame.Instance.HandleVibrate();
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
        this.SetActiveFalseComponentLuckyManager();
        winGameUI.DOAnchorPosY(1660, 0.5f).OnComplete(() =>
        {
            ScoreWinGame.Instance.FillWinAndLose.SetActive(false);
            DataGame.Instance.dataSave.Diamon[0] = ScoreUIPlayGame.Instance.IntermediateDiamon;
            this.gridController.TurnOnAllGrid();
            this.SceneTransition.NextLevel();
            ScoreUIPlayGame.Instance.CountDiamon = 0;
            PlayerPrefs.SetInt("DiamonWinGame", ScoreUIPlayGame.Instance.IntermediateDiamon);
            GameStateController.Instance.CurrentGameState = GameState.Dragging;
        });
    }

    public void OnClickCheckAdNextGame()
    {
        if (isCheckRewardedAd)
        {
            OnClickNextGame();
        }
        else
        {
            if (count < 3)
            {
                OnClickNextGame();
                if (GameController.Instance.Level > 4)
                {
                    count++;
                }
            }
            else
            {
                InterStialAdManager.Instance.LoadAd();
                Debug.Log("vao day");
                count = 0;
            }

        }
    }


    public IEnumerator DelayClickNextGame()
    {
        PlayerPrefs.SetInt("DiamonWinGame", ScoreUIPlayGame.Instance.IntermediateDiamon);
        ScoreUIPlayGame.Instance.TxtDiamonPlayGame.text = DataGame.Instance.dataSave.Diamon[0].ToString();
        yield return new WaitForSeconds(2f);
        this.OnClickNextGame();
    }

    public override void OnClickAds()
    {
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
        LuckySpinManager.Instance.IsStop = true;
        isCheckRewardedAd = true;
        base.OnClickAds();
        RewardedAdManager.Instance.LoadRewardedAd();
    }
}
