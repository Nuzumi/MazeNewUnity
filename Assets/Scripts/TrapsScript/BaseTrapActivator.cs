﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapActivationStatus {DontDestroyAfter, DestroyAfter};

public class BaseTrapActivator : MonoBehaviour {
    
    public int timesToActivateTrap;
    [SerializeField]
    private IntGameObjectEvent timerResponse;
    [SerializeField]
    private FloatIntGameObjectEvent addTimer;
    public float timeActivation = -1;
    public bool triggerActivation;
    public bool activateTrapComponentsAfterFirstTimer;
    public bool activateTrapTimerAfterTriggerEnter;
    [SerializeField]
    private BaseTrap trapToActivate;

    private int remainTimesToActivateTrap;

    private void OnEnable()
    {
        timerResponse.AddListener(TryActivateTrap);
    }

    private void OnDisable()
    {
        timerResponse.RemoveListener(TryActivateTrap);
    }

    private void Start()
    {
        remainTimesToActivateTrap = timesToActivateTrap;
        if(timeActivation != -1 && !activateTrapTimerAfterTriggerEnter)
        {
            if (activateTrapComponentsAfterFirstTimer)
            {
                addTimer.Invoke(timeActivation,(int) TrapActivationStatus.DontDestroyAfter, gameObject);
            }
            else
            {
                addTimer.Invoke(timeActivation, (int) TrapActivationStatus.DestroyAfter, gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerActivation && collision.CompareTag("Enemy") && !collision.isTrigger)
        {
            if (activateTrapTimerAfterTriggerEnter)
            {
                addTimer.Invoke(0, (int)TrapActivationStatus.DontDestroyAfter, gameObject);
            }
            else
            {
                trapToActivate.ActivateTrap(true);
            }
        }
            
    }

    private void TryActivateTrap(int number, GameObject invokingGameObject)
    {
        if (invokingGameObject == gameObject)
        {
            switch ((TrapActivationStatus)number)
            {
                case TrapActivationStatus.DontDestroyAfter:
                    trapToActivate.ActivateTrap(false);
                    addTimer.Invoke(timeActivation,remainTimesToActivateTrap == 0 ? 1 : 0, gameObject);
                    remainTimesToActivateTrap--;
                    break;

                case TrapActivationStatus.DestroyAfter:
                    trapToActivate.ActivateTrap(true);
                    break;
            }
        }
            
    }
}
