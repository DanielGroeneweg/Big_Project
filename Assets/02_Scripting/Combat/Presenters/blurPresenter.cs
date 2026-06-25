using UnityEngine;

public class blurPresenter : Presenter
{
    [SerializeField] int minStrength;
    [SerializeField] int maxStrength;
    float currentStam = -1;
    public override void Present(float min, float max, float current)
    {
        if (current == currentStam) return;
        currentStam = current;

        float strengthDif = maxStrength - minStrength;
        float strength = maxStrength - (current / max) * strengthDif;

        SeparableBlurPass.BlurSettings.strength = (int)strength;
        SeparableBlurPass.BlurSettings.FillWeights();
    }
}