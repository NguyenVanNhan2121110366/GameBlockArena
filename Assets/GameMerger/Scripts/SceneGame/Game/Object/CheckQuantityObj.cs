using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class CheckQuantityObj : MonoBehaviour, IWinGame
{
    #region Variable

    [SerializeField] private string nameTagOfLevel;
    [SerializeField] private int numberObjNeedToFind;
    [SerializeField] private int quantity;
    [SerializeField] private int tagLevel;
    [SerializeField] private GameObject objNextGame;
    [SerializeField] private RectTransform winGameUI;
    private GridController gridController;
    private TextMeshProUGUI txtQuantity;
    #endregion
    #region Public
    public int TagLevel { get => tagLevel; set => tagLevel = value; }
    public GameObject ObjNextGame { get => objNextGame; set => objNextGame = value; }
    public int Quantity { get => quantity; set => quantity = value; }
    public int NumberObjNeedToFind { get => numberObjNeedToFind; set => numberObjNeedToFind = value; }
    #endregion



    private void Awake()
    {
        if (gridController == null) gridController = FindFirstObjectByType<GridController>();
        // Bug 
        objNextGame.SetActive(false);
        this.ChecktxtQuantity();
    }

    private void Start()
    {
        this.quantity = 0;
        this.UpdateTxtQuantity();
        LevelManager.Instance.ImageConversion(TagLevel);
    }
    private void Update()
    {
        if (quantity >= numberObjNeedToFind)
        {
            this.gridController.IsCheckWinGame();
        }
    }

    private void ChecktxtQuantity()
    {
        if (txtQuantity == null) txtQuantity = GameObject.FindWithTag("txtQuantity").GetComponent<TextMeshProUGUI>();
        else Debug.LogError("Not null");
    }

    public void UpdateTxtQuantity()
    {
        quantity = 0;
        txtQuantity.text = quantity.ToString() + " / " + numberObjNeedToFind.ToString();
    }

    public void UpdateQuantity(string nameTag)
    {

        if (nameTag == nameTagOfLevel)
        {
            LuckySpinManager.Instance.IsStop = false;
            quantity++;
            txtQuantity.text = quantity.ToString() + " / " + numberObjNeedToFind.ToString();
        }
    }

    public void WinGame()
    {

    }


    public void NextGame()
    {
        if (quantity >= numberObjNeedToFind)
        {
            if (!this.gridController.IsLastMatched() && !this.gridController.IsCheckSpawnLast)
            {
                //SettingGame.Instance.HandleVibrate();

                GameStateController.Instance.CurrentGameState = GameState.WinGame;
                GameController.Instance.Level++;
                DataGame.Instance.dataSave.Level[0] = GameController.Instance.Level;
                ScoreWinGame.Instance.UpdateHighScoreUI();
                this.gridController.DestroyAllObj();
                StartCoroutine(DelayWinGame());
                if (GameController.Instance.Level <= 10)
                {
                    this.objNextGame.SetActive(true);
                    this.AnimateWinGameUI();
                }
                else
                {
                    UIWinGameManager.Instance.TurnOnUIWinGame();
                }
                this.gridController.ObjFrameBorder.SetActive(false);

            }
        }
    }
    private IEnumerator DelayWinGame()
    {
        yield return null;
    }

    private void AnimateWinGameUI()
    {
        winGameUI.DOAnchorPosY(-10, 0.5f).OnComplete(() =>
        {
            var obj = GameObject.Find("LuckySpinManager");
            obj.GetComponent<LuckySpinManager>().enabled = true;
        });
    }

}
