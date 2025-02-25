using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [SerializeField] private Button bntExitGame;
    [SerializeField] private Button bntStartGame;
    private Button txtStart;

    private void Awake()
    {

        if (txtStart == null) txtStart = GameObject.Find("txtStart").GetComponent<Button>();
        if (bntExitGame == null) bntExitGame = GameObject.Find("bntExitGame").GetComponent<Button>(); else return;
        if (bntStartGame == null) bntStartGame = GameObject.Find("bntStartGame").GetComponent<Button>(); else return;
        bntStartGame.onClick.AddListener(OnStartGame);
        bntExitGame.onClick.AddListener(OnClickExit);
        txtStart.onClick.AddListener(OnStartGame);
    }

    private void OnStartGame()
    {
        UIHomeController.Instance.HandleVibrate();
        PlayerPrefs.SetString("nameScene", "Game");
        SceneManager.LoadScene("Loading");
    }

    private void OnClickExit()
    {
        Application.Quit();
    }
}
