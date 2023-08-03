using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class EnvironmentReadWriteData : MonoBehaviour
{
    private string objectDataPath;
    private string fileName;
    public class EnvironmentCaptionData {
        public string name = "";
        public string description = "";
        public string[] lookAroundText = {"A","B","C"};
        public string[] talkToPeopleText = { "1","2","3" };
    }
    public EnvironmentCaptionData environmentData;
    public string saveName;

    private void Awake()
    {

        fileName = gameObject.name + "EnvironmentDataPath.json";
        CreateObjectDataDir();
        try
        {
            LoadObjectData();
        }
        catch(Exception)
        {
            CreateObjectData();
            Debug.LogError("Data Not Found");
            Debug.LogError("Creating Data");
            LoadObjectData();
        }
    }
    private void LoadObjectData()
    {
        using StreamReader reader = new StreamReader(objectDataPath + fileName);
        string json = reader.ReadToEnd();
        EnvironmentCaptionData data = JsonUtility.FromJson<EnvironmentCaptionData>(json);
        environmentData = data;
    }
    private void CreateObjectData()
    {
        EnvironmentCaptionData data = new EnvironmentCaptionData();
        Debug.Log("Saving Data at " + objectDataPath + fileName);
        string json = JsonUtility.ToJson(data);
        using StreamWriter writer = new StreamWriter(objectDataPath + fileName);
        writer.Write(json);
    }

    void CreateObjectDataDir()
    {
        var sceneName = SceneManager.GetActiveScene().name;
       //var path = Application.dataPath + " /StreamingAssets/"+"ObjectData/" + sceneName + "/";
        var path = Application.streamingAssetsPath+"ObjectData/" + sceneName + "/";
        objectDataPath = path;
        Debug.Log(path);
        if (!Directory.Exists(objectDataPath))
        {
            Directory.CreateDirectory(objectDataPath);
        }
    }
}
