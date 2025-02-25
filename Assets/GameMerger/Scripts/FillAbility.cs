using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillAbility : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        Debug.Log("Click");
        gameObject.SetActive(false);
        GameStateController.Instance.CurrentGameState = GameState.Dragging;
        AbilityClickDestroyBox.Instance.IsCheckUseAbility = false;
        AbilityClickDestroyBox.Instance.DisableAllDot();
    }
}
