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
    private int oneSegmentWidth;

    private void Start()
    {
        int playerPoints = SaveLoadDataController.LoadedData.playerPoints;

        for(int i = 0; i < trapsList.Count; i++)
        {
            var trapPart = Instantiate(trapPartOfList, panel.transform);
            trapPart.GetComponent<TrapPartOfList>().SetTrapParts(trapsList[i]);
            if(i != trapsList.Count - 1)
            {
                var arrow = Instantiate(arrowPartOfList, panel.transform);
                arrow.GetComponent<ArrowPartOfList>().SetCostText(playerPoints + "/" + trapsList[i].cost);
            }
        }

        var rect = panel.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(oneSegmentWidth * trapsList.Count, rect.sizeDelta.y);

        
    }

}
