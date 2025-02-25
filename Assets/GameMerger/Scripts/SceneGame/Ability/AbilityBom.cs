using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBom : AbilityController
{
    public static AbilityBom Instance;
    [SerializeField] private Button bntAbilityBom;
    public bool isCheckClickBom;


    public bool IsCheckClickBom { get => isCheckClickBom; set => isCheckClickBom = value; }
    private void Awake()
    {
        if (GridController == null) this.GridController = FindFirstObjectByType<GridController>(); else return;
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
        if (bntAbilityBom == null) bntAbilityBom = GameObject.Find("bntBom").GetComponent<Button>();
        bntAbilityBom.onClick.AddListener(this.ExcuteAbility);
    }

    private void Start()
    {
        this.DiamonToSpend = 600;
        this.ObjFillAbilityDestroyBox.SetActive(false);
        IsCheckUseAbility = true;
        InvokeRepeating(nameof(DisableAllDot), 0, 1f);
    }


    public override void ExcuteAbility()
    {
        base.ExcuteAbility();
        if (CheckNull())
        {
            if (this.CheckDiamonCanUseAbility())
            {
                this.EnableAllDot();
                isCheckClickBom = true;
            }
        }
        else
            GameStateController.Instance.CurrentGameState = GameState.Dragging;
    }
    private void EnableAllDot()
    {
        this.ObjFillAbilityDestroyBox.SetActive(true);
        //GameStateController.Instance.CurrentGameState = GameState.ExcuteAbility;
        if (IsCheckUseAbility)
        {
            for (var i = 0; i < this.GridController.With; i++)
            {
                for (var j = 0; j < this.GridController.Height; j++)
                {
                    if (this.GridController.AllDots[i, j] != null)
                    {
                        this.GridController.AllDots[i, j].GetComponent<BoxCollider2D>().enabled = true;
                        this.GridController.AllDots[i, j].GetComponent<MoveObject>().enabled = false;
                        this.GridController.AllDots[i, j].GetComponent<SpriteRenderer>().sortingOrder = 5;
                    }
                }
            }
        }
    }




    private void DisableAllDot()
    {
        if (!IsCheckUseAbility)
        {
            for (var i = 0; i < this.GridController.With; i++)
            {
                for (var j = 0; j < this.GridController.Height; j++)
                {
                    if (this.GridController.AllDots[i, j] != null)
                    {
                        this.GridController.AllDots[i, j].GetComponent<BoxCollider2D>().enabled = false;
                        IsCheckUseAbility = true;
                        this.GridController.AllDots[i, j].GetComponent<SpriteRenderer>().sortingOrder = 3;
                    }
                }
            }
            if (GameStateController.Instance.CurrentGameState == GameState.ExcuteAbility)
            {
                GameStateController.Instance.CurrentGameState = GameState.ExcuteAbilityFinish;
                this.MinusDiamon();
                Debug.Log("Vao day");
            }
            this.ObjFillAbilityDestroyBox.SetActive(false);
        }
    }
}
