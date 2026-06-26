using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class SettingsSetter : MonoBehaviour
{
    [SerializeField] Settings settings;
    [SerializeField] AudioMixer mixer;
    [SerializeField] PlayerController playerController;
    [SerializeField] LanguageSetter languageSetter;
    [SerializeField] float brightnessMax = 1;
    [SerializeField] float brightnessMin = 0;
    [SerializeField] Material uiMaterial;
    private void Start()
    {
        if (settings != null) LoadSettings();
    }
    public void LoadSettings()
    {
        float brightness = brightnessMin + settings.brightness * ((brightnessMax - brightnessMin) / 100);
        Brightness.BrightnessSettings.brightness = brightness;

        if (mixer != null)
        {
            mixer.SetFloat("Volume", Mathf.Log10(settings.volume / 100) * 20);
        }

        if (playerController != null)
        {
            playerController.SetSensitivity(settings.mouseSensitivity / 100f);
        }

        if (languageSetter != null)
        {
            languageSetter.SetLanguage(settings.language);
        }

        if (uiMaterial != null)
        {
            Color col = new Color(1, 1, 1, uiMaterial.color.a);
            col.r *= brightness;
            col.g *= brightness;
            col.b *= brightness;
            uiMaterial.color = col;
        }
    }
}