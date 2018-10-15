using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowPartOfList : MonoBehaviour {

    [SerializeField]
    private Text costText;

    public void SetCostText(string text){
        costText.text = text;
    }
}
