using UnityEngine;
using UnityEngine.UI;

public class TransparencyPresenter : Presenter
{
    [SerializeField] Image image;
    [SerializeField] float brightnessMin;
    [SerializeField] float brightnessMax;
    public override void Present(float min, float max, float current)
    {
        Debug.Log($"min: {min}, max: {max}, current: {current}");
        float brightnessDif = brightnessMax - brightnessMin;
        float brightness = brightnessMin + (current/max) * brightnessDif;

        Color col = new Color();
        col.r = brightness;
        col.g = brightness;
        col.b = brightness;
        col.a = (max - current) / max;
        image.color = col;
    }
}
