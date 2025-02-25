using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GridController : MonoBehaviour
{

    #region Variable
    private SceneTransition sceneTransition;
    [SerializeField] private GameObject objBan;
    [SerializeField] private int with, height;
    [SerializeField] private GameObject[] dots = new GameObject[9];
    [SerializeField] private Transform dot;
    [SerializeField] private GameObject grid;
    [SerializeField] private Vector2 lastDestroyPosition;
    [SerializeField] private List<GameObject> objects = new();
    [SerializeField] private GameObject frameBorder;
    private GameObject objFrameBorder;
    [SerializeField] private bool checkDestroyList;
    [SerializeField] private List<GameObject> gridObj = new();
    [SerializeField] private List<GameObject> dotObj = new();
    [SerializeField] private GameObject lastObj;
    private bool isCheckGameOver;
    [SerializeField] private CheckQuantityObj checkQuantityObj;
    public bool IsCheckSpawnLast;
    [SerializeField] private string nameTag;
    private GameObject[,] allGrids;
    private GameObject[,] allDots;
    #endregion
    #region Public
    public Vector2 LastDestroy { get => lastDestroyPosition; set => lastDestroyPosition = value; }
    public int With { get => with; set => with = value; }
    public int Height { get => height; set => height = value; }
    public GameObject[,] AllGrid { get => allGrids; set => allGrids = value; }
    public GameObject[,] AllDots { get => allDots; set => allDots = value; }
    public List<GameObject> Objects { get => objects; set => objects = value; }
    public bool CheckDestroyList { get => checkDestroyList; set => checkDestroyList = value; }
    public List<GameObject> GridObj { get => gridObj; set => gridObj = value; }
    public List<GameObject> DotObj { get => dotObj; set => dotObj = value; }
    public GameObject ObjFrameBorder { get => objFrameBorder; set => objFrameBorder = value; }
    public GameObject LastObj { get => lastObj; set => lastObj = value; }
    #endregion

    private void Awake()
    {
        sceneTransition = FindFirstObjectByType<SceneTransition>();
        if (dot == null) dot = GameObject.Find("Dots").GetComponent<Transform>();
    }

    private void OnDisable()
    {
        // Application.Quit();
        // sceneTransition.RestartLevelLoseGameDisable();
        // sceneTransition.NextLevelDisable();
        GameStateController.Instance.CurrentGameState = GameState.Exit;
        this.DestroyAllObj();
    }

    void Start()
    {
        this.IsCheckSpawnLast = false;
        this.SpawnFrameBorder();
        this.isCheckGameOver = false;
        this.allGrids = new GameObject[with, height];
        this.allDots = new GameObject[with, height];
        this.GetAllDotIntoTheArray();
        StartCoroutine(this.CreateBoard());
        //StartCoroutine(this.DestroyMatched());
        EndGameController.Instance.CheckGridController();
        this.checkDestroyList = true;
        // if (PlayerPrefs.GetString("WinGame", "null") == "Win")
        // {
        //     sceneTransition.ReturnToLevel1();
        //     PlayerPrefs.SetString("WinGame", "Not Win Yet");
        //     PlayerPrefs.Save();
        // }
        //this.SpawnBan();
    }

    private void SpawnFrameBorder()
    {
        var pos = new Vector2(1.5f, 1.5f);
        objFrameBorder = Instantiate(this.frameBorder, pos, Quaternion.identity);
    }

    private void GetAllDotIntoTheArray()
    {
        for (var i = 0; i < dot.childCount; i++)
        {
            dots[i] = dot.GetChild(i).gameObject;
        }
    }

    private IEnumerator CreateBoard()
    {
        for (var i = 0; i < this.with; i++)
        {
            for (var j = 0; j < this.height; j++)
            {
                var pos = new Vector2(i, j);
                var grid = Instantiate(this.grid, pos, Quaternion.identity);
                grid.SetActive(true);
                grid.transform.parent = transform;
                this.allGrids[i, j] = grid;
            }
        }
        GameStateController.Instance.CurrentGameState = GameState.Dragging;
        yield return null;
    }


    public void DestroyList()
    {
        foreach (var obj in objects)
        {
            Destroy(obj);
        }
        objects.Clear();
    }

    public void CreateBox(float column)
    {
        var pos = new Vector2(column, -1.8f);
        var grid = Instantiate(this.grid, pos, Quaternion.identity);
        grid.SetActive(true);
        grid.transform.parent = transform;
        this.gridObj.Add(grid);
        CreateDotObj(column);
    }
    public void SpawnDot()
    {
        CreateDotObj(.7f);
        CreateDotObj(2.3f);
        //this.SpawnBan();
    }
    private void SpawnBan()
    {
        var listPos = new List<Vector2Int>();
        Vector2Int pos;
        var countLevel = 0;
        if (GameController.Instance.Level > 0 && GameController.Instance.Level < 3)
            countLevel = 1;
        else if (GameController.Instance.Level > 3 && GameController.Instance.Level < 6)
            countLevel = 2;
        else
            countLevel = 3;

        var rowRandom = Random.Range(0, 4);
        // dang fix
        for (var i = 0; i < countLevel; i++)
        {
            do
            {
                var column = Random.Range(0, 4);
                var row = Random.Range(0, 4);
                pos = new Vector2Int(column, row);
            } while
            (listPos.Contains(pos));
            listPos.Add(pos);
            var obj = Instantiate(objBan, new Vector2(pos.x, pos.y), Quaternion.identity);
            obj.GetComponent<ObjBan>().Column = pos.x;
            obj.GetComponent<ObjBan>().Row = pos.y;
            this.allDots[pos.x, pos.y] = obj;
        }
    }

    public void CreateDotObj(float column)
    {
        var pos = new Vector2(column, -1.8f);
        var randomIndex = LevelManager.Instance.RanDomInDexNumber();
        var obj = Instantiate(this.dots[randomIndex], pos, Quaternion.identity);
        obj.SetActive(true);
        obj.transform.parent = transform;
        this.objects.Add(obj);
        obj.GetComponent<SpriteRenderer>().sortingOrder = 3;
        //obj.GetComponent<DestroyBox>().enabled = false;
    }

    public void AddObjectIntoGrid(GameObject obj, Vector2 pos)
    {
        var posObj = obj.transform.position;
        var x = Mathf.RoundToInt(posObj.x);
        var y = Mathf.RoundToInt(posObj.y);
        if (x >= 0 && x < this.with && y >= 0 && y < this.height)
        {
            if (allDots[x, y] == null)
            {
                obj.AddComponent<DotInteraction>();
                obj.transform.position = new Vector3(x, y, 0);
                obj.GetComponent<DotInteraction>().Column = x;
                obj.GetComponent<DotInteraction>().Row = y;
                this.allDots[x, y] = obj;
                obj.GetComponent<BoxCollider2D>().enabled = false;
                obj.SetActive(true);
                this.SpawnBoxNumber(pos);
                this.lastDestroyPosition = new Vector2(x, y);
                this.lastObj = obj;
                obj.AddComponent<DestroyBox>();
                Destroy(obj.GetComponent<TransitionGameState>());
                TurnGame.Instance.UpdateTurn();
                this.objects.Add(obj);
                if (!obj.GetComponent<DotInteraction>().IsMatched)
                    GameStateController.Instance.CurrentGameState = GameState.Dragging;
                AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[0]);
                StartCoroutine(obj.GetComponent<DotInteraction>().CheckDestroyMatched());
            }
            else
            {
                obj.transform.position = pos;
            }
        }
        else
        {
            obj.transform.position = pos;
        }

    }

    private void SpawnBoxNumber(Vector2 column)
    {
        if (GameStateController.Instance.CurrentGameState != GameState.WinGame)
        {
            var pos = column;
            var randomIndex = LevelManager.Instance.RanDomInDexNumber();
            var objBox = Instantiate(this.dots[randomIndex], pos, Quaternion.identity);
            objBox.transform.parent = transform;
            objBox.GetComponent<SpriteRenderer>().sortingOrder = 3;
            //objBox.GetComponent<DestroyBox>().enabled = false;
            objBox.SetActive(true);
        }
    }
    #region Destroy Matched

    private void DestroyMatchedAt(int column, int row)
    {
        if (this.allDots[column, row].TryGetComponent<DotInteraction>(out var dotInteraction))
            if (dotInteraction.IsMatched)
            {
                AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[1]);
                this.nameTag = this.allDots[column, row].tag;
                this.CheckNameTagAnimator(column, row, "Destroy");
                //EffectManager.Instance.SpawnEffects(this.allDots[column, row]);
                Destroy(this.allDots[column, row], 0.5f);
                this.allDots[column, row] = null;
                GameStateController.Instance.CurrentGameState = GameState.Destroy;
            }
    }

    private void CheckNameTagAnimator(int column, int row, string nameAnimator)
    {
        switch (this.allDots[column, row].tag)
        {
            case "tag1":
                this.allDots[column, row].GetComponent<Animator>().SetTrigger(nameAnimator + "01");
                break;
            case "tag3":
                this.allDots[column, row].GetComponent<Animator>().SetTrigger(nameAnimator + "03");
                break;
            case "tag9":
                this.allDots[column, row].GetComponent<Animator>().SetTrigger(nameAnimator + "09");
                break;
            case "tag27":
                this.allDots[column, row].GetComponent<Animator>().SetTrigger(nameAnimator + "27");
                break;
            case "tag81":
                this.allDots[column, row].GetComponent<Animator>().SetTrigger(nameAnimator + "81");
                break;
            case "tag243":
                this.allDots[column, row].GetComponent<Animator>().SetTrigger(nameAnimator + "243");
                break;
            default:
                break;
        }
    }


    public IEnumerator DestroyMatched()
    {
        yield return new WaitForSeconds(0.1f);
        for (var i = 0; i < this.with; i++)
        {
            for (var j = 0; j < this.height; j++)
            {
                if (this.allDots[i, j] != null && !checkDestroyList)
                {
                    this.DestroyMatchedAt(i, j);
                    //???
                    if (GameStateController.Instance.CurrentGameState != GameState.ExcuteAbility
                    && GameStateController.Instance.CurrentGameState != GameState.ExcuteAbilityFinish)
                    {
                        this.IsCheckSpawnLast = true;
                    }

                }
            }
        }
        //Fix Bug
        StartCoroutine(this.SpawnObjectInLastPoint());
    }
    #endregion
    // Spawn object again after destroy 
    public IEnumerator SpawnObjectInLastPoint()
    {
        yield return new WaitForSeconds(0.5f);
        if (GameStateController.Instance.CurrentGameState != GameState.WinGame && GameStateController.Instance.CurrentGameState != GameState.LoseGame)
        {

            var column = Mathf.RoundToInt(this.lastDestroyPosition.x);
            var row = Mathf.RoundToInt(this.lastDestroyPosition.y);
            if (this.allDots[column, row] == null)
            {
                var number = RandomDot();
                var pos = this.lastDestroyPosition;
                var obj = Instantiate(this.dots[number], pos, Quaternion.identity);
                obj.transform.parent = transform;
                obj.AddComponent<DotInteraction>();
                obj.GetComponent<DotInteraction>().Column = Mathf.RoundToInt(this.lastDestroyPosition.x);
                obj.GetComponent<DotInteraction>().Row = Mathf.RoundToInt(this.lastDestroyPosition.y);
                obj.GetComponent<BoxCollider2D>().enabled = false;
                this.allDots[column, row] = obj;
                obj.SetActive(true);
                obj.GetComponent<SpriteRenderer>().sortingOrder = 3;
                obj.AddComponent<DestroyBox>();
                lastObj = obj;
                this.CheckNameTagAnimator(column, row, "SpawnTag");
                GameStateController.Instance.CurrentGameState = GameState.Dragging;
                if (checkQuantityObj == null)
                {
                    checkQuantityObj = FindAnyObjectByType<CheckQuantityObj>();
                }
                TurnGame.Instance.PlusTurn(nameTag);
                this.objects.Add(obj);
                this.checkDestroyList = true;
            }
        }
        //this.CheckTurn();
        // StartCoroutine(EndGameController.Instance.GameOver());
        objects.RemoveAll(obj => obj == null);
        //Chua fix xong
        StartCoroutine(CheckDestroyAgain());
    }

    private bool CheckMatched()
    {
        for (var i = 0; i < with; i++)
        {
            for (var j = 0; j < height; j++)
            {
                if (this.allDots[i, j])
                {
                    if (this.allDots[i, j].GetComponent<DotInteraction>().IsMatched)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator CheckDestroyAgain()
    {
        yield return new WaitForSeconds(0.5f);
        if (CheckMatched())
        {
            StartCoroutine(DestroyMatched());
        }
    }

    public bool IsLastMatched()
    {
        for (var i = 0; i < this.with; i++)
        {
            for (var j = 0; j < this.height; j++)
            {
                if (this.allDots[i, j] != null && this.allDots[i, j].GetComponent<DotInteraction>().IsMatched && IsCheckSpawnLast)
                {
                    return true;
                }
            }
        }
        return false;
    }


    public void IsCheckWinGame()
    {
        if (!IsLastMatched() && IsCheckSpawnLast)
        {
            StartCoroutine(this.DelayIsCheckSpawnLast());
        }
    }

    private IEnumerator DelayIsCheckSpawnLast()
    {
        IsCheckSpawnLast = false;
        IsCheckSpawnLast = false;
        yield return new WaitForSeconds(1f);
        Debug.Log("Vao day khong");
        FindFirstObjectByType<CheckQuantityObj>().NextGame();
    }

    private int RandomDot()
    {
        var number = 0;
        if (this.nameTag == "tag1")
        {
            number = 1;
        }
        else if (this.nameTag == "tag3")
        {
            number = 2;
        }
        else if (this.nameTag == "tag9")
        {
            number = 3;
        }
        else if (this.nameTag == "tag27")
        {
            number = 4;
        }
        else if (this.nameTag == "tag81")
        {
            number = 5;
        }
        else if (this.nameTag == "tag243")
        {
            number = 6;
        }
        return number;
    }


    public void DestroyAllObj()
    {
        this.TurnOnOrOff(false, gridObj);
        for (var i = 0; i < this.with; i++)
        {
            for (var j = 0; j < this.height; j++)
            {
                allGrids[i, j].SetActive(false);
                if (allDots[i, j] != null)
                    allDots[i, j].SetActive(false);
            }
        }
    }

    public void TurnOnAllGrid()
    {
        for (var i = 0; i < this.with; i++)
        {
            for (var j = 0; j < this.height; j++)
            {
                allGrids[i, j].SetActive(true);
                if (allDots[i, j] != null)
                    allDots[i, j].SetActive(true);
            }
        }
        this.TurnOnOrOff(true, gridObj);
    }

    private void TurnOnOrOff(bool called, List<GameObject> objs)
    {
        foreach (var obj in objs)
        {
            if (objs != null)
                obj.SetActive(called);
        }
    }


}