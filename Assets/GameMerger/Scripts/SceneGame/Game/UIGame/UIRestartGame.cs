using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIRestartGame : MonoBehaviour
{
    public static UIRestartGame Instance;
    public GameObject UIRestartGameObj;
    private Button bntContinue;
    public RectTransform RectUIRestartGame;

    private void Awake()
    {
        // if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
        // if (UIRestartGameObj == null) UIRestartGameObj = GameObject.Find("UIRestartGame"); else Debug.LogError("uiRestartGame not null");
        // if (RectUIRestartGame == null) RectUIRestartGame = GameObject.Find("UIRestartGame").GetComponent<RectTransform>(); else Debug.LogError("uiRestartGame not null");
        // if (bntContinue == null) bntContinue = GameObject.Find("bntContinueRestartGame").GetComponent<Button>();
        // bntContinue.onClick.AddListener(OnClickContinue);
    }
    // Start is called before the first frame update
    void Start()
    {
        //UIRestartGameObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnClickContinue()
    {
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
        RectUIRestartGame.DOAnchorPosY(-1600, 0.5f).OnComplete(() =>
        {
            ScoreWinGame.Instance.FillWinAndLose.gameObject.SetActive(false);
            UIRestartGameObj.SetActive(false);
        });

    }
}
