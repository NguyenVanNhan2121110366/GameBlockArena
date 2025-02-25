using UnityEngine;
public class SceneTransition : MonoBehaviour
{
    private GridController gridController;
    private CheckQuantityObj checkQuantityObj;

    private void Awake()
    {
        this.gridController = FindFirstObjectByType<GridController>();
    }
    public void NextLevel()
    {
        //ScoreUIPlayGame.Instance.ResetScore();
        checkQuantityObj = FindFirstObjectByType<CheckQuantityObj>();
        checkQuantityObj.ObjNextGame.SetActive(false);
        Destroy(SpawnScene.Instance.CurrentLevel);
        this.gridController.DestroyList();
        this.gridController.SpawnDot();
        //GameController.Instance.Level++;
        var level = GameController.Instance.Level;
        var scene = Instantiate(GameController.Instance.Scenes[level], transform.position, Quaternion.identity);
        scene.SetActive(true);
        TurnGame.Instance.TurnByLevel();
        TurnGame.Instance.UpdateTextTurn();
        SpawnScene.Instance.CurrentLevel = scene;
        this.gridController.ObjFrameBorder.SetActive(true);
        DataGame.Instance.dataSave.Level[0] = level;
        DataGame.Instance.SaveData();
    }

    public void RestartLevel()
    {
        this.gridController.CheckDestroyList = false;
        ScoreUIPlayGame.Instance.RestartScore();
        Destroy(SpawnScene.Instance.CurrentLevel);
        //EndGameController.Instance.ObjGameOver.SetActive(false);
        EndGameController.Instance.IsTurn = false;
        EndGameController.Instance.IsFull = false;
        this.gridController.DestroyList();
        var level = GameController.Instance.Level;
        var scene = Instantiate(GameController.Instance.Scenes[level], transform.position, Quaternion.identity);
        scene.SetActive(true);
        SpawnScene.Instance.CurrentLevel = scene;
        TurnGame.Instance.TurnByLevel();
        TurnGame.Instance.UpdateTextTurn();
        this.gridController.SpawnDot();
        this.gridController.ObjFrameBorder.SetActive(true);
    }

    public void ReturnToLevel1()
    {
        //this.gridController.CheckDestroyList = false;
        ScoreUIPlayGame.Instance.ResetScore();
        Destroy(SpawnScene.Instance.CurrentLevel);
        EndGameController.Instance.ObjGameOver.SetActive(false);
        EndGameController.Instance.IsTurn = false;
        EndGameController.Instance.IsFull = false;
        this.gridController.DestroyList();
        GameController.Instance.Level = 0;
        var scene = Instantiate(GameController.Instance.Scenes[GameController.Instance.Level], transform.position, Quaternion.identity);
        scene.SetActive(true);
        SpawnScene.Instance.CurrentLevel = scene;
        TurnGame.Instance.TurnByLevel();
        TurnGame.Instance.UpdateTextTurn();
        if (PlayerPrefs.GetString("WinGame", "Null") != "Win" && PlayerPrefs.GetInt("ClickContinue") != 2)
        {
            this.gridController.SpawnDot();
            Debug.Log("Vao day");
        }

        this.gridController.ObjFrameBorder.SetActive(true);
    }
}
