using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NativeEventReceiver : MonoBehaviour {

    [SerializeField]
    private NativeEvent eventToListenFor;

    [SerializeField]
    private UnityEvent unityEvent;

    private void OnEnable()
    {
        eventToListenFor.AddListener(InvokeEvents);
    }

    private void OnDisable()
    {
        eventToListenFor.RemoveListener(InvokeEvents);
    }

    private void InvokeEvents()
    {
        unityEvent.Invoke();
    }
}
