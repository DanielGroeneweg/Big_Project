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
        image.fillAmount = fillAmountMax * percentageHealth;
    }
    private void Update()
    {
        if (canvas != null)
        {
            canvas.transform.LookAt(target);
        }
    }
}