using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DemoGame : MonoBehaviour
{
    public static DemoGame Instance;
    [SerializeField] private Button bntSkip;
    [SerializeField] private GameObject objHand;
    private GridController gridController;
    private int countDemo;
    public GameObject fillDemoGame;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject demoGame01;
    [SerializeField] private GameObject demoGame02;
    [SerializeField] private GameObject demoGame03;
    [SerializeField] private GameObject demoGame04;
    private Vector2 pos;
    private Vector2 originPos;
    private bool isCheck;
    public GameObject TableText;
    public GameObject Hand;
    public bool IsCheckDemo;





    private void Awake()
    {
        if (TableText == null) TableText = GameObject.Find("TableText");
        if (gridController == null) gridController = FindFirstObjectByType<GridController>();
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
        if (objHand == null) objHand = GameObject.Find("Hand"); else Debug.LogError("objHand not null");
        if (bntSkip == null) bntSkip = GameObject.Find("bntSkip").GetComponent<Button>(); else return;
        if (demoGame01 == null) demoGame01 = GameObject.Find("DemoGame01"); else return;
        if (demoGame02 == null) demoGame02 = GameObject.Find("DemoGame02"); else return;
        if (demoGame03 == null) demoGame03 = GameObject.Find("DemoGame03"); else return;
        if (demoGame04 == null) demoGame04 = GameObject.Find("DemoGame04"); else return;
        bntSkip.onClick.AddListener(this.OnClickDemo);
    }

    private void OnEnable()
    {
        if (DataGame.Instance.dataSave.IsCheckDemo[0])
        {
            TableText.SetActive(false);
            demoGame01.SetActive(false);
            demoGame02.SetActive(false);
            demoGame03.SetActive(false);
            demoGame04.SetActive(false);
            objHand.SetActive(false);
            bntSkip.gameObject.SetActive(false);
            gameObject.GetComponent<DemoGame>().enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TableText.SetActive(false);
        IsCheckDemo = false;
        isCheck = true;
        objHand.SetActive(false);
        originPos = new Vector2(0.55f, -2.8f);
        pos = new Vector2(-0.15f, -1);
        countDemo = 0;
        demoGame01.SetActive(false);
        demoGame02.SetActive(false);
        demoGame03.SetActive(false);
        demoGame04.SetActive(false);
        bntSkip.gameObject.SetActive(false);
    }
    private void Update()
    {
        this.MoveHand();
    }

    private void MoveHand()
    {
        if (isCheck && !IsCheckDemo && GameStateController.Instance.CurrentGameState == GameState.Demo)
        {
            isCheck = false;
            objHand.SetActive(true);
            objHand.transform.DOMove(pos, 2f).OnComplete(() =>
            {
                objHand.SetActive(false);
                objHand.transform.DOMove(originPos, 0.5f).OnComplete(() =>
                {
                    if (!IsCheckDemo)
                        objHand.SetActive(true);
                    isCheck = true;
                });
            });
        }
    }

    public void Demo01()
    {
        this.demoGame01.SetActive(true);
        //DataGame.Instance.dataSave.IsCheckDemo[0] = true;
        bntSkip.gameObject.SetActive(true);
        objHand.SetActive(false);
    }

    private void OnClickDemo()
    {
        countDemo++;
        if (countDemo == 1)
        {
            this.demoGame01.SetActive(false);
            this.demoGame02.SetActive(true);
        }
        if (countDemo == 2)
        {
            this.demoGame02.SetActive(false);
            this.demoGame03.SetActive(true);
        }
        if (countDemo == 3)
        {
            this.demoGame03.SetActive(false);
            this.demoGame04.SetActive(true);
        }
        if (countDemo == 4)
        {
            this.demoGame04.SetActive(false);
            this.fillDemoGame.SetActive(false);
            bntSkip.gameObject.SetActive(false);
            GameStateController.Instance.CurrentGameState = GameState.Dragging;
            DataGame.Instance.dataSave.IsCheckDemo[0] = true;
            GameStateController.Instance.Ads.gameObject.SetActive(true);
            GameStateController.Instance.Setting.gameObject.SetActive(true);
            GameStateController.Instance.ObjAbility.gameObject.SetActive(true);
            DataGame.Instance.SaveData();
            TableText.SetActive(false);
            this.gridController.CreateBox(0.7f);
            this.gridController.CreateBox(2.3f);
        }
    }

}
