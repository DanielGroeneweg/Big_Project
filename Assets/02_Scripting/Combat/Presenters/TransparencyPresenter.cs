using UnityEngine;
using UnityEngine.UI;

public class TransparencyPresenter : Presenter
{
    [SerializeField] Image image;
    public override void Present(float min, float max, float current)
    {
        Color col = image.color;
        col.a = (max - current) / max;
        image.color = col;
    }
}
