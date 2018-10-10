using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public IntGameObjectEvent timerResponse;
    public FloatIntGameObjectEvent setTimer;

    private List<Tuple<float,int,GameObject>> timeGameObjectList = new List<Tuple<float,int,GameObject>>();

    private void OnEnable()
    {
        setTimer.AddListener(AddTimer);
    }

    private void OnDisable()
    {
        setTimer.RemoveListener(AddTimer);
    }

    private void Update()
    {
        for(int i =0; i< timeGameObjectList.Count; i++)
        {
            if(Time.timeSinceLevelLoad > timeGameObjectList[i].Item1)
            {
                timerResponse.Invoke(timeGameObjectList[i].Item2, timeGameObjectList[i].Item3);
                timeGameObjectList.RemoveAt(i);
                i--;
            }
        }
    }

    public void AddTimer(float tMinusResponse, int timerNumber, GameObject gameObject)
    {
        timeGameObjectList.Add(new Tuple<float, int, GameObject>(tMinusResponse + Time.timeSinceLevelLoad, timerNumber, gameObject));
    }
}
