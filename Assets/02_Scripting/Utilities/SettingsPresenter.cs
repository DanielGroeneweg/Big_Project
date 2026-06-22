using UnityEngine;
using UnityEngine.UI;

public class SettingsPresenter : MonoBehaviour
{
    [SerializeField] Settings settings;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider sensitivitySlider;
    [SerializeField] Slider brightnessSlider;
    private void OnEnable()
    {
        volumeSlider.value = settings.volume;
        sensitivitySlider.value = settings.mouseSensitivity;
        brightnessSlider.value = settings.brightness;
    }
}