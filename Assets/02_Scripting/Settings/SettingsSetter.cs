using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class SettingsSetter : MonoBehaviour
{
    [SerializeField] Settings settings;
    [SerializeField] AudioMixer mixer;
    [SerializeField] PlayerController playerController;
    [SerializeField] Volume volume;
    [SerializeField] LanguageSetter languageSetter;
    [SerializeField] float brightnessMax = -5;
    [SerializeField] float brightnessMin = 2;
    Exposure exposure;
    private void Start()
    {
        if (volume != null)
        {
            foreach (VolumeComponent comp in volume.profile.components)
            {
                if (comp is Exposure exp)
                {
                    exposure = exp;
                }
            }
        }

        if (settings != null) LoadSettings();
    }
    public void LoadSettings()
    {
        // Make the brightness go so that 0 in settings means brightness min (2) in volume, while 100 in settings means brightness max (-5) in volume
        float brightness = brightnessMin + settings.brightness * ((brightnessMax - brightnessMin) / 100);
        if (exposure != null)
        {
            exposure.fixedExposure.Override(brightness);
        }

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
    }
}