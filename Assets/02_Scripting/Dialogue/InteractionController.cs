using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class InteractionController : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] float proximityRange;
    [SerializeField] float holdingTime;
    [SerializeField] bool repeatable;
    [SerializeField] UnityEvent onInteract;
    bool holding = false;

    #region Track closest interaction
    static List<InteractionController> controllersInRange = new();
    static InteractionController closestController;
    static int lastUpdatedFrame = -1;
    static void RefreshClosest()
    {
        if (Time.frameCount == lastUpdatedFrame) return;
        lastUpdatedFrame = Time.frameCount;

        InteractionController closest = null;
        float minDist = float.MaxValue;
        foreach (var controller in controllersInRange)
        {
            float dist = Vector3.Distance(
                PlayerController.instance.transform.position,
                controller.transform.position
            );
            if (dist < minDist) { minDist = dist; closest = controller; }
        }
        closestController = closest;
    }
    void RemoveFromRange()
    {
        if (controllersInRange.Contains(this))
        {
            controllersInRange.Remove(this);
            slider.value = 0;
            holding = false;
            slider.gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        controllersInRange.Remove(this);
    }
    #endregion
    private void Update()
    {
        RefreshClosest();

        float playerDist = Vector3.Distance(
            PlayerController.instance.transform.position,
            transform.position
        );

        if (playerDist <= proximityRange)
        {
            if (!controllersInRange.Contains(this))
                controllersInRange.Add(this);
        }
        else
        {
            RemoveFromRange();
        }

        // Only show slider on the closest drop
        bool isClosest = closestController == this;
        if (isClosest)
        {
            if (!slider.gameObject.activeSelf) slider.gameObject.SetActive(true);
        }
        else
        {
            if (slider.gameObject.activeSelf)
            {
                slider.value = 0;
                holding = false;
                slider.gameObject.SetActive(false);
            }
        }
    }
    public void OnE(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && closestController == this)
        {
            holding = true;
            StartCoroutine(Holding());
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            holding = false;
            slider.value = 0;
        }
    }
    IEnumerator Holding()
    {
        float timePassed = 0;
        while (holding)
        {
            yield return null;
            timePassed += Time.deltaTime;
            slider.value = timePassed / holdingTime;

            if (timePassed >= holdingTime)
            {
                onInteract?.Invoke();
                if (repeatable) Destroy(gameObject);
                break;
            }
        }
        holding = false;
        slider.value = 0;
    }
}