using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SettingsPresenter : MonoBehaviour
{
    [SerializeField] Settings settings;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider sensitivitySlider;
    [SerializeField] Slider brightnessSlider;
    [SerializeField] TMP_Dropdown dropdown;
    private void OnEnable()
    {
        volumeSlider.value = settings.volume;

        sensitivitySlider.value = settings.mouseSensitivity;

        brightnessSlider.value = settings.brightness;

        dropdown.value = (int)settings.language;
    }
}