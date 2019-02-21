using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapInfoController : MonoBehaviour {

    public static List<TrapInfo> trapsInfo;
    public static List<TrapInfo> ActiveTrapsInfo
    {
        get
        {
            return trapsInfo.Where(ti => ti.cost <= SaveLoadDataController.LoadedData.playerPoints).ToList();
        }
    }

    public List<TrapInfo> trapsInfoList;

    private void Awake()
    {
        trapsInfo = trapsInfoList;
    }
}
