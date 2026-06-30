using UnityEngine;

public class FistAttackRight : State
{


    public FistAttackRight(StatesData data)
    {
        this.data = data;
    }

    public bool IsAttackOver()
    {
        return true;
    }
    
}
