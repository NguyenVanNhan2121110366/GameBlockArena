using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIWinGameManager : ClickButtonManager
{
    private RectTransform rectWinGame;
    private static UIWinGameManager instance;
    private TextMeshProUGUI txtScoreWinGame;
    private Button bntRestartGame;
    public static UIWinGameManager Instance { get => instance; set => instance = value; }
    void Awake()
    {
        if (gridController == null) gridController = FindFirstObjectByType<GridController>(); else Debug.Log("grid was exits");
        if (instance == null) instance = this; else Destroy(gameObject);
        if (rectWinGame == null) rectWinGame = GameObject.Find("WinGame").GetComponent<RectTransform>();
        else Debug.Log("rectWinGame was exits");
        if (BntHome == null) BntHome = GameObject.Find("bntHomeWinGame").GetComponent<Button>();
        else Debug.Log("BntHome was exits");
        if (bntRestartGame == null) bntRestartGame = GameObject.Find("bntRestartWinGame").GetComponent<Button>();
        else Debug.Log("bntRestartGame was exits");
        if (txtScoreWinGame == null) txtScoreWinGame = GameObject.Find("txtScoreWinGame").GetComponent<TextMeshProUGUI>();
        else Debug.Log("txtScoreWinGame was exits");
        if (SceneTransition == null) SceneTransition = FindFirstObjectByType<SceneTransition>();
        else Debug.Log("SceneTransition was exits");
        BntHome.onClick.AddListener(OnClickHome);
        bntRestartGame.onClick.AddListener(OnClickRestart);
    }

    void Start()
    {
        rectWinGame.gameObject.SetActive(false);

    }

    private void OnClickRestart()
    {
        PlayerPrefs.SetString("WinGame", "NotWinYet");
        PlayerPrefs.Save();
        rectWinGame.DOAnchorPosY(-1000, 1f).OnComplete(() =>
        {
            ScoreWinGame.Instance.FillWinAndLose.SetActive(false);
            txtScoreWinGame.text = ScoreUIPlayGame.Instance.CountScore.ToString();
            GameStateController.Instance.CurrentGameState = GameState.Dragging;
            this.gridController.TurnOnAllGrid();
            SceneTransition.ReturnToLevel1();
            rectWinGame.gameObject.SetActive(false);
            Debug.Log("Restart");
        });
    }

    public override void OnClickHome()
    {
        DataGame.Instance.dataSave.CurentScore[0] = 0;
        DataGame.Instance.dataSave.Level[0] = 0;
        DataGame.Instance.SaveData();
        PlayerPrefs.SetString("WinGame", "Win");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Loading");
    }

    public void TurnOnUIWinGame()
    {
        PlayerPrefs.SetString("WinGame", "Win");
        PlayerPrefs.Save();
        txtScoreWinGame.text = ScoreUIPlayGame.Instance.CountScore.ToString();
        ScoreWinGame.Instance.FillWinAndLose.SetActive(true);
        rectWinGame.gameObject.SetActive(true);
        rectWinGame.DOAnchorPosY(0, 1f).OnComplete(() =>
        {

        });
    }
}
