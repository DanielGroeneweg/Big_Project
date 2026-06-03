using TMPro;
using UnityEngine;
public class ValueTextPresenter : Presenter
{
    [SerializeField]
    TMP_Text displayText;
    public override void Present(float min, float max, float current)
    {
        current = Mathf.Clamp(current, min, max);
        displayText.text = $"{current} / {max}";
    }
}