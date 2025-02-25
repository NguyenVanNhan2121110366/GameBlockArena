using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class EndGameController : MonoBehaviour, IEndGame
{
    #region Variable
    private CheckQuantityObj checkQuantityObj;
    public static EndGameController Instance;
    private GridController gridController;
    [SerializeField] private GameObject objGameOver;
    private RectTransform rectGameOver;
    [SerializeField] private bool isFull;
    [SerializeField] private bool isTurn;

    #endregion
    #region Public
    public bool IsFull { get => isFull; set => isFull = value; }
    public bool IsTurn { get => isTurn; set => isTurn = value; }
    public GameObject ObjGameOver { get => objGameOver; set => objGameOver = value; }
    #endregion


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        if (rectGameOver == null) rectGameOver = GameObject.Find("GameOver").GetComponent<RectTransform>();
        this.CheckObjGameOver();
        this.CheckQuantity();

    }

    public void Result()
    {

    }

    #region Check
    private void CheckQuantity()
    {
        if (checkQuantityObj == null) checkQuantityObj = FindFirstObjectByType<CheckQuantityObj>();
        else Debug.Log("Not null");
    }

    public void CheckGridController()
    {
        if (gridController == null) this.gridController = FindFirstObjectByType<GridController>();
        else Debug.Log("Not null");
    }

    private void CheckObjGameOver()
    {
        if (objGameOver == null) this.objGameOver = GameObject.Find("GameOver");
        else Debug.Log("Not null");
    }

    #endregion

    void Start()
    {
        this.isFull = false;
        this.objGameOver.SetActive(false);
        isTurn = true;
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        FullArray();
        if (isFull || TurnGame.Instance.CurrentTurn <= 0)
        {
            Debug.Log("Vao day khong fen");
            this.CheckQuantity();
            if (checkQuantityObj && checkQuantityObj.Quantity < checkQuantityObj.NumberObjNeedToFind)
            {
                //SettingGame.Instance.HandleVibrate();
                PlayerPrefs.SetInt("ClickContinue", 2);
                PlayerPrefs.Save();
                GameStateController.Instance.CurrentGameState = GameState.LoseGame;
                ScoreWinGame.Instance.FillWinAndLose.SetActive(true);
                //ScoreLoseGame.Instance.UpdateScoreUILoseGame();
                Destroy(SpawnScene.Instance.CurrentLevel);
                this.objGameOver.SetActive(true);
                this.AnimateLoseGameUI();
                this.gridController.ObjFrameBorder.SetActive(false);
            }
        }
        // else if (isFull && this.gridController.IsLastMatched())
        // {
        //     StartCoroutine(this.gridController.DestroyMatched());
        //     //sthis.gridController.DestroyMatched();
        //     isTurn = false;
        // }
    }

    private void AnimateLoseGameUI()
    {
        this.rectGameOver.DOAnchorPosY(-10, 0.5f);
    }

    public void FullArray()
    {
        this.isFull = true;
        for (var i = 0; i < gridController.With; i++)
        {
            for (var j = 0; j < this.gridController.Height; j++)
            {
                if (this.gridController.AllDots[i, j] == null)
                {
                    this.isFull = false;
                    return;
                }
            }
        }
    }
}
