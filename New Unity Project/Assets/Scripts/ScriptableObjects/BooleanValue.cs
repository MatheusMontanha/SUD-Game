using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BooleanValue : ScriptableObject, ISerializationCallbackReceiver
{
    public BooleanValue(bool initialValue)
    {
        this.value = initialValue;
    }
    public bool initialValue;
    [HideInInspector] public bool value;
    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        value = initialValue;
    }
}
