using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public int DiamonToSpend;
    public GameObject ObjFillAbilityDestroyBox;
    public GridController GridController;
    public bool IsCheckUseAbility;

    public virtual void ExcuteAbility()
    {
        GameStateController.Instance.CurrentGameState = GameState.ExcuteAbility;
    }

    public bool CheckDiamonCanUseAbility()
    {
        //if (CheckNull())
        {
            if (ScoreUIPlayGame.Instance.IntermediateDiamon >= DiamonToSpend)
            {
                Debug.Log("Con du diamon");
                return true;
            }
        }
        GameStateController.Instance.CurrentGameState = GameState.Dragging;
        Debug.Log("Khong du diamon");
        return false;
    }



    public void MinusDiamon()
    {
        ScoreUIPlayGame.Instance.IntermediateDiamon -= DiamonToSpend;
        ScoreUIPlayGame.Instance.TxtDiamonPlayGame.text = ScoreUIPlayGame.Instance.IntermediateDiamon.ToString();
    }

    public bool CheckNull()
    {
        for (var i = 0; i < GridController.With; i++)
        {
            for (var j = 0; j < this.GridController.Height; j++)
            {
                if (this.GridController.AllDots[i, j] != null)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
