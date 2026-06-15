using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class FlashPresenter : Presenter
{
    [SerializeField] Image image;
    [SerializeField] float flashTime;
    public override void Present(float min, float max, float current)
    {
        StopAllCoroutines();
        image.enabled = true;
        StartCoroutine(Disable());
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(flashTime);
        image.enabled = false;
    }
}