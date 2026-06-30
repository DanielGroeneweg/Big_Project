using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
/// <summary>
/// This class serves as a data container for the enemy's states in the finite state machine (FSM).
/// </summary>
[DefaultExecutionOrder(100)]
public abstract class StatesData : MonoBehaviour
{
    public Animator animator;
   
}
