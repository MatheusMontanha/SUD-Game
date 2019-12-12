using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public FloatValue(float initialValue)
    {
        this.initialValue = initialValue;
        this.value = initialValue;
    }
    public float initialValue;
    [HideInInspector] public float value;
    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        value = initialValue;
    }
}
