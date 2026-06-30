using UnityEngine;
[CreateAssetMenu(menuName = "EventBus/PlayerAttackEventData")]
public class PlayerAttackEventData : GameEventData
{
    
}
public class PlayerAttackEvent : GameEvent<PlayerAttackEventData> { }
