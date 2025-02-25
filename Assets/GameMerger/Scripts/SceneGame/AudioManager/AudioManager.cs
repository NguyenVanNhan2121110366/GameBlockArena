using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource bgrMusic;
    [SerializeField] private AudioClip[] soundDestroy;
    public AudioClip[] SoundDestroy { get => soundDestroy; set => soundDestroy = value; }
    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }
    public AudioSource BgrMusic { get => bgrMusic; set => bgrMusic = value; }
    private void Awake()
    {
        if (Instance == null) Instance = this; else Debug.LogError("Instance not null");
        if (bgrMusic == null) bgrMusic = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        this.audioSource = GetComponent<AudioSource>();

    }
    private void Start()
    {
        //Haptic feedback
        if (PlayerPrefs.GetInt("HapticFeedBack") == 0)
        {
            if (SettingGame.Instance)
                SettingGame.Instance.IsCheckHaptic = false;
            if (UIHomeController.Instance)
                UIHomeController.Instance.IsCheckHaptic = false;
            PlayerPrefs.SetInt("HapticFeedBack", 0);
            PlayerPrefs.Save();
        }

        //Music bgr
        if (PlayerPrefs.GetInt("Music") == 0)
            bgrMusic.volume = 0.5f;
        else if (PlayerPrefs.GetInt("Music") == 2)
            bgrMusic.volume = 0.5f;
        else
            bgrMusic.volume = 0;

            
        //Effects
        if (PlayerPrefs.GetInt("Effects") == 0)
            audioSource.volume = 1;
        else if (PlayerPrefs.GetInt("Effects") == 2)
            audioSource.volume = 1;
        else
            audioSource.volume = 0;
        //audioSource.volume = PlayerPrefs.GetInt("Effects");
    }


    // Update is called once per frame
    void Update()
    {

    }
}
