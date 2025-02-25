using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPlusTurn : AbilityController
{
    private Canvas canvas;
    public static AbilityPlusTurn Instance;
    private Button bntAbilityPlusTurn;

    private void Awake()
    {
        if (canvas == null) canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
        if (ObjFillAbilityDestroyBox == null) ObjFillAbilityDestroyBox = GameObject.Find("fillAbility"); else Debug.LogError("objFillAbilityDestroyBox not null");
        if (bntAbilityPlusTurn == null) bntAbilityPlusTurn = GameObject.Find("bntPlusTurn").GetComponent<Button>(); else Debug.LogError("btnAbilityOlusturn not null");
        bntAbilityPlusTurn.onClick.AddListener(ExcuteAbility);

    }
    // Start is called before the first frame update
    void Start()
    {
        ObjFillAbilityDestroyBox.SetActive(false);
        this.DiamonToSpend = 100;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ExcuteAbility()
    {
        base.ExcuteAbility();
        if (CheckDiamonCanUseAbility())
        {
            this.ObjFillAbilityDestroyBox.SetActive(true);
            this.ObjFillAbilityDestroyBox.GetComponent<BoxCollider2D>().enabled = false;
            this.SwitchPos(10, 11);
            this.MinusDiamon();
            PlusTurn();
        }
        else
            GameStateController.Instance.CurrentGameState = GameState.Dragging;
    }

    private void PlusTurn()
    {
        StartCoroutine(TurnGame.Instance.AnimateTurn(3));
    }

    public void SwitchPos(int posA, int toLocation)
    {
        var objFill = this.canvas.transform.GetChild(posA);
        objFill.SetSiblingIndex(toLocation);
    }
}
