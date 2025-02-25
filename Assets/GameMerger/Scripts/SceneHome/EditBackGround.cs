using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditBackGround : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bgr;
    [SerializeField] private GameObject objbgr;
    // Start is called before the first frame update
    void Start()
    {
        var screenHeight = Camera.main.orthographicSize * 2;
        var screenWidth = screenHeight * Screen.width / Screen.height;
        objbgr.transform.localScale = new Vector3(screenWidth / bgr.bounds.size.x + 0.2f, screenHeight / bgr.bounds.size.y + 0.11f, 1);
    }
    
}
