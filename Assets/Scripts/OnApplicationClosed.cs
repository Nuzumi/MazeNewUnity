using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnApplicationClosed : MonoBehaviour {

    private static bool created;

    private void Awake()
    {
        if(!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
        }
    }

    private void OnDestroy()
    {
        SaveLoadDataController.SaveData();
    }
}
