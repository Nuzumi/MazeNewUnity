using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrapPlacement : MonoBehaviour {

    [SerializeField]
    private Vector3 trapDistanceVectorFromPlayer;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float slowedDownTimeScale;
    [SerializeField]
    private FloatEvent changeTimeScale;
    [SerializeField]
    private BoolEvent changePlayerMoveState;
    [SerializeField]
    private GameObjectEvent trapIconPressed;
    [SerializeField]
    private NativeEvent trapIconRelesed;
    [SerializeField]
    private GameObject joystickStick;

    private bool trapPlacementActive;
    private Vector3 joystickStartPosition;
    private GameObject trapInstance;

    private void Start()
    {
        joystickStartPosition = joystickStick.transform.position;
    }

    private void OnEnable()
    {
        trapIconPressed.AddListener(OnTrapIconPressed);
        trapIconRelesed.AddListener(OnTrapIconRelesed);
    }

    private void OnDisable()
    {
        trapIconPressed.RemoveListener(OnTrapIconPressed);
        trapIconRelesed.RemoveListener(OnTrapIconRelesed);
    }

    private void Update()
    {
        if (trapPlacementActive)
        {
            trapInstance.transform.position = player.transform.position + (joystickStick.transform.position - joystickStartPosition).normalized * trapDistanceVectorFromPlayer.magnitude;
        }
    }

    public void OnTrapIconPressed(GameObject trap)
    {
        changeTimeScale.Invoke(slowedDownTimeScale);
        changePlayerMoveState.Invoke(false);
        trapPlacementActive = true;
        trapInstance = Instantiate(trap, player.transform.position + trapDistanceVectorFromPlayer, Quaternion.identity);
    }



    public void OnTrapIconRelesed()
    {
        changeTimeScale.Invoke(1);
        changePlayerMoveState.Invoke(true);
        trapPlacementActive = false;
    }

    
}
