using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;
    [SerializeField] private GameObject[] effects = new GameObject[3];

    private void Awake()
    {
        if (Instance == null) Instance = this; else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEffects(GameObject obj)
    {
        if (obj == null) return;
        var effect = Instantiate(effects[GetEffectDestroy(obj)], obj.transform.position, Quaternion.Euler(90, 0, 0));
        Destroy(effect, 3f);
    }

    private int GetEffectDestroy(GameObject obj)
    {
        var effect = 0;
        switch (obj.tag)
        {
            case "tag1":
                effect = 0;
                break;
            case "tag3":
                effect = 1;
                break;
            case "tag9":
                effect = 2;
                break;
            default:
                break;
        }
        return effect;
    }
}
