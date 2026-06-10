using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class StaminaUsage
{
    public playerActions playerAction;
    public float staminaAmount;
}
[DefaultExecutionOrder(100)]
public class Stamina : MonoBehaviour
{
    [SerializeField] float maxStamina;
    [Tooltip("The amount of stamina recovered each instance of stamina recovery")]
    [SerializeField] float staminaRecoverAmount;
    [Tooltip("The amount of seconds between each instance of stamina recovery")]
    [SerializeField] float staminaRecoverSpeed;
    [Tooltip("Whether or not stamina can be recovered while using stamina")]
    [SerializeField] bool recoverStaminaWhileUsing;
    [SerializeField] StaminaUsage[] actionStaminaRequirements;
    public float _Stamina { get; private set; }
    public Action<StaminaChangeData> staminaChangeEvent;
    public StaminaUsage[] ActionStaminaRequirements { get { return actionStaminaRequirements; } }
    private void Start()
    {
        _Stamina = maxStamina;
        StartCoroutine(RecoverStamina());
    }
    public void UseStamina(float stamina)
    {
        _Stamina = Mathf.Clamp(_Stamina - stamina, 0, maxStamina);
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
        if (actionStaminaRequirements == null) actionStaminaRequirements = new StaminaUsage[0];

        HashSet<playerActions> actions = new();
        List<StaminaUsage> allActions = new();

        // Loop through everything in the list, removing duplicates
        for (int i = 0; i < actionStaminaRequirements.Length; i++)
        {
            if (!actions.Contains(actionStaminaRequirements[i].playerAction))
            {
                allActions.Add(actionStaminaRequirements[i]);
                actions.Add(actionStaminaRequirements[i].playerAction);
            }
        }
        actionStaminaRequirements = allActions.ToArray();

        // Loop through all playerActions, checking if they are present
        foreach (playerActions playerAction in Enum.GetValues(typeof(playerActions)))
        {
            if (!actions.Contains(playerAction))
            {
                actionStaminaRequirements = new StaminaUsage[allActions.Count + 1];

                // Duplicate the list into the array
                for (int i = 0; i < allActions.Count; i++)
                    actionStaminaRequirements[i] = allActions[i];

                // Add the missing actions
                actionStaminaRequirements[allActions.Count] = new StaminaUsage() { playerAction = playerAction };
                allActions.Add(actionStaminaRequirements[allActions.Count]);
                actions.Add(playerAction);
            }
        }
    }
#endif
}