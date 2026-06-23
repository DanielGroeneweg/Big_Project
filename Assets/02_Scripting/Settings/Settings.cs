using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
public class Settings : ScriptableObject
{
    [Range(0, 100)] public float volume;
    [Range(0, 100)] public float brightness;
    [Range(0, 100)] public float mouseSensitivity;
    public Languages language;
    public void ChangeVolume(float v) { volume = Mathf.Clamp(v, 0, 100); }
    public void ChangeBrightness(float v) { brightness = Mathf.Clamp(v, 0, 100); }
    public void ChangeSensitivity(float v) { mouseSensitivity = Mathf.Clamp(v, 0, 100); }
    public void ChangeLanguage(Int32 i) { language = (Languages)i; }
}