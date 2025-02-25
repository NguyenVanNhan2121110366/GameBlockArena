using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Test2 : MonoBehaviour
{
    [SerializeField] private Button btnTest;
    // Start is called before the first frame update
    void Start()
    {
        btnTest.onClick.AddListener(Load);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Load()
    {
        SceneManager.LoadScene("Test");
    }
}
