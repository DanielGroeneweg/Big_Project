using UnityEngine;

public class blurPresenter : Presenter
{
    [SerializeField] int minStrength;
    [SerializeField] int maxStrength;
    public override void Present(float min, float max, float current)
    {
        float strengthDif = maxStrength - minStrength;
        float strength = maxStrength - (current / max) * strengthDif;

        SeparableBlurPass.BlurSettings.strength = (int)strength;
    }
}