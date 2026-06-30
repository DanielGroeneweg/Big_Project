using UnityEngine;

public class FistAttackLeft : State
{
 

    public FistAttackLeft(StatesData data)
    {
        this.data = data;
    }
    public bool IsAttackOver()
    {
        return true;
    }

}
