using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Image loadingImg;
    [SerializeField] private string nameScene;
    [SerializeField] private TextMeshProUGUI txtPercentLoading;

    private void Awake()
    {
        if (loadingImg == null) loadingImg = GameObject.Find("FillBar").GetComponent<Image>();
        if (txtPercentLoading == null) txtPercentLoading = GameObject.Find("txtLoading").GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        txtPercentLoading.text = "0%";
        nameScene = PlayerPrefs.GetString("nameScene", "Home");
        StartCoroutine(this.LoadingAsync(nameScene));
    }

    private void OnDisable()
    {
        PlayerPrefs.SetString("nameScene", "Home");
    }

    private IEnumerator LoadingAsync(string nameScene)
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nameScene);
        var currentProgress = 0f;
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            var progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            currentProgress = Mathf.Lerp(currentProgress, progress, Time.deltaTime * 1.5f);
            loadingImg.fillAmount = currentProgress;
            txtPercentLoading.text = Mathf.CeilToInt(currentProgress * 100) + "%";
            yield return null;
            if (currentProgress >= 0.99f)
            {
                asyncOperation.allowSceneActivation = true;
            }
        }
    }


}
