using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SignalListener : MonoBehaviour
{
    public SSignal signal;
    public UnityEvent signalEvent;
    private void OnEnable() {
        signal.RegisterListener(this);
    }
    void OnDisable()
    {
     signal.DeregisterListener(this);   
    }
    public void OnSignalRaised()
    {
        signalEvent.Invoke();
    }

}
