using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReceiveDailyGiftManager : MonoBehaviour
{
    [SerializeField] private Button[] bntReceiveDailyGifts = new Button[5];
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject[] objFill = new GameObject[5];
    [SerializeField] private GameObject[] fillToReceiveGift = new GameObject[5];
    public Button[] BntReceiveDailyGifts { get => bntReceiveDailyGifts; set => bntReceiveDailyGifts = value; }
    public GameObject[] ObjFill { get => objFill; set => objFill = value; }
    public GameObject[] FillToReceiveGift { get => fillToReceiveGift; set => fillToReceiveGift = value; }


    private void Awake()
    {
        this.parent = GameObject.Find("BgrReceiveDailygift").GetComponent<Transform>();
        this.GetButtonIntoArray();
        this.GetObjFillToReceiveGift();
        this.DisableFill();
    }
    // Start is called before the first frame update
    void Start()
    {
        ReceiveDailyGift.Instance.EnableBntDayReceiveGift();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetButtonIntoArray()
    {
        for (var i = 0; i < parent.childCount; i++)
        {
            bntReceiveDailyGifts[i] = parent.GetChild(i).gameObject.GetComponent<Button>();
            //bntReceiveDailyGifts[i] = GameObject.Find("giftDay" + i + 1).GetComponent<Button>();
            //bntReceiveDailyGifts[i].onClick.AddListener(ReceiveDailyGift.Instance.DailyGift);
            //bntReceiveDailyGifts[i].GetComponent<Button>().enabled = false;
        }
    }

    private void DisableFill()
    {
        for (var i = 0; i < 5; i++)
        {
            objFill[i].SetActive(false);
        }
    }

    private void GetObjFillToReceiveGift()
    {
        for (var i = 0; i < parent.childCount; i++)
        {
            fillToReceiveGift[i] = parent.GetChild(i).GetChild(1).gameObject;
            fillToReceiveGift[i].SetActive(false);
        }
    }
}
