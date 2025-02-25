using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class TurnGame : MonoBehaviour
{
    [SerializeField] private Image turnBar;
    [SerializeField] private GridController gridController;
    public static TurnGame Instance;
    #region Variable
    [SerializeField] private int maxTurn;
    [SerializeField] private int currentTurn;
    [SerializeField] private TextMeshProUGUI txtTurn;
    #endregion
    #region  Public
    public int CurrentTurn { get => currentTurn; set => currentTurn = value; }
    public int MaxTurn { get => maxTurn; set => maxTurn = value; }
    public TextMeshProUGUI TxtTurn { get => txtTurn; set => txtTurn = value; }
    public Image TurnBar { get => turnBar; set => turnBar = value; }
    #endregion

    private void Awake()
    {
        if (Instance == null) Instance = this; else Debug.Log("Not null");
        if (turnBar == null) this.turnBar = GameObject.Find("TurnBar").GetComponent<Image>(); else Debug.Log("Not null");
        if (txtTurn == null) this.txtTurn = GameObject.Find("txtTurnbar").GetComponent<TextMeshProUGUI>(); else Debug.Log("Not null");
        if (gridController == null) gridController = FindFirstObjectByType<GridController>(); else return;
    }
    private void Start()
    {
        TurnByLevel();
        this.UpdateTextTurn();
    }

    public void UpdateTextTurn()
    {
        this.txtTurn.text = this.currentTurn.ToString();
    }

    public IEnumerator AnimateTurn(int count)
    {
        var countTurn = this.currentTurn;
        for (var i = 0; i < count; i++)
        {
            countTurn++;
            this.txtTurn.text = countTurn.ToString();
            yield return new WaitForSeconds(0.3f);
        }
        currentTurn = countTurn;
        MaxTurn = countTurn;
        AbilityPlusTurn.Instance.ObjFillAbilityDestroyBox.GetComponent<BoxCollider2D>().enabled = true;
        AbilityPlusTurn.Instance.ObjFillAbilityDestroyBox.SetActive(false);
        AbilityPlusTurn.Instance.SwitchPos(11, 10);
        GameStateController.Instance.CurrentGameState = GameState.ExcuteAbilityFinish;
    }

    private void CreateTurn()
    {
        this.currentTurn = this.maxTurn;
    }

    public void UpdateTurn()
    {
        currentTurn--;
        CheckTurn();
    }

    public void CheckTurn()
    {
        if (currentTurn == 0 && !gridController.IsLastMatched())
        {
            EndGameController.Instance.IsTurn = false;
        }
        else if (currentTurn == 0 && gridController.IsLastMatched())
        {
            EndGameController.Instance.IsTurn = true;
        }
        this.UpdateTextTurn();
    }
    private void Update()
    {
        this.UpdateValueTurnBar();
    }

    private void UpdateValueTurnBar()
    {
        turnBar.fillAmount = Mathf.Lerp(turnBar.fillAmount, (float)currentTurn / (float)maxTurn, 9 * Time.deltaTime);
    }

    public void PlusTurn(string nametag)
    {
        switch (nametag)
        {
            case "tag1":
                currentTurn++;
                break;
            case "tag3":
                currentTurn += 2;
                break;
            case "tag9":
                currentTurn += 2;
                break;
            case "tag27":
                currentTurn += 3;
                break;
            case "tag81":
                currentTurn += 4;
                break;
            case "tag243":
                currentTurn += 7;
                break;
            case "tag2187":
                currentTurn += 8;
                break;
            case "tag6561":
                currentTurn += 9;
                break;
            default:
                Debug.Log("Khong co gi");
                break;
        }
        UpdateTextTurn();
    }

    public void TurnByLevel()
    {
        var currentLevel = GameController.Instance.Level;
        switch (currentLevel)
        {
            //Level 1
            case 0: maxTurn = 20; break;
            //Level 2
            case 1: maxTurn = 20; break;

            //Level 3
            case 2: maxTurn = 15; break;
            //Level 4
            case 3: maxTurn = 12; break;
            //Level 5
            case 4: maxTurn = 15; break;
            //Level 6
            case 5: maxTurn = 15; break;
            //Level 7
            case 6: maxTurn = 10; break;
            //Level 8
            case 7: maxTurn = 15; break;
            //Level 9
            case 8: maxTurn = 20; break;
            //Level 10
            case 9: maxTurn = 20; break;
            //Level 11
            case 10: maxTurn = 15; break;
            //Level 12
            case 11: maxTurn = 20; break;
            //Level 13
            case 12: maxTurn = 20; break;
            //Level 14
            case 13: maxTurn = 20; break;
            //Level 15
            case 14: maxTurn = 20; break;
            //Level 16
            case 15: maxTurn = 20; break;
            //Level 17
            case 16: maxTurn = 20; break;
            //Level 18
            case 17: maxTurn = 20; break;
            //Level 19
            case 18: maxTurn = 20; break;
            //Level 20
            case 19: maxTurn = 20; break;
            //Level 21
            case 20: maxTurn = 20; break;
            //Level 22
            case 21: maxTurn = 20; break;
            //Level 23
            case 22: maxTurn = 20; break;
            //Level 24
            case 23: maxTurn = 20; break;
            //Level 25
            case 24: maxTurn = 20; break;
            //Level 26
            case 25: maxTurn = 20; break;
            //Level 27
            case 26: maxTurn = 20; break;
            //Level 28
            case 27: maxTurn = 20; break;
            //Level 29
            case 28: maxTurn = 20; break;
            //Level 30
            case 29: maxTurn = 20; break;
            //Level 31
            case 30: maxTurn = 20; break;
            //Level 32
            case 31: maxTurn = 20; break;
            //Level 33
            case 32: maxTurn = 20; break;
            default:
                break;
        }
        this.CreateTurn();
    }
}