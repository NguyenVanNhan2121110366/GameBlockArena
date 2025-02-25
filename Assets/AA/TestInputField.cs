using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestInputField : MonoBehaviour
{
    public List<String> userNames;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Image testImg;
    [SerializeField] private float currentProgress;
    [SerializeField] private Button bntClick;
    [SerializeField] private GameObject anima;

    private void Awake()
    {
        //if (inputField == null) inputField = GameObject.Find("InputField").GetComponent<InputField>();
        bntClick.onClick.AddListener(Clicked);
    }
    // Start is called before the first frame update
    void Start()
    {

        //StartCoroutine(LoadAsyncGame());
    }

    private void Clicked()
    {
        Debug.Log("you was clicked");
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            anima.GetComponent<Animator>().SetTrigger("Destroy");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            anima.GetComponent<Animator>().SetTrigger("Spawn");
        }
    }
    public void Test()
    {
        Debug.Log("CC");
    }

    private void UpdateValueInputField()
    {
        var value = inputField.text;
        if (!userNames.Contains(value))
        {
            userNames.Add(value);
        }
        else
        {
            Debug.Log("Ten nay da ton tai");
        }
        Debug.Log(value);
    }


    public IEnumerator LoadAsyncGame()
    {
        // Tải scene bất đồng bộ
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Home");

        // Ngừng tự động chuyển scene
        asyncOperation.allowSceneActivation = false;

        currentProgress = 0f;

        // Tiến trình tải (cho đến khi tiến độ đạt 90%)
        while (!asyncOperation.isDone)
        {
            // Cập nhật tiến trình
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            currentProgress = Mathf.Lerp(currentProgress, progress, Time.deltaTime * 3f);
            testImg.fillAmount = currentProgress;
            yield return null;
            if (currentProgress >= 0.99)
            {
                //testImg.fillAmount = 1f;
                asyncOperation.allowSceneActivation = true;
                Debug.Log("Vao day");
            }
        }
        yield return new WaitForSeconds(1f);
        //asyncOperation.allowSceneActivation = true;
    }

}
