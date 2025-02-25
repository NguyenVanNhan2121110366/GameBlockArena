using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
public class ScoreUIPlayGame : ScoreController
{
    [Header("Score Play Game")]
    public static ScoreUIPlayGame Instance;
    private GridController grid;
    [SerializeField] private TextMeshProUGUI txtCountDiamon;
    [SerializeField] private int plusDiamon;
    [SerializeField] private int countDiamon;
    [SerializeField] private TextMeshProUGUI txtHightScoreGame;
    [SerializeField] private GameObject objDiamonPlus;
    [SerializeField] private TextMeshProUGUI textPlusDiamon;
    public int IntermediateDiamon;

    public int CountDiamon { get => countDiamon; set => countDiamon = value; }
    public TextMeshProUGUI TxtCountDiamon { get => txtCountDiamon; set => txtCountDiamon = value; }
    public TextMeshProUGUI TxtHightScoreGame { get => txtHightScoreGame; set => txtHightScoreGame = value; }
    private void Awake()
    {
        if (grid == null) grid = FindFirstObjectByType<GridController>();
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
        if (txtHightScoreGame == null) txtHightScoreGame = GameObject.Find("txtHightScoreGame").GetComponent<TextMeshProUGUI>();
        this.GetComponentScoreObj();

    }

    public void GetComponentScoreObj()
    {
        TxtScoreObj = GameObject.Find("txtScoreUI").GetComponent<TextMeshProUGUI>();
        TxtDiamonPlayGame = GameObject.Find("txtScoreDiamon").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        txtHightScoreGame.text = DataGame.Instance.dataSave.HightScore[0].ToString();
        IntermediateDiamon = DataGame.Instance.dataSave.Diamon[0];
        PlayerPrefs.SetInt("DiamonWinGame", IntermediateDiamon);
        ObserverManager.Instance.AddObserver(this);
        InvokeRepeating(nameof(UpdateHighScore), 0, 0.1f);
        TxtDiamonPlayGame.text = IntermediateDiamon.ToString();
        HighScores = DataGame.Instance.dataSave.HightScore.ToList();
    }

    private void DelayAssignScore()
    {
        if (DataGame.Instance.dataSave.Diamon[0] == 0)
        {
            DataGame.Instance.dataSave.Diamon[0] = 10;
            Debug.Log("Vo day");
        }
        else
            Debug.Log("Vao day ne");
    }

    private void OnDisable()
    {
        ObserverManager.Instance.RemoveObserver(this);
    }

    public override void UpdateScoreUI()
    {
        ScoreObj = ObserverManager.Instance.ShareScoreObj;
    }

    public void RestartScore()
    {
        ObserverManager.Instance.ShareScoreObj = PlayerPrefs.GetInt("ScoreWin", 0);
        CountScore = DataGame.Instance.dataSave.CurentScore[0];
        countDiamon = 0;
        TxtDiamonPlayGame.text = IntermediateDiamon.ToString();
        DataGame.Instance.dataSave.Diamon[0] = IntermediateDiamon;
        TxtScoreObj.text = CountScore.ToString();
        DataGame.Instance.SaveData();
    }
    private void OnEnable()
    {
        CountScore = DataGame.Instance.dataSave.CurentScore[0];
        ObserverManager.Instance.ShareScoreObj = DataGame.Instance.dataSave.CurentScore[0];
        TxtScoreObj.text = ObserverManager.Instance.ShareScoreObj.ToString();
    }

    public void ResetScore()
    {
        ObserverManager.Instance.ShareScoreObj = DataGame.Instance.dataSave.CurentScore[0];
        ScoreObj = 0;
        CountScore = 0;
        IntermediateDiamon = PlayerPrefs.GetInt("DiamonWinGame", 0);
        TxtDiamonPlayGame.text = DataGame.Instance.dataSave.Diamon[0].ToString();
        TxtScoreObj.text = CountScore.ToString();
    }


    public override void UpdateScoreDiamon()
    {
        ScoreDiamon = ObserverManager.Instance.ShareScoreDiamon;
        IntermediateDiamon += ScoreDiamon;
        TxtDiamonPlayGame.text = IntermediateDiamon.ToString();
        countDiamon += ScoreDiamon;
        ObserverManager.Instance.ShareScoreDiamon = 0;
        txtCountDiamon.text = "+ " + countDiamon.ToString();
    }


    public override void AnimateScore(int amout)
    {
        for (var i = 0; i < amout; i += 5)
        {
            var score = Instantiate(ObjScore);
            score.transform.position = TxtScoreObj.transform.position;
            var duration = Random.Range(MinAnimDuration, MaxAnimDuration);
            score.transform.DOMove(ObjScore.transform.position, duration).SetEase(EaseCustom).OnComplete(() =>
            {
                CountScore += 5;
                TxtScoreObj.text = CountScore.ToString();
                Destroy(score);
            });
        }
    }

    public void MovePlusDiamon(int diamonPlus, float plusX, float plusY)
    {
        var pos = new Vector2(grid.LastDestroy.x + plusX, grid.LastDestroy.y + plusY);
        var objDiamon = Instantiate(objDiamonPlus, pos, Quaternion.identity);
        objDiamon.SetActive(true);
        objDiamon.transform.GetChild(0).GetComponent<TextMeshPro>().text = "+ " + diamonPlus.ToString();
        //textPlusDiamon = GameObject.Find("txtCountDiamonPlus").GetComponent<TextMeshProUGUI>();
        //textPlusDiamon.text = "+ " + diamonPlus.ToString();
        objDiamon.transform.DOMove(new Vector2(objDiamon.transform.position.x, objDiamon.transform.position.y + 0.6f), 1.5f).OnComplete(() =>
        {
            Destroy(objDiamon);
        });
    }
}
