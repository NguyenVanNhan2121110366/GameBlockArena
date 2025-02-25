using UnityEngine.UI;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private Image imageDot;
    [SerializeField] private Sprite[] imageDots = new Sprite[9];
    [SerializeField] private Transform dotSprite;
    private void Awake()
    {
        Instance = this;
        this.imageDot = GameObject.Find("Grid_1").GetComponent<Image>();
        //if (dotSprite == null) this.dotSprite = GameObject.Find("Dots").GetComponent<Transform>(); else Debug.Log("Not null");
    }
    // Start is called before the first frame update
    void Start()
    {
        this.GetDotIntoArray();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetDotIntoArray()
    {
        for (var i = 0; i < this.dotSprite.childCount; i++)
            imageDots[i] = dotSprite.GetChild(i).GetComponent<SpriteRenderer>().sprite;
    }

    public void ImageConversion(int index)
    {
        imageDot.sprite = imageDots[index];
    }


    public int RanDomInDexNumber()
    {
        var randomIndex = Random.Range(0, 100);
        var numberReturn = 0;
        // level 1 -> 5
        if (GameController.Instance.Level < 5)
            numberReturn = 0;
        // level 6 -> 9
        else if (GameController.Instance.Level >= 5 && GameController.Instance.Level < 9)
        {
            if (randomIndex >= 0 && randomIndex <= 60)
                numberReturn = 0;
            else
                numberReturn = 1;
        }
        // level 10 -> 13
        else if (GameController.Instance.Level >= 9 && GameController.Instance.Level < 13)
        {
            if (randomIndex >= 0 && randomIndex <= 40)
                numberReturn = 0;
            else if (randomIndex > 40 && randomIndex <= 80)
                numberReturn = 1;
            else
                numberReturn = 2;
        }
        // level 14 -> 17
        else if (GameController.Instance.Level >= 13 && GameController.Instance.Level < 17)
        {
            if (randomIndex >= 0 && randomIndex <= 30)
                numberReturn = 0;
            else if (randomIndex > 30 && randomIndex <= 70)
                numberReturn = 1;
            else if (randomIndex > 70 && randomIndex <= 90)
                numberReturn = 2;
            else
                numberReturn = 3;
        }
        // Level 18 -> 21
        else if (GameController.Instance.Level >= 17 && GameController.Instance.Level < 21)
        {
            if (randomIndex >= 0 && randomIndex <= 10)
                numberReturn = 0;
            else if (randomIndex > 10 && randomIndex <= 30)
                numberReturn = 1;
            else if (randomIndex > 20 && randomIndex <= 60)
                numberReturn = 2;
            else if (randomIndex > 60 && randomIndex <= 90)
                numberReturn = 3;
            else
                numberReturn = 4;
        }
        // Level 22 -> 25
        else if (GameController.Instance.Level >= 21 && GameController.Instance.Level < 25)
        {
            if (randomIndex >= 0 && randomIndex <= 50)
                numberReturn = 2;
            else if (randomIndex > 50 && randomIndex <= 90)
                numberReturn = 3;
            else
                numberReturn = 4;
        }
        // Level 26 -> 29
        else if (GameController.Instance.Level >= 25 && GameController.Instance.Level < 29)
        {
            if (randomIndex >= 0 && randomIndex <= 30)
                numberReturn = 2;
            else if (randomIndex > 30 && randomIndex <= 60)
                numberReturn = 3;
            else if (randomIndex > 60 && randomIndex <= 90)
                numberReturn = 4;
            else
                numberReturn = 5;
        }

        // Level 30 -> 33
        else if (GameController.Instance.Level >= 29 && GameController.Instance.Level < 33)
        {
            if (randomIndex >= 0 && randomIndex <= 10)
                numberReturn = 2;
            else if (randomIndex > 10 && randomIndex <= 40)
                numberReturn = 3;
            else if (randomIndex > 30 && randomIndex <= 80)
                numberReturn = 4;
            else if (randomIndex > 80 && randomIndex <= 90)
                numberReturn = 5;
            else
                numberReturn = 6;
        }
        return numberReturn;
    }
}
