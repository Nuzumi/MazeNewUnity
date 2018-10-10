using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeEventInvoker : MonoBehaviour {

    [SerializeField]
    private NativeEvent eventToInvoke;

    public void Invoke()
    {
        eventToInvoke.Invoke();
    }
}
