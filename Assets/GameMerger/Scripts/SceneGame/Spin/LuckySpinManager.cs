using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LuckySpinManager : MonoBehaviour
{
    [Header("Position A and B")]
    [SerializeField] private Transform posA;
    [SerializeField] private Transform posB;
    [Header("Text and array")]
    [SerializeField] private TextMeshProUGUI txtAdsSpin;
    [SerializeField] private GameObject[] allPoints;
    private bool isStop;
    private string namePoint;
    private Transform currentPoint;
    [Header("Object")]
    [SerializeField] private GameObject objPoint;

    #region public
    public static LuckySpinManager Instance;
    public bool IsStop { get => isStop; set => isStop = value; }
    public string NamePoint { get => namePoint; set => namePoint = value; }
    #endregion
    private void Awake()
    {
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
        if (posA == null) posA = GameObject.Find("PosA").GetComponent<Transform>(); else Debug.LogError("pos A not null");
        if (posB == null) posB = GameObject.Find("PosB").GetComponent<Transform>(); else Debug.LogError("pos B not null");
        //if (objPoint == null) objPoint = GameObject.Find("PointCheck"); else Debug.LogError("objPoint not null");
    }
    // Start is called before the first frame update
    void Start()
    {
        currentPoint = posA;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop)
        {
            this.MovePoint();
            this.CheckPoint();
        }
    }

    private void MovePoint()
    {
        objPoint.transform.position = Vector2.MoveTowards(objPoint.transform.position, currentPoint.position, Time.deltaTime * 1f);
        if (currentPoint == posA && Vector2.Distance(currentPoint.position, objPoint.transform.position) < 0.1f)
        {
            currentPoint = posB;
        }
        else if (currentPoint == posB && Vector2.Distance(currentPoint.position, objPoint.transform.position) < 0.1f)
        {
            currentPoint = posA;
        }
    }

    private void CheckPoint()
    {
        foreach (var point in allPoints)
        {
            if (Mathf.Abs(point.transform.position.x - objPoint.transform.position.x) <= 0.22f)
            {
                txtAdsSpin.text = point.name;
                namePoint = point.name;
            }
        }
    }

    public int ManyTimes()
    {
        var diamon = 0;
        switch (namePoint)
        {
            case "x2":
                diamon = ScoreUIPlayGame.Instance.CountDiamon * 2;
                break;
            case "x3":
                diamon = ScoreUIPlayGame.Instance.CountDiamon * 3;
                break;
            case "x4":
                diamon = ScoreUIPlayGame.Instance.CountDiamon * 4;
                break;
            case "x5":
                diamon = ScoreUIPlayGame.Instance.CountDiamon * 5;
                break;
            default:
                Debug.Log("Khong co gi");
                break;
        }
        return diamon;
    }
}
