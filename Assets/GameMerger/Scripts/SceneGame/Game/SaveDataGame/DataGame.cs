using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class DataSave
{
    public int[] HightScore = new int[3];
    public int[] CurentScore = new int[3];
    public int[] Diamon = new int[3];
    public bool[] IsCheckDemo = new bool[3];
    public int[] Language = new int[3];
    public int[] Level = new int[3];
    public float[] Volume = new float[3];
    public float[] Pos = new float[3];
}
public class DataGame : MonoBehaviour
{
    public static DataGame Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this; else return;
    }
    public DataSave dataSave;

    public void SaveData()
    {
        var path = Application.persistentDataPath + "/dt.save";
        var binary = new BinaryFormatter();
        var fileStream = File.Open(path, FileMode.OpenOrCreate);
        binary.Serialize(fileStream, dataSave);
        fileStream.Close();
        Debug.Log("Save");
    }

    public void LoadData()
    {
        var path = Application.persistentDataPath + "/dt.save";
        if (File.Exists(path))
        {
            var binary = new BinaryFormatter();
            var fileStream = File.Open(path, FileMode.Open);
            dataSave = (DataSave)binary.Deserialize(fileStream);
            fileStream.Close();
        }
        else
            Debug.Log("Khong co data");
    }

    private void OnEnable()
    {
        this.LoadData();
        Debug.Log("Load Data");
    }

    private void OnDisable()
    {
        this.SaveData();
        Debug.Log("Save Data");
    }
}
