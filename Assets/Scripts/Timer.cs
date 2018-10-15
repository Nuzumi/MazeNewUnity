using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public IntGameObjectEvent removeTimer;
    public IntGameObjectEvent timerResponse;
    public FloatIntGameObjectEvent setTimer;

    private List<Tuple<float,int,GameObject>> timeGameObjectList = new List<Tuple<float,int,GameObject>>();

    private void OnEnable()
    {
        setTimer.AddListener(AddTimer);
        removeTimer.AddListener(RemoveTimer);
    }

    private void OnDisable()
    {
        setTimer.RemoveListener(AddTimer);
        removeTimer.RemoveListener(RemoveTimer);
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

    public void RemoveTimer(int timerNumber, GameObject gameObject)
    {
        var tup = timeGameObjectList.Find(t => t.Item2 == timerNumber && t.Item3 == gameObject);
        timeGameObjectList.Remove(tup);
    }
}
