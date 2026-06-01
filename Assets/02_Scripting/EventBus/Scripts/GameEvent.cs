using System;
using UnityEngine;
public abstract class GameEvent<T> : ScriptableObject
    where T : GameEventData
{
    private event Action<T> _Event;
    public void Raise(T data)
    {
        _Event?.Invoke(data);
    }
    public void Register(Action<T> listener)
    {
        _Event += listener;
    }
    public void Unregister(Action<T> listener)
    {
        _Event -= listener;
    }
}