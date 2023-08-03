using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class GameSaveData : MonoBehaviour
{
    
    public bool saveNow;
    public string sceneToLoad;
    public class SavedGameData
    {
        public string sceneName;
    }


    void Start()
    {
        //If a save  exists, load that save, however, if not create a new one using the scenes default object data.
        if (Directory.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData/" + SceneManager.GetActiveScene().name))
        {
            Load();
        }
        else
        {
            Save();
            Load();
        }
        

    }

    public void NewGame()
    {
        //Delete previous save game.
        string savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData" + "/";
        Directory.Delete(savePath, true);
        Save();
        Load();
    }
    public void Save()
    {
        string savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData" + "/";
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        if (!Directory.Exists(savePath + SceneManager.GetActiveScene().name))
        {;
            Directory.CreateDirectory(savePath + SceneManager.GetActiveScene().name);
        }
    }
    public void Load()
    {
        string savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData" + "/" + SceneManager.GetActiveScene().name + "/";
        /* Here is an example, using an array of all the objects within the scene, and updating the objects to their previous state
        savePath = savePath + objectsDataInGame[i].transform.gameObject.name + ".json";
        for (int i = 0; i < objectsDataInGame.Length; i++)
        {
            using StreamReader reader = new StreamReader(savePath);
            string json = reader.ReadToEnd();
            InteractedData loadedData = JsonUtility.FromJson<InteractedData>(json);
            objectsDataInGame[i].previouslySelected = loadedData.previouslySelected;
        }
        LoadGliderData();
        */
    }
    public void OnApplicationQuit()
    {
        Save();
    }
    //Template for SaveData
    public void SaveDataTemplate()
    {
        SavedGameData data = new SavedGameData();
        string savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData/" + "Template.json";
        data.sceneName = SceneManager.GetActiveScene().name;
        string json = JsonUtility.ToJson(data);
        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
        writer.Close();
    }
    public void LoadDataTemplate()
    {
        string savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData/" + "Template.json";
        using StreamReader streamReader = new StreamReader(savePath);
        string json = streamReader.ReadToEnd();
        SavedGameData loadedData = JsonUtility.FromJson<SavedGameData>(json);
        sceneToLoad = loadedData.sceneName;
    }
}
