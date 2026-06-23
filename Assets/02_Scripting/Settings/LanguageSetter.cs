using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
public class LanguageSetter : MonoBehaviour
{
    [SerializeField] UnityEvent onSetEnglish;
    [SerializeField] UnityEvent onSetDutch;
    public void SetLanguage(Languages language)
    {
        switch (language)
        {
            case Languages.English:
                onSetEnglish?.Invoke();
                break;
            case Languages.Dutch:
                onSetDutch?.Invoke();
                break;
        }
    }
}