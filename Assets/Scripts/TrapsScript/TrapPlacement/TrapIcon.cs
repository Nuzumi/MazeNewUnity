using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrapIcon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private GameObjectEvent trapIconPressed;
    [SerializeField]
    private NativeEvent trapIconRelesed;
    [SerializeField]
    private GameObject trap;
 
    public void OnPointerDown(PointerEventData eventData)
    {
        trapIconPressed.Invoke(trap);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        trapIconRelesed.Invoke();
    }
}
