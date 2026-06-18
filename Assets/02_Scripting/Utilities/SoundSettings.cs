using System;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
[Serializable] public class SettingsData
{
    public float volume;
}
public class SoundSettings : MonoBehaviour
{
    public float volume;
    public AudioMixer mixer;
    public Slider slider;
    string path;
    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        path = Application.persistentDataPath + "/soundSettings.json";
    }
    private void Start()
    {
        path = Application.persistentDataPath + "/soundSettings.json";
        slider.value = Read();
    }
    public void SetVolume(float volume)
    {
        mixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        Save();
    }
    void Save()
    {
        SettingsData data = new SettingsData() { volume = volume };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }
    float Read()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SettingsData data = JsonUtility.FromJson<SettingsData>(json);
            if (data != null)
            {
                volume = data.volume;
            }
            else
            {
                volume = 100;
            }
        }
        else
        {
            volume = 100;
            SettingsData data = new SettingsData() { volume = volume };
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);
        }
        return volume;
    }
}
