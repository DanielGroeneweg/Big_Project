using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FadeOutPresenter : Presenter
{
    [SerializeField] Image[] images;
    [SerializeField] float fadeOutDelay;
    [SerializeField] float fadeOutTime;
    public override void Present(float min, float max, float current)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        float alpha = 1;
        Color col;

        foreach (Image image in images)
        {
            col = image.color;
            col.a = alpha;
            image.color = col;
        }

        col.a = alpha;

        yield return new WaitForSeconds(fadeOutDelay);

        while (alpha > 0)
        {
            yield return null;
            alpha -= 1 / fadeOutTime * Time.deltaTime;

            foreach (Image image in images)
            {
                col = image.color;
                col.a = alpha;
                image.color = col;
            }
        }
    }
}
