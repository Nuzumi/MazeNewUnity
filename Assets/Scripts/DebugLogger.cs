using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugLogger : MonoBehaviour {

    public StringEvent debugEvent;

    private Text debugText;

    private void Awake()
    {
        debugText = transform.GetChild(0).gameObject.GetComponent<Text>();
        SaveLoadDataController.debugEvent = debugEvent;
    }

    private void OnEnable()
    {
        debugEvent.AddListener(Debug);
    }

    private void OnDisable()
    {
        debugEvent.RemoveListener(Debug);
    }

    private void Debug(string log)
    {
        debugText.text += "\n" + log;
    }


}
