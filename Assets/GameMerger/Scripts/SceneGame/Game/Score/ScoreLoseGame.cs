using TMPro;
using UnityEngine;
public class ScoreLoseGame : ScoreController
{
    [Header("Score Lose Game")]
    [SerializeField] private TextMeshProUGUI txtHighScore;
    public static ScoreLoseGame Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
    }
    private void Start()
    {
        //TxtScoreObj.text = ScoreObj.ToString();
        //txtHighScore.text = DataGame.Instance.dataSave.HightScore[0].ToString();
        ObserverManager.Instance.AddObserver(this);
    }


    private void OnDisable()
    {
        ObserverManager.Instance.RemoveObserver(this);
        //UpdateHighScoreUI();
        //this.SortHighScore();
    }

    public override void UpdateScoreUI()
    {
        ScoreObj = ObserverManager.Instance.ShareScoreObj;
        TxtScoreObj.text = DataGame.Instance.dataSave.CurentScore[0].ToString();
    }

    public override void UpdateHighScoreUI()
    {
        if (GameStateController.Instance.CurrentGameState == GameState.LoseGame)
        {
            if (DataGame.Instance.dataSave.HightScore[0] < ScoreObj)
            {
                HighScoreObj = ScoreObj;
                txtHighScore.text = HighScoreObj.ToString();
            }
            else
            {
                DataGame.Instance.dataSave.CurentScore[0] = ScoreObj;
                txtHighScore.text = DataGame.Instance.dataSave.HightScore[0].ToString();
            }
        }
    }

}
