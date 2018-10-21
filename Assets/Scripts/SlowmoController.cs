using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowmoController : MonoBehaviour {

    [SerializeField]
    private FloatEvent changeTimeScale;

    private void OnEnable()
    {
        changeTimeScale.AddListener(ChangeTimeScale);
    }

    private void OnDisable()
    {
        changeTimeScale.RemoveListener(ChangeTimeScale);
    }

    private void ChangeTimeScale(float timeSpeedFactor = 1)
    {
        Time.timeScale = timeSpeedFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
