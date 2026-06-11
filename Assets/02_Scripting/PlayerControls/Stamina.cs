using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(100)]
public class Stamina : MonoBehaviour
{
    [SerializeField] float maxStamina;
    [Tooltip("The amount of stamina recovered each instance of stamina recovery")]
    [SerializeField] [Min(0)] float staminaRecoverAmount;
    [Tooltip("The amount of seconds between each instance of stamina recovery")]
    [SerializeField] [Min(0)] float staminaRecoverSpeed;
    [Tooltip("Whether or not stamina can be recovered while using stamina")]
    [SerializeField] bool recoverStaminaWhileUsing;
    [SerializedDictionary("Player Action", "Stamina Usage")]
    [SerializeField] SerializedDictionary<playerActions, float> actionStaminaDictionary;
    public float _Stamina { get; private set; }
    public Action<StaminaChangeData> staminaChangeEvent;
    public Dictionary<playerActions, float> ActionStaminaDictionary => actionStaminaDictionary;
    private void Start()
    {
        _Stamina = maxStamina;
        StartCoroutine(RecoverStamina());
    }
    public void UseStamina(float stamina)
    {
        _Stamina = Mathf.Clamp(_Stamina - Mathf.Abs(stamina), 0, maxStamina);
        staminaChangeEvent?.Invoke(new StaminaChangeData() { currentStamina = _Stamina, maxStamina = maxStamina });
        if (!recoverStaminaWhileUsing)
        {
            StopAllCoroutines();
            StartCoroutine(RecoverStamina());
        }
    }
    IEnumerator RecoverStamina()
    {
        while (true)
        {
            yield return new WaitForSeconds(staminaRecoverSpeed);
            _Stamina = Mathf.Clamp(_Stamina + staminaRecoverAmount, 0, maxStamina);
            staminaChangeEvent?.Invoke(new StaminaChangeData() { currentStamina = _Stamina, maxStamina = maxStamina });
        }
    }
#if UNITY_EDITOR
    /// <summary>
    /// Makes sure the array has all actions from the enum and has those only once (no duplicates)
    /// </summary>
    private void OnValidate()
    {
        if (actionStaminaDictionary == null) actionStaminaDictionary = new SerializedDictionary<playerActions, float>();

        Dictionary<playerActions, float> actions = new();

        // Loop through everything in the list, removing duplicates
        foreach (playerActions playerAction in ActionStaminaDictionary.Keys)
        {
            if (!actions.ContainsKey(playerAction))
            {
                actions.Add(playerAction, actionStaminaDictionary[playerAction]);
                if (actions[playerAction] < 0) actions[playerAction] = 0;
            }
        }

        // Loop through all playerActions, checking if they are present
        foreach (playerActions playerAction in Enum.GetValues(typeof(playerActions)))
        {
            if (!actions.ContainsKey(playerAction))
            {
                actions.Add(playerAction, 0);
            }
        }

        actionStaminaDictionary = new SerializedDictionary<playerActions, float>(actions);
    }
#endif
}