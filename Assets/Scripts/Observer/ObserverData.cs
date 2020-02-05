using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ObserverCallback<T>(T value, string key);

public class ObserverData<T> : ScriptableObject
{
    public string Key;
    public bool HasExpectedValue = false;
    public T Value;
    public ObserverCallback<T> Callback;

    public ObserverData(ObserverCallback<T> callback, string key, T value = default(T))
    {
        Callback = callback;
        Key = key;

        Value = value;
        
        HasExpectedValue = true;
    }
}
