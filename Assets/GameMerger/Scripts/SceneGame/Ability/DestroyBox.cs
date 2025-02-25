using System.Collections;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    private GridController gridController;
    private CheckQuantityObj checkQuantityObj;
    private void Awake()
    {
        this.gridController = FindFirstObjectByType<GridController>();
    }
    private void OnMouseDown()
    {
        if (AbilityClickDestroyBox.Instance.IsCheckClickDestroyBox)
        {
            ClickDestroy();
            AbilityClickDestroyBox.Instance.IsCheckUseAbility = false;
        }
        else if (AbilityBom.Instance.IsCheckClickBom)
        {
            this.DestroyBig();
            AbilityBom.Instance.IsCheckUseAbility = false;
        }


    }

    private void GetComponentQuantity()
    {
        checkQuantityObj = FindAnyObjectByType<CheckQuantityObj>();
        checkQuantityObj.UpdateQuantity(gameObject.tag);
        //checkQuantityObj.NextGame();
    }
    private void EffectDestroy()
    {
        //EffectManager.Instance.SpawnEffects(gameObject);
        //StartCoroutine(this.gridController.CheckNameTagAnimator(gameObject.GetComponent<DotInteraction>().Column, gameObject.GetComponent<DotInteraction>().Row, "Destroy"));
    }

    private void ClickDestroy()
    {
        AbilityClickDestroyBox.Instance.IsCheckClickDestroyBox = false;
        this.gridController.AllDots[gameObject.GetComponent<DotInteraction>().Column, gameObject.GetComponent<DotInteraction>().Row] = null;
        Destroy(gameObject);
    }

    private void DestroyBig()
    {
        var dotObj = GetComponent<DotInteraction>();
        var column = dotObj.Column;
        var row = dotObj.Row;
        AbilityBom.Instance.IsCheckClickBom = false;
        for (var i = -1; i <= 1; i++)
        {
            if (column + i >= 0 && column + i < this.gridController.With)
            {
                Destroy(this.gridController.AllDots[column + i, row]);
                this.gridController.AllDots[column + i, row] = null;
            }
        }
    }

    private void OnDestroy()
    {
        if (GameStateController.Instance.CurrentGameState != GameState.LoseGame
        && GameStateController.Instance.CurrentGameState != GameState.WinGame
        && GameStateController.Instance.CurrentGameState != GameState.Again
        && GameStateController.Instance.CurrentGameState != GameState.Exit)
        {
            gameObject.GetComponent<Object>().UpdateScoreUI();
            this.GetComponentQuantity();
            //this.EffectDestroy();
        }
    }
}
