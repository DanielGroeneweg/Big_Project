using System;
using System.IO;
using UnityEngine;
[Serializable] public class SettingsData
{
    public float volume;
    public float sensitivity;
    public float brightness;
}
public class SettingsManager : MonoBehaviour
{
    [SerializeField] Settings settings;
    string path;
    static SettingsManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            path = Application.persistentDataPath + "/soundSettings.json";
            Read();
        }

        else
            Destroy(gameObject);
    }
    void Save()
    {
        SettingsData data = new SettingsData() { volume = settings.volume, brightness = settings.brightness, sensitivity = settings.mouseSensitivity };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }
    void Read()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SettingsData data = JsonUtility.FromJson<SettingsData>(json);

            if (data != null)
            {
                settings.volume = data.volume;
                settings.brightness = data.brightness;
                settings.mouseSensitivity = data.sensitivity;
            }

            else
            {
                settings.volume = 50;
                settings.brightness = 50;
                settings.mouseSensitivity = 50;
            }
        }

        else
        {
            settings.volume = 50;
            settings.brightness = 50;
            settings.mouseSensitivity = 50;

            Save();
        }
    }
    private void OnApplicationQuit()
    {
        Save();
    }
}
