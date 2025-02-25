using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine.SocialPlatforms.Impl;
public abstract class ScoreController : MonoBehaviour, IUpdateScoreUI
{
    #region Variable
    public int ScoreObj;
    public int ScoreDiamon;
    public int HighScoreObj;
    public bool IsAddToGrid;
    public List<int> HighScores = new();

    public TextMeshProUGUI TxtScoreObj;
    public TextMeshProUGUI TxtDiamonPlayGame;
    public GameObject ObjScore;
    public int CountScore;
    public float MaxAnimDuration;
    public float MinAnimDuration;
    public Ease EaseCustom;

    #endregion

    public void UpdateHighScore()
    {
        if (GameStateController.Instance.CurrentGameState == GameState.LoseGame)
        {
            this.SortHighScore();
        }
    }

    public virtual void SortHighScore()
    {
        HighScores = DataGame.Instance.dataSave.HightScore.ToList();
        if (!HighScores.Contains(HighScoreObj))
        {
            HighScores.Add(HighScoreObj);
        }

        if (!HighScores.Contains(DataGame.Instance.dataSave.CurentScore[0]))
        {
            HighScores.Add(DataGame.Instance.dataSave.CurentScore[0]);
        }

        HighScores.Sort((a, b) => b.CompareTo(a));
        HighScores = HighScores.Take(3).ToList();
        for (var i = 0; i < HighScores.Count; i++)
            DataGame.Instance.dataSave.HightScore[i] = HighScores[i];
    }

    public virtual void UpdateScoreUI() { }
    public virtual void UpdateHighScoreUI() { }
    public virtual void UpdateScoreDiamon() { }
    public virtual void AnimateScore(int amout) { }

}
