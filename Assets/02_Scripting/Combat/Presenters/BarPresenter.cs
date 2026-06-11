using UnityEngine;
using UnityEngine.UI;
public class BarPresenter : Presenter
{
    [SerializeField] Image image;
    [SerializeField] float fillAmountMax;
    public override void Present(float min, float max, float current)
    {
        float percentageHealth = current / max;
        image.rectTransform.sizeDelta = new Vector2(fillAmountMax * percentageHealth, image.rectTransform.sizeDelta.y);
    }
}