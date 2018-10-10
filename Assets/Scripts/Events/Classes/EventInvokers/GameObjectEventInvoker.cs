using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectEventInvoker : MonoBehaviour {

    [SerializeField]
    private GameObjectEvent eventToInvoke;

    [SerializeField]
    private GameObject eventParameter;

    public void Invoke()
    {
        eventToInvoke.Invoke(eventParameter);
    }
}
