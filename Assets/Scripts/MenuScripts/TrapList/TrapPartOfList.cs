using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapPartOfList : MonoBehaviour {

    [SerializeField]
    private Image trapImage;
    [SerializeField]
    private Text trapCostText;
    [SerializeField]
    private Text trapName;

    public void SetTrapParts(TrapInfo trapInfo)
    {
        trapImage.sprite = trapInfo.trapIconSprite;
        trapCostText.text = trapInfo.cost.ToString();
        trapName.text = trapInfo.trapName;
    }

}
