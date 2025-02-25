using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    #region Variable
    [SerializeField] private Vector2 pos;
    private GridController gridController;
    private DemoGame demoGame;
    #endregion

    private bool isDragging;

    private void Awake()
    {
        this.demoGame = FindFirstObjectByType<DemoGame>();
        this.gridController = FindFirstObjectByType<GridController>();
    }
    void Start()
    {
        InvokeRepeating(nameof(CheckGameStateDestroyObjInGrid), 0, 0.1f);
        this.isDragging = false;
    }

    void Update()
    {
        if (isDragging && GameStateController.Instance.CurrentGameState != GameState.WinGame
        && GameStateController.Instance.CurrentGameState == GameState.Dragging && TurnGame.Instance.CurrentTurn > 0)
        {
            var inputMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            inputMouse.z = 0;
            transform.position = inputMouse;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }

    //Destroy obj In grid box, drag

    private void CheckGameStateDestroyObjInGrid()
    {
        if (GameStateController.Instance.CurrentGameState == GameState.WinGame || GameStateController.Instance.CurrentGameState == GameState.LoseGame)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[4]);
        DemoGame.Instance.IsCheckDemo = true;
        if (DemoGame.Instance.IsCheckDemo == true && GameStateController.Instance.CurrentGameState == GameState.Demo)
        {
            DemoGame.Instance.TableText.SetActive(true);
            DemoGame.Instance.Demo01();
        }
        isDragging = true;
        pos = transform.position;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
        FindAnyObjectByType<GridController>().AddObjectIntoGrid(this.gameObject, pos);
    }
}
