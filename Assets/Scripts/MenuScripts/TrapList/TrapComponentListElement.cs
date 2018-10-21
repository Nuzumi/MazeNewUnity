using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapComponentListElement : MonoBehaviour {

    [SerializeField]
    private Image componentImage;
    [SerializeField]
    private Text componentName;
    [SerializeField]
    private Text componntValue;

    public void SetComponentValues(BaseTrapComponent trapComponent)
    {
        componentImage.sprite = trapComponent.componentImage;
        componentName.text = trapComponent.componentNamage;
        componntValue.text = trapComponent.effectValue.ToString();
    }
}
