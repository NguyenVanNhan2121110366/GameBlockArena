
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public abstract class ClickButtonManager : MonoBehaviour
{
    [SerializeField] private Button bntNothank;
    [SerializeField] private Button bntHome;
    [SerializeField] private Button bntContinue;
    [SerializeField] private Button bntAdsRewarded;
    public SceneTransition SceneTransition;
    public GridController gridController;
    public Button BntNoThank { get => bntNothank; set => bntNothank = value; }
    public Button BntHome { get => bntHome; set => bntHome = value; }
    public Button BntContinue { get => bntContinue; set => bntContinue = value; }
    public Button BntRewarded { get => bntAdsRewarded; set => bntAdsRewarded = value; }


    public virtual void CheckButtonNextGame() { }
    public virtual void CheckButtonHome(string name) { }
    public virtual void OnClickNextGame() { }
    public virtual void OnClickNoThank() { }
    public virtual void OnClickHome()
    {
        ScoreLoseGame.Instance.UpdateHighScoreUI();
        StartCoroutine(UpdateGameState());
        Debug.Log("Home");
    }

    private IEnumerator UpdateGameState()
    {
        ///
        ScoreLoseGame.Instance.UpdateHighScoreUI();
        DataGame.Instance.SaveData();
        GameStateController.Instance.CurrentGameState = GameState.BackHome;
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Home");
    }

    public virtual void OnClickAds() { }
    public virtual void OnClickContinue() { }
    public virtual void OnClickResume() { }
    public virtual void OnClickSetting() { }
    public virtual void OnClickExit() { }
}
