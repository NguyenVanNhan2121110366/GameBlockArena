using UnityEngine;

public class SpawnScene : MonoBehaviour
{
    public static SpawnScene Instance;
    private SceneTransition sceneTransition;
    #region Variable
    [SerializeField] private GameObject level;
    [SerializeField] private GameObject currentLevel;
    #endregion

    #region Public
    public GameObject CurrentLevel { get => currentLevel; set => currentLevel = value; }
    #endregion

    private void Awake()
    {
        if (sceneTransition == null) sceneTransition = FindFirstObjectByType<SceneTransition>();
        else Debug.Log("sceneTransition was exits");
        if (Instance == null) Instance = this; else Debug.Log("Not null");
    }
    void Start()
    {
        level = GameController.Instance.Scenes[GameController.Instance.Level];
        var scene = Instantiate(level, transform.position, Quaternion.identity);
        currentLevel = scene;
        scene.SetActive(true);
        if (PlayerPrefs.GetString("WinGame", "null") == "Win")
        {
            DataGame.Instance.dataSave.CurentScore[0] = 0;
            DataGame.Instance.dataSave.Level[0] = 0;
            DataGame.Instance.SaveData();
            sceneTransition.ReturnToLevel1();
            PlayerPrefs.SetString("WinGame", "Not Win Yet");
            PlayerPrefs.Save();
        }
    }
}
