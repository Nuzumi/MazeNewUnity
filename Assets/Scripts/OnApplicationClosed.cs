using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnApplicationClosed : MonoBehaviour {

    private void OnDestroy()
    {
        SaveLoadDataController.SaveData();
    }
}
