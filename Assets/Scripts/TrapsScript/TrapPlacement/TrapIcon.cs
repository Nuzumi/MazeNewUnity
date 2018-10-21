using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrapIcon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private float slowedDownTimeScale;
    [SerializeField]
    private FloatEvent changeTimeScale;
    [SerializeField]
    private GameObjectEvent trapIconPressed;
    [SerializeField]
    private NativeEvent trapIconRelesed;
    public GameObject trap;
    public float cooldownTime;
    [SerializeField]
    private Image cooldownTimerImage;

    private bool canBePressed = true;
    private float lastTimeRelesed;
 
    public void OnPointerDown(PointerEventData eventData)
    {
        if (canBePressed)
        {
            trapIconPressed.Invoke(trap);
            changeTimeScale.Invoke(slowedDownTimeScale);
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (canBePressed)
        {
            trapIconRelesed.Invoke();
            canBePressed = false;
            lastTimeRelesed = Time.timeSinceLevelLoad;
            cooldownTimerImage.fillAmount = 1;
            changeTimeScale.Invoke(1);
        }
    }

    private void Update()
    {
        if (!canBePressed)
        {
            if(lastTimeRelesed + cooldownTime <= Time.timeSinceLevelLoad)
                canBePressed = true;
            cooldownTimerImage.fillAmount = 1 - (Time.timeSinceLevelLoad - lastTimeRelesed) / cooldownTime;

        }
    }

}
