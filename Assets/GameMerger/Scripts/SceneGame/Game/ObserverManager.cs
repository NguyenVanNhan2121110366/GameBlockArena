using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObserverManager : MonoBehaviour
{
    public static ObserverManager Instance;
    public List<IUpdateScoreUI> updateScores = new List<IUpdateScoreUI>();
    public int ShareScoreObj;
    public int ShareScoreDiamon;

    private void Awake()
    {
        if (Instance == null) Instance = this; else Debug.Log("Not null");
    }


    public void AddObserver(IUpdateScoreUI observer)
    {
        updateScores.Add(observer);
    }

    public void RemoveObserver(IUpdateScoreUI observer)
    {
        updateScores.Remove(observer);
    }

    public void UpdateScoreUIGame()
    {
        foreach (var updateScore in updateScores)
        {
            updateScore.UpdateScoreUI();
        }
    }

    public void UpdateScoreDiamon()
    {
        foreach (var updateScore in updateScores)
        {
            updateScore.UpdateScoreDiamon();
        }
    }

}
