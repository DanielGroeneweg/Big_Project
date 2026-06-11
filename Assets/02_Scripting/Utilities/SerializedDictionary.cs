using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializedDictionary<TKey, TValue>
{
    [SerializeField]
    DictionaryItem<TKey, TValue>[] Items;
    public Dictionary<TKey, TValue> ToDictionary()
    {
        Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
        foreach (var item in Items)
        {
            dict.Add(item.Key, item.Value);
        }
        return dict;
    }
}

[Serializable]
public class DictionaryItem<TKey, TValue>
{
    [SerializeField]
    public TKey Key;
    [SerializeField]
    public TValue Value;

    public DictionaryItem(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}
