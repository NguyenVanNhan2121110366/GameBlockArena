using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionGameState : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnMouseDown()
    {
        if (GameStateController.Instance.CurrentGameState == GameState.ExcuteAbilityFinish)
            GameStateController.Instance.CurrentGameState = GameState.Dragging;
    }

}
