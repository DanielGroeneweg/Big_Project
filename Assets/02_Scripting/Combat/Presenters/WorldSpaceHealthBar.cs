using UnityEngine;
using UnityEngine.UI;
public class WorldSpaceHealthBar : Presenter
{
    [SerializeField] Canvas canvas;
    [SerializeField] Transform target;
    [SerializeField] Image image;
    [SerializeField] float fillAmountMax;
    public override void Present(float min, float max, float current)
    {
        float percentageHealth = current / max;
        image.rectTransform.sizeDelta = new Vector2(fillAmountMax * percentageHealth, 0.25f);
    }
    private void Update()
    {
        if (canvas != null)
        {
            canvas.transform.LookAt(target);
        }
    }
}