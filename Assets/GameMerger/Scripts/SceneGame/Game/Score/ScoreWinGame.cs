using TMPro;
using UnityEngine;
public class ScoreWinGame : ScoreController
{
    public static ScoreWinGame Instance;
    [SerializeField] private TextMeshProUGUI txtHighScore;
    private GameObject fillWinAndLose;
    public GameObject FillWinAndLose { get => fillWinAndLose; set => fillWinAndLose = value; }

    private void Awake()
    {
        if (fillWinAndLose == null) fillWinAndLose = GameObject.Find("fillWinAndLose");
        if (Instance == null) Instance = this; else Debug.LogError("Intance not null");
    }
    private void Start()
    {
        this.fillWinAndLose.SetActive(false);
        txtHighScore.text = DataGame.Instance.dataSave.HightScore[0].ToString();
        ObserverManager.Instance.AddObserver(this);
    }

    private void OnDisable()
    {
        ObserverManager.Instance.RemoveObserver(this);
    }


    public override void UpdateScoreUI()
    {
        ScoreObj = ObserverManager.Instance.ShareScoreObj;
        TxtScoreObj.text = ScoreObj.ToString();
    }
    public override void UpdateScoreDiamon()
    {

    }

    public override void UpdateHighScoreUI()
    {
        if (GameStateController.Instance.CurrentGameState == GameState.WinGame)
        {
            if (DataGame.Instance.dataSave.HightScore[0] <= ScoreObj)
            {
                HighScoreObj = ScoreObj;
                DataGame.Instance.dataSave.HightScore[0] = HighScoreObj;
                txtHighScore.text = HighScoreObj.ToString();
                ScoreUIPlayGame.Instance.TxtHightScoreGame.text = HighScoreObj.ToString();
            }
            else
            {
                txtHighScore.text = DataGame.Instance.dataSave.HightScore[0].ToString();
            }
            DataGame.Instance.dataSave.CurentScore[0] = ScoreObj;
            //this.SortHighScore();
            PlayerPrefs.SetInt("ScoreWin", ScoreObj);
            fillWinAndLose.SetActive(true);
            DataGame.Instance.dataSave.Diamon[0] = ScoreUIPlayGame.Instance.IntermediateDiamon;
            DataGame.Instance.SaveData();
        }
    }
}
