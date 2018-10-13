using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapProgressLine : MonoBehaviour {

    [SerializeField]
    private List<TrapInfo> trapsList;
    [SerializeField]
    private GameObject trapPartOfList;
    [SerializeField]
    private GameObject arrowPartOfList;
    [SerializeField]
    private GameObject panel;
    [SerializeField]


    private void Start()
    {
        for(int i = 0; i < trapsList.Count; i++)
        {
            var trapPart = Instantiate(trapPartOfList, panel.transform);
            trapPart.GetComponent<TrapPartOfList>().SetTrapParts(trapsList[i]);
            Instantiate(arrowPartOfList, panel.transform);
        }
    }

}
