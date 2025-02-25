using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStateController : MonoBehaviour
{
    #region Variable
    private GameObject ads;
    private GameObject setting;
    public static GameStateController Instance;
    private GameObject objAbility;
    private GridController gridController;
    [SerializeField] private GameState currentGameState;
    private DemoGame demoGame;
    #endregion
    #region Public
    public GameState CurrentGameState { get => currentGameState; set => currentGameState = value; }
    public GameObject Ads { get => ads; set => ads = value; }
    public GameObject Setting { get => setting; set => setting = value; }
    public GameObject ObjAbility { get => objAbility; set => objAbility = value; }
    #endregion


    private void Awake()
    {
        if (gridController == null) gridController = FindFirstObjectByType<GridController>();
        if (objAbility == null) objAbility = GameObject.Find("Ability"); else Debug.LogError("objAbility not null");
        if (ads == null) ads = GameObject.Find("Ads"); else Debug.LogError("bntAds not null");
        if (setting == null) setting = GameObject.Find("Setting"); else Debug.LogError("bntSetting not null");
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        demoGame = FindFirstObjectByType<DemoGame>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckGameState());
    }

    private IEnumerator CheckGameState()
    {
        yield return null;
        if (!DataGame.Instance.dataSave.IsCheckDemo[0])
        {
            this.currentGameState = GameState.Demo;
            ads.SetActive(false);
            setting.SetActive(false);
            objAbility.SetActive(false);
        }
        else
        {
            DemoGame.Instance.TableText.SetActive(false);
            this.gridController.CreateBox(0.7f);
            this.gridController.CreateBox(2.3f);
            this.demoGame.fillDemoGame.SetActive(false);
            this.currentGameState = GameState.Dragging;
        }
    }

    private void Update()
    {
        if (currentGameState == GameState.Finish)
        {
            currentGameState = GameState.Dragging;
        }
    }
}

public enum GameState
{
    None,
    Dragging,
    ExcuteAbility,
    ExcuteAbilityFinish,
    Checking,
    Destroy,
    Finish,
    WinGame,
    BackHome,
    Again,
    LoseGame,
    Exit,
    Demo,
}
