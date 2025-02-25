using UnityEngine;
using UnityEngine.UI;

public class AbilityClickDestroyBox : AbilityController
{
    #region Variable

    [Header("ABILITY DESTROY BOX ")]
    [SerializeField] private Button bntAbilityDestroyBox;
    [SerializeField] private bool isCheckClickDestroyBox;


    #endregion
    #region Public
    public static AbilityClickDestroyBox Instance;
    public bool IsCheckClickDestroyBox { get => isCheckClickDestroyBox; set => isCheckClickDestroyBox = value; }
    #endregion
    private void Awake()
    {
        //if (ScoreUI == null) ScoreUI = FindFirstObjectByType<ScoreUIPlayGame>();
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
        if (ObjFillAbilityDestroyBox == null) ObjFillAbilityDestroyBox = GameObject.Find("fillAbility"); else Debug.LogError("objFillAbilityDestroyBox not null");
        if (GridController == null) this.GridController = FindFirstObjectByType<GridController>(); else return;
        if (bntAbilityDestroyBox == null) bntAbilityDestroyBox = GameObject.Find("bntDestroyBox").GetComponent<Button>(); else return;
        this.bntAbilityDestroyBox.onClick.AddListener(this.ExcuteAbility);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.DiamonToSpend = 200;
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
                this.isCheckClickDestroyBox = true;
            }
        }
        else
            GameStateController.Instance.CurrentGameState = GameState.Dragging;
    }

    private void EnableAllDot()
    {
        Debug.Log("Vao day");
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




    public void DisableAllDot()
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
                Debug.Log("Vao day ne");
            }
            this.ObjFillAbilityDestroyBox.SetActive(false);
        }
    }
}
