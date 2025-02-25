using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using DG.Tweening;
public class UIHomeController : MonoBehaviour
{

    public static UIHomeController Instance;
    private GameObject objBntDotMusic;
    private GameObject fillStartGame;
    private float duration = 0.3f;
    private Button bntDotMusic;
    private GameObject bntMusicbgr;
    private GameObject objBntSound;
    private GameObject bntSound;
    private Button bntDotSound;
    private GameObject objBntRing;
    private GameObject bntRing;
    //private Button bntDotRing;
    private Button bntContinue;
    private GameObject objSetting;
    [SerializeField] private Button bntSetting;
    [SerializeField] private RectTransform rectSetting;
    private GameObject fillSetting;
    public bool IsCheckHaptic { get => isCheckHaptic; set => isCheckHaptic = value; }

    [SerializeField] private Button bntLanguage;
    [SerializeField] private GameObject chooseLanguage;
    [SerializeField] private GameObject fillHome;
    private TextMeshProUGUI txtDiamon;
    private TextMeshProUGUI txtHightScore;
    private bool isCheck;
    private bool isCheckHaptic;
    [SerializeField] private GameObject fillChooseLanguage;
    public GameObject ChooseLanguage { get => chooseLanguage; set => chooseLanguage = value; }
    public TextMeshProUGUI TxtDiamon { get => txtDiamon; set => txtDiamon = value; }
    public GameObject FillChoseLanguage { get => fillChooseLanguage; set => fillChooseLanguage = value; }
    public bool IsCheck { get => isCheck; set => isCheck = value; }
    private void Awake()
    {
        //
        if (fillStartGame == null) fillStartGame = GameObject.Find("fillStartGame");
        if (rectSetting == null) rectSetting = GameObject.Find("SettingHome").GetComponent<RectTransform>();
        if (fillSetting == null) fillSetting = GameObject.Find("fillSetting");
        if (bntSetting == null) bntSetting = GameObject.Find("bntSettingGame").GetComponent<Button>();
        if (objSetting == null) objSetting = GameObject.Find("SettingHome");
        // Background Music
        if (objBntDotMusic == null) objBntDotMusic = GameObject.Find("bntDotMusic");
        if (bntDotMusic == null) bntDotMusic = GameObject.Find("bntDotMusic").GetComponent<Button>();
        if (bntMusicbgr == null) bntMusicbgr = GameObject.Find("bntMusicbgr");
        // Sound Effects
        if (bntDotSound == null) bntDotSound = GameObject.Find("bntDotSound").GetComponent<Button>();
        if (objBntSound == null) objBntSound = GameObject.Find("bntDotSound");
        if (bntSound == null) bntSound = GameObject.Find("bntSound");
        // Ring
        if (bntRing == null) bntRing = GameObject.Find("bntRing");
        if (objBntRing == null) objBntRing = GameObject.Find("bntDotRing");
        //if (bntDotRing == null) bntDotRing = GameObject.Find("bntDotRing").GetComponent<Button>();

        if (bntContinue == null) bntContinue = GameObject.Find("bntContinue").GetComponent<Button>();
        if (fillChooseLanguage == null) fillChooseLanguage = GameObject.Find("fillChooseLanguage"); else Debug.LogError("fill not null");
        if (txtDiamon == null) txtDiamon = GameObject.Find("txtDiamon").GetComponent<TextMeshProUGUI>(); else Debug.LogError("txtDiamon not null");
        if (txtHightScore == null) txtHightScore = GameObject.Find("txtHightScore").GetComponent<TextMeshProUGUI>(); else Debug.LogError("txtHightScore not null");
        if (Instance == null) Instance = this; else Destroy(gameObject);
        if (bntLanguage == null) bntLanguage = GameObject.Find("bntLanguage").GetComponent<Button>(); else Debug.LogError("bntLanguage Not null");
        if (chooseLanguage == null) chooseLanguage = GameObject.Find("ChooseLanguage");
        bntLanguage.onClick.AddListener(this.ClickButton);

        bntDotMusic.onClick.AddListener(OnClickbntDotMusic);
        bntDotSound.onClick.AddListener(OnClickbntSoundEffects);
        //bntDotRing.onClick.AddListener(OnClickbntRing);
        bntContinue.onClick.AddListener(OnClickContinue);

        bntSetting.onClick.AddListener(OnClickSetting);
    }
    private void Start()
    {
        fillSetting.SetActive(false);
        objSetting.SetActive(false);
        fillChooseLanguage.SetActive(false);
        txtDiamon.text = DataGame.Instance.dataSave.Diamon[0].ToString();
        txtHightScore.text = DataGame.Instance.dataSave.HightScore[0].ToString();
        isCheck = true;
        chooseLanguage.SetActive(false);
    }


    private void ClickButton()
    {
        if (isCheck)
        {
            isCheck = false;
            fillChooseLanguage.SetActive(true);
            chooseLanguage.SetActive(true);

        }
        else
        {
            isCheck = true;
            fillChooseLanguage.SetActive(false);
            chooseLanguage.SetActive(false);

        }
    }

    private void OnClickbntDotMusic()
    {
        HandleVibrate();
        // this.CheckAllClickToggle(objBntDotMusic, bntMusicbgr);
        objBntDotMusic.transform.DOLocalMoveX(-objBntDotMusic.transform.localPosition.x, duration);
        var toggle = Mathf.Sign(-objBntDotMusic.transform.localPosition.x);
        if (toggle > 0)
        {
            AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
            AudioManager.Instance.BgrMusic.volume = 1f;
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
        HandleVibrate();
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

    // private void OnClickbntRing()
    // {
    //     // this.CheckAllClickToggle(objBntRing, bntRing);
    //     objBntRing.transform.DOLocalMoveX(-objBntRing.transform.localPosition.x, duration);
    //     var toggle = Mathf.Sign(-objBntRing.transform.localPosition.x);
    //     if (toggle > 0)
    //     {
    //         isCheckHaptic = true;
    //         HandleVibrate();
    //         PlayerPrefs.SetInt("HapticFeedBack", 1);
    //         PlayerPrefs.Save();
    //         bntRing.GetComponent<Animator>().SetTrigger("ToggleOn");
    //     }
    //     else
    //     {
    //         PlayerPrefs.SetInt("HapticFeedBack", 0);
    //         PlayerPrefs.Save();
    //         isCheckHaptic = false;
    //         bntRing.GetComponent<Animator>().SetTrigger("ToggleOf");
    //     }
    // }

    public void HandleVibrate()
    {
        if (isCheckHaptic)
            Handheld.Vibrate();
    }

    private void OnClickContinue()
    {
        HandleVibrate();
        rectSetting.DOAnchorPosY(-1600, 1f).OnComplete(() =>
        {
            objSetting.SetActive(false);
            fillSetting.SetActive(false);
        });
    }

    private void OnClickSetting()
    {
        HandleVibrate();
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.SoundDestroy[3]);
        fillSetting.SetActive(true);
        objSetting.SetActive(true);
        if (DataGame.Instance.dataSave.Pos[0] == 0)
        {
            DataGame.Instance.dataSave.Pos[0] = 50;
            DataGame.Instance.SaveData();
        }
        StartCoroutine(Delay());
        rectSetting.DOAnchorPosY(-10, 1f).OnComplete(() =>
        {

        });
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

        // if (PlayerPrefs.GetInt("HapticFeedBack") == 1)
        // {
        //     objBntRing.transform.localPosition = new Vector2(50, objBntRing.transform.localPosition.y);
        //     bntRing.GetComponent<Animator>().SetTrigger("ToggleOn");
        // }
        // else
        // {
        //     objBntRing.transform.localPosition = new Vector2(-50, objBntRing.transform.localPosition.y);
        //     bntRing.GetComponent<Animator>().SetTrigger("ToggleOf");
        // }

        yield return new WaitForSeconds(1f);
    }
}
