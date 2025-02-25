using UnityEngine;
public class Object : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int diamonScore;
    private ScoreController scoreController;

    public int Score { get => score; set => score = value; }

    private void Awake()
    {
        this.scoreController = FindFirstObjectByType<ScoreController>();
    }
    private void Update()
    {

    }

    public void UpdateScoreUI()
    {
        this.UpdateScore();
        this.UpdateScoreDiamon();
        ObserverManager.Instance.UpdateScoreUIGame();
        ObserverManager.Instance.UpdateScoreDiamon();
    }
    //Plus score
    public void UpdateScore()
    {
        ObserverManager.Instance.ShareScoreObj += this.score;
        ScoreUIPlayGame.Instance.AnimateScore(score);
    }

    public void UpdateScoreDiamon()
    {
        ObserverManager.Instance.ShareScoreDiamon += this.diamonScore;
        //Debug.Log(ObserverManager.Instance.ShareScoreDiamon);
        Debug.Log(diamonScore);
    }
}
