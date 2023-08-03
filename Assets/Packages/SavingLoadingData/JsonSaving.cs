using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;

public class JsonSaving : MonoBehaviour
{
    [System.Serializable]
    public class PlayerSettingsData
    {
        public float sensitivity;
        public float dialogueVolume;
        public float masterVolume;
        public bool fullscreen;
        public Vector2 resolutionSize;
    }
    public PlayerSettingsData settings;
    private string playerSettingsDataPersistentPath = "";
    public bool save = false;
    public bool load = false;
    public AudioSource[] audioSources;
    public AudioSource dialogueAudioSource;

    [SerializeField]
    private Slider masterVolumeToLoad, dialogueVolumeToLoad, sensitivityToLoad;
    [SerializeField]
    private TextMeshProUGUI masterVolumeValue, dialogueValue, sensValue;
    [SerializeField]
    private Toggle fullscreenToggle;
    [SerializeField]
    private Image fullscreenToggleCheckmark;
    private void Awake()
    {
        audioSources = FindObjectsOfType<AudioSource>();
        SetPaths();
        try{
            LoadSettingsData();
        } catch(Exception){
            CreatePlayerSettings();
            SaveSettingsData();
            LoadSettingsData();
        }
    }
    private void Update()
    {
        if (save)
        {
            SaveSettingsData();
            save = false;
        }
        if (load)
        {
            LoadSettingsData();
            load = false;
        }
        LoadSensText();
    }
    private void CreatePlayerSettings()
    {
        settings = new PlayerSettingsData();
        settings.masterVolume = 100;
        settings.dialogueVolume = 100;
        settings.sensitivity = 2;
        settings.fullscreen = true;
        settings.resolutionSize = new Vector2(Screen.currentResolution.width,Screen.currentResolution.height);
    }
    private void SetPaths()
    {
        playerSettingsDataPersistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "PlayerSettingsData.json";
    }
    public void SaveSettingsData()
    {
        string savePath = playerSettingsDataPersistentPath;
        //Save slider data
        SaveSliderData();
        string json = JsonUtility.ToJson(settings);
        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
        writer.Close();
    }
    public void LoadSettingsData()
    {
        using StreamReader reader = new StreamReader(playerSettingsDataPersistentPath);
        string json = reader.ReadToEnd();

        PlayerSettingsData data = JsonUtility.FromJson<PlayerSettingsData>(json);
        settings = data;
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = settings.masterVolume/100;
        }
        dialogueAudioSource.volume = settings.dialogueVolume / 100;
        //slider data is then updated to match settings variable
        LoadSliderData();
        //slidet text is then updated to match slider value
        LoadSensText();

    }
    private void OnApplicationQuit()
    {
       SaveSettingsData();
    }

    public void SaveAndLoad()
    {
        //Settings from slider data is loaded into json
        SaveSettingsData();
        LoadSettingsData();
    }

    public void SaveSliderData()
    {
        settings.sensitivity = sensitivityToLoad.value;
        settings.masterVolume = masterVolumeToLoad.value;
        settings.dialogueVolume = dialogueVolumeToLoad.value;

    }
    public void LoadSliderData()
    {
        sensitivityToLoad.value = settings.sensitivity;
        masterVolumeToLoad.value = settings.masterVolume;
        dialogueVolumeToLoad.value = settings.dialogueVolume;
        fullscreenToggle.isOn = settings.fullscreen;
    }
    public void LoadSensText()
    {
        sensValue.text = sensitivityToLoad.value.ToString();
        masterVolumeValue.text = masterVolumeToLoad.value.ToString();
        dialogueValue.text = dialogueVolumeToLoad.value.ToString();
    }
    public void ToggleFullScreen()
    {
        if (fullscreenToggle.isOn)
        {
            settings.fullscreen = true;
            fullscreenToggleCheckmark.enabled = true;
            Screen.fullScreen = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            settings.fullscreen = false;
            fullscreenToggleCheckmark.enabled = false;
            Screen.fullScreen = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
