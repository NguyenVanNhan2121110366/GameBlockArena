using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Variable
    private SceneTransition sceneTransition;
    [SerializeField] private Transform sceneParent;
    [SerializeField] private int level;
    public static GameController Instance;
    [SerializeField] private GameObject[] scenes;
    #endregion
    #region Public
    public int Level { get => level; set => level = value; }
    public GameObject[] Scenes { get => scenes; set => scenes = value; }
    #endregion
    private void Awake()
    {
        sceneTransition = FindFirstObjectByType<SceneTransition>();
        if (Instance == null) Instance = this; else Destroy(gameObject);
        this.sceneParent = GameObject.Find("AllLevel").GetComponent<Transform>();
        scenes = new GameObject[35];
        this.GetSceneIntoGrid();
        level = DataGame.Instance.dataSave.Level[0];
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("ClickContinue") == 2)
        {
            Destroy(SpawnScene.Instance.CurrentLevel);
            sceneTransition.ReturnToLevel1();
            PlayerPrefs.SetInt("ClickContinue", 1);
            PlayerPrefs.Save();
            DataGame.Instance.dataSave.CurentScore[0] = 0;
            DataGame.Instance.dataSave.Level[0] = 0;
            DataGame.Instance.SaveData();
            Debug.Log("Vaodayne");

            ScoreUIPlayGame.Instance.CountScore = DataGame.Instance.dataSave.CurentScore[0];
            ObserverManager.Instance.ShareScoreObj = DataGame.Instance.dataSave.CurentScore[0];
            ScoreUIPlayGame.Instance.TxtScoreObj.text = ObserverManager.Instance.ShareScoreObj.ToString();
        }
    }

    private void GetSceneIntoGrid()
    {
        for (var i = 0; i < sceneParent.childCount; i++)
            scenes[i] = sceneParent.GetChild(i).gameObject;
    }
}
