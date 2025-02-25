using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingGame : ClickButtonManager
{
    public static SettingGame Instance;
    private RectTransform rectObjUI;
    private float duration = 0.3f;
    private Button bntSetting;
    private GameObject objUI;
    private GameObject objBntDotMusic;
    private Button bntDotMusic;
    private GameObject bntMusicbgr;
    private GameObject objBntSound;
    private GameObject bntSound;
    private Button bntDotSound;
    private GameObject objBntRing;
    private GameObject bntRing;
    private Button bntDotRing;
    private Button bntNo;
    private Button bntYes;
    private Button bntAgain;
    private RectTransform rectAttenion;
    private GameObject objAttentionPause;
    private bool isCheckHaptic;
    public bool IsCheckHaptic { get => isCheckHaptic; set => isCheckHaptic = value; }


    private void Awake()
    {
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
        this.SceneTransition = FindFirstObjectByType<SceneTransition>();
        if (rectAttenion == null) rectAttenion = GameObject.Find("bgrAttentionPauseGame").GetComponent<RectTransform>();
        if (bntSetting == null) bntSetting = GameObject.Find("bntSetting").GetComponent<Button>(); else Debug.LogError("bntSetting not null");
        if (objUI == null) objUI = GameObject.Find("PauseGame");
        if (rectObjUI == null) rectObjUI = GameObject.Find("PauseGame").GetComponent<RectTransform>();
        if (BntContinue == null) BntContinue = GameObject.Find("bntContinuePauseGame").GetComponent<Button>();
        if (BntHome == null) BntHome = GameObject.Find("bntHomePause").GetComponent<Button>();
        if (bntYes == null) bntYes = GameObject.Find("bntYesPause").GetComponent<Button>();
        if (bntNo == null) bntNo = GameObject.Find("bntNoPause").GetComponent<Button>();
        if (objAttentionPause == null) objAttentionPause = GameObject.Find("bgrAttentionPauseGame");
        if (bntAgain == null) bntAgain = GameObject.Find("bntAgainPause").GetComponent<Button>();
        // Background Music
        if (objBntDotMusic == null) objBntDotMusic = GameObject.Find("bntDotMusic");
        if (bntDotMusic == null) bntDotMusic = GameObject.Find("bntDotMusic").GetComponent<Button>();
        if (bntMusicbgr == null) bntMusicbgr = GameObject.Find("bntMusicbgr");
        // Sound Effects
        if (bntDotSound == null) bntDotSound = GameObject.Find("bntDotSound").GetComponent<Button>();
        if (objBntSound == null) objBntSound = GameObject.Find("bntDotSound");
        if (bntSound == null) bntSound = GameObject.Find("bntSound");
        // Ring
        // if (bntRing == null) bntRing = GameObject.Find("bntRing");
        // if (objBntRing == null) objBntRing = GameObject.Find("bntDotRing");
        // if (bntDotRing == null) bntDotRing = GameObject.Find("bntDotRing").GetComponent<Button>();
        //Click Button
        bntDotMusic.onClick.AddListener(OnClickbntDotMusic);
        bntDotSound.onClick.AddListener(OnClickbntSoundEffects);
        //bntDotRing.onClick.AddListener(OnClickbntRing);
        BntContinue.onClick.AddListener(OnClickContinue);
        BntHome.onClick.AddListener(OnClickHome);
        bntSetting.onClick.AddListener(OnClickSetting);
        bntYes.onClick.AddListener(OnCLickYes);
        bntNo.onClick.AddListener(OnClickNo);
        bntAgain.onClick.AddListener(OnClickAgain);
    }
    // Start is called before the first frame update
    void Start()
    {
        isCheckHaptic = false;
        objUI.SetActive(false);
        objAttentionPause.SetActive(false);
    }

    private void OnClickbntDotMusic()
    {
        //HandleVibrate();
        // this.CheckAllClickToggle(objBntDotMusic, bntMusicbgr);
        objBntDotMusic.transform.DOLocalMoveX(-objBntDotMusic.transform.localPosition.x, duration);
        var toggle = Mathf.Sign(-objBntDotMusic.transform.localPosition.x);
        if (toggle > 0)
        {
            AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
            AudioManager.Instance.BgrMusic.volume = 0.5f;
            bntMusicbgr.GetComponent<Animator>().SetTrigger("ToggleOn");
            PlayerPrefs.SetInt("Music", 2);
            PlayerPrefs.Save();
            DataGame.Instance.SaveData();
        }
        else
        {
            AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
            AudioManager.Instance.BgrMusic.volume = 0f;
            bntMusicbgr.GetComponent<Animator>().SetTrigger("ToggleOf");
            PlayerPrefs.SetInt("Music", 3);
            PlayerPrefs.Save();
            DataGame.Instance.SaveData();
        }
    }

    private void OnClickbntSoundEffects()
    {
        //HandleVibrate();
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
        // this.CheckAllClickToggle(objBntSound, bntSound);
        objBntSound.transform.DOLocalMoveX(-objBntSound.transform.localPosition.x, duration);
        var toggle = Mathf.Sign(-objBntSound.transform.localPosition.x);
        if (toggle > 0)
        {
            AudioManager.Instance.AudioSource.volume = 1f;
            bntSound.GetComponent<Animator>().SetTrigger("ToggleOn");
            PlayerPrefs.SetInt("Effects", 2);
            PlayerPrefs.Save();
            DataGame.Instance.dataSave.Pos[0] = -objBntSound.transform.localPosition.x;
            DataGame.Instance.SaveData();
        }
        else
        {
            AudioManager.Instance.AudioSource.volume = 0f;
            bntSound.GetComponent<Animator>().SetTrigger("ToggleOf");
            PlayerPrefs.SetInt("Effects", 3);
            PlayerPrefs.Save();
            DataGame.Instance.dataSave.Pos[0] = -objBntSound.transform.localPosition.x;
            DataGame.Instance.SaveData();
        }
    }

    private void OnClickbntRing()
    {
        // this.CheckAllClickToggle(objBntRing, bntRing);
        objBntRing.transform.DOLocalMoveX(-objBntRing.transform.localPosition.x, duration);
        var toggle = Mathf.Sign(-objBntRing.transform.localPosition.x);
        if (toggle > 0)
        {
            isCheckHaptic = true;
            //HandleVibrate();
            PlayerPrefs.SetInt("HapticFeedBack", 1);
            PlayerPrefs.Save();
            bntRing.GetComponent<Animator>().SetTrigger("ToggleOn");
        }
        else
        {
            PlayerPrefs.SetInt("HapticFeedBack", 0);
            PlayerPrefs.Save();
            isCheckHaptic = false;
            bntRing.GetComponent<Animator>().SetTrigger("ToggleOf");
        }
    }

    // public void HandleVibrate()
    // {
    //     if (isCheckHaptic)
    //         Handheld.Vibrate();
    // }

    public override void OnClickContinue()
    {
        //HandleVibrate();
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
        rectObjUI.DOAnchorPosY(-1500, 0.5f).OnComplete(() =>
        {
            ScoreWinGame.Instance.FillWinAndLose.SetActive(false);
            objUI.SetActive(false);
        });
    }

    public override void OnClickHome()
    {
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
        rectObjUI.DOAnchorPosY(-1500, 0.5f).OnComplete(() =>
        {
            objUI.SetActive(false);
            BannerAdsManager.Instance.DestroyBanner();
            SceneManager.LoadScene("Loading");
        });
    }

    private void OnClickAgain()
    {
        //HandleVibrate();
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
        rectObjUI.DOAnchorPosY(-1500, 0.5f).OnComplete(() =>
        {
            objUI.SetActive(false);
            objAttentionPause.SetActive(true);
            rectAttenion.DOAnchorPosY(-10, 1f).OnComplete(() =>
            {

            });
        });
    }

    private void OnCLickYes()
    {
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
        rectAttenion.DOAnchorPosY(-1500, 0.5f).OnComplete(() =>
        {
            ScoreWinGame.Instance.FillWinAndLose.SetActive(false);
            GameStateController.Instance.CurrentGameState = GameState.Again;
            SceneTransition.ReturnToLevel1();
            DataGame.Instance.dataSave.CurentScore[0] = 0;
            DataGame.Instance.dataSave.Level[0] = 0;
            DataGame.Instance.SaveData();
            StartCoroutine(this.DelayGameState());
        });
    }

    private IEnumerator DelayGameState()
    {
        yield return null;
        GameStateController.Instance.CurrentGameState = GameState.Dragging;
    }

    private void OnClickNo()
    {
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
        rectAttenion.DOAnchorPosY(-1500, 0.5f).OnComplete(() =>
        {
            objAttentionPause.SetActive(false);
            objUI.SetActive(true);
            rectObjUI.DOAnchorPosY(-10, 0.5f).OnComplete(() =>
            {

            });
        });
    }

    public override void OnClickSetting()
    {
        //HandleVibrate();
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
        base.OnClickSetting();
        ScoreWinGame.Instance.FillWinAndLose.SetActive(true);
        objUI.SetActive(true);
        if (DataGame.Instance.dataSave.Pos[0] == 0)
        {
            DataGame.Instance.dataSave.Pos[0] = 50;
            DataGame.Instance.SaveData();
        }
        StartCoroutine(Delay());
        rectObjUI.DOAnchorPosY(-10, 0.5f);
    }

    private IEnumerator Delay()
    {
        // Effects
        if (PlayerPrefs.GetInt("Effects") == 3)
        {
            objBntSound.transform.localPosition = new Vector2(-50, objBntSound.transform.localPosition.y);
            bntSound.GetComponent<Animator>().SetTrigger("ToggleOf");
        }
        else
        {
            objBntSound.transform.localPosition = new Vector2(50, objBntSound.transform.localPosition.y);
            bntSound.GetComponent<Animator>().SetTrigger("ToggleOn");
        }

        // Music bgr
        if (PlayerPrefs.GetInt("Music") == 3)
        {
            objBntDotMusic.transform.localPosition = new Vector2(-50, objBntDotMusic.transform.localPosition.y);
            bntMusicbgr.GetComponent<Animator>().SetTrigger("ToggleOf");
        }
        else
        {
            objBntDotMusic.transform.localPosition = new Vector2(50, objBntDotMusic.transform.localPosition.y);
            bntMusicbgr.GetComponent<Animator>().SetTrigger("ToggleOn");
        }

        // Haptic feedback

        yield return new WaitForSeconds(1f);
    }
}
