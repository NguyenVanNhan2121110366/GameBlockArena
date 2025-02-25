using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class AnimationDaillyRewarded : MonoBehaviour
{
    private GameObject fillDiamonPlus;
    [SerializeField] private GameObject objDiamon;
    public static AnimationDaillyRewarded Instance;
    private Button bntExtDaillyReward;
    private GameObject fill;
    private Button bntGift;
    private GameObject objDailyReward;
    private RectTransform recObjDailyReward;
    [SerializeField] private Ease ease;
    [SerializeField] private float x, y;
    //private List<GameObject> diamons;
    private Queue<GameObject> diamons = new Queue<GameObject>();

    public GameObject FillDiamonPlus { get => fillDiamonPlus; set => fillDiamonPlus = value; }

    private void Awake()
    {
        if (fillDiamonPlus == null) fillDiamonPlus = GameObject.Find("fillPlusDiamon"); else Debug.LogWarning("fillDiamonPlus not null");
        if (Instance == null) Instance = this; else Debug.LogWarning("Instance not null");
        //Reward
        if (bntExtDaillyReward == null) bntExtDaillyReward = GameObject.Find("bntExit").GetComponent<Button>();
        if (fill == null) fill = GameObject.Find("fillSetting");
        if (bntGift == null) bntGift = GameObject.Find("bntGift").GetComponent<Button>();
        if (objDailyReward == null) objDailyReward = GameObject.Find("DaillyRewardObj");
        if (recObjDailyReward == null) recObjDailyReward = GameObject.Find("DaillyRewardObj").GetComponent<RectTransform>();
        bntGift.onClick.AddListener(OnClickGift);
        bntExtDaillyReward.onClick.AddListener(OnClickExitDailyReward);
    }
    // Start is called before the first frame update
    void Start()
    {
        fillDiamonPlus.SetActive(false);
        fill.SetActive(false);
        objDailyReward.SetActive(false);
        this.SpawnDiamon();
    }

    private void OnClickGift()
    {
        fill.SetActive(true);
        objDailyReward.SetActive(true);
        recObjDailyReward.DOAnchorPosY(-10, 1f);
    }

    private void OnClickExitDailyReward()
    {
        
        recObjDailyReward.DOAnchorPosY(-1800, 1f).OnComplete(() =>
        {
            fill.SetActive(false);
            objDailyReward.SetActive(false);
        });
    }

    private void SpawnDiamon()
    {
        for (var i = 0; i < 20; i++)
        {
            var diamon = Instantiate(objDiamon, objDiamon.transform.position, objDiamon.transform.rotation);
            diamons.Enqueue(diamon);
        }

    }

    public void AnimatePlus(int amount)
    {
        var countPlus = DataGame.Instance.dataSave.Diamon[0];
        // var count = DataGame.Instance.dataSave.Diamon[0];
        var pos = new Vector2(-1.9f, 4.3f);
        fill.SetActive(false);
        for (var i = 0; i < amount; i += 50)
        {
            if (diamons.Count > 0)
            {
                var obj = diamons.Dequeue();
                obj.SetActive(true);
                var duration = Random.Range(x, y);
                obj.transform.DOMove(pos, duration).SetEase(ease).OnComplete(() =>
                {
                    
                    countPlus += 50;
                    Debug.Log(countPlus);
                    UIHomeController.Instance.TxtDiamon.text = countPlus.ToString();
                    DataGame.Instance.dataSave.Diamon[0] = countPlus;
                    DataGame.Instance.SaveData();
                    obj.SetActive(false);
                    // Debug.Log(countPlus);
                    // Debug.Log(UIHomeController.Instance.TxtDiamon.text = countPlus.ToString());
                    // Debug.Log(DataGame.Instance.dataSave.Diamon[0] = countPlus);
                });
            }

        }
        recObjDailyReward.DOAnchorPosY(-1800, 2f).OnComplete(() =>
        {
            objDailyReward.SetActive(false);
            // UIHomeController.Instance.TxtDiamon.text = countPlus.ToString();
            // DataGame.Instance.dataSave.Diamon[0] = countPlus;
            // DataGame.Instance.SaveData();
            fillDiamonPlus.SetActive(false);
            Debug.Log("Co vao trong day khong ???");
        });

    }
}
