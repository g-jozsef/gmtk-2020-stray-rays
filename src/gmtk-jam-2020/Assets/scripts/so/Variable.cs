using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Variable<T> : ScriptableObject
{
    public T Value;
    public T DefaultValue;


    private void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        Value = DefaultValue;
    }
}
