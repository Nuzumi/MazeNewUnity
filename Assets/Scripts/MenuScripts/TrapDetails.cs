using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TrapDetails : MonoBehaviour {

    [SerializeField]
    private GameObject trapDetailPanel;
    [SerializeField]
    private GameObject componentPrefab;
    [SerializeField]
    private GameObject componentPanel;
    [SerializeField]
    private Image trapImage;
    [SerializeField]
    private Text trapName;
    [SerializeField]
    private Text trapCost;
    [SerializeField]
    private Text activateText;
    [SerializeField]
    private Text cooldownTime;
    [SerializeField]
    private TrapInfoEvent trapPressed;
    
    private GameObject trapIcon;
    private TrapIcon trapIconClass;
    private BaseTrapActivator trapActivator;
    private List<BaseTrapComponent> trapsComponents;

    private void OnEnable()
    {
        trapPressed.AddListener(SetTrapInfo);
    }

    private void OnDisable()
    {
        trapPressed.RemoveListener(SetTrapInfo);
    }

    private void SetTrapInfo(TrapInfo info)
    {
        ClearTrapComponentsPanel();
        trapDetailPanel.SetActive(true);
        trapIcon = info.trapIconObject;
        trapIconClass = trapIcon.GetComponent<TrapIcon>();
        trapsComponents = trapIcon.GetComponent<TrapIcon>().trap.GetComponent<BaseTrap>().trapComponents;
        trapActivator = trapIcon.GetComponent<TrapIcon>().trap.transform.GetChild(0).GetComponent<BaseTrapActivator>();

        trapImage.sprite = info.trapIconSprite;
        trapName.text = info.trapName;
        trapCost.text = info.cost.ToString();
        cooldownTime.text = "cooldown: " + trapIconClass.cooldownTime;

        componentPanel.GetComponent<RectTransform>().sizeDelta =
            new Vector2(componentPanel.GetComponent<RectTransform>().sizeDelta.x, 80 * trapsComponents.Count);
        foreach(var c in trapsComponents)
        {
            var instance = Instantiate(componentPrefab, componentPanel.transform);
            instance.GetComponent<TrapComponentListElement>().SetComponentValues(c);
        }

        SetActivationText();
    }

    private void ClearTrapComponentsPanel()
    {
        int childCount = componentPanel.transform.childCount;
        for(int i = childCount -1;i> -1; i--)
        {
            Destroy(componentPanel.transform.GetChild(i).gameObject);
        }


    }

    private void SetActivationText()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Trap will activate ");
        if(trapActivator.timeActivation != -1 && !trapActivator.triggerActivation)
        {
            stringBuilder.Append("after ");
            stringBuilder.Append(trapActivator.timeActivation);
            stringBuilder.Append("s.");
        }
        else
        {
            stringBuilder.Append("when enemy steps on it.");
        }
        stringBuilder.AppendLine();

        if (trapActivator.activateTrapTimerAfterTriggerEnter)
        {
            stringBuilder.Append("Trap effects will occure ");
            stringBuilder.Append(trapActivator.timesToActivateTrap);
            stringBuilder.Append(" times");
            stringBuilder.AppendLine();
            stringBuilder.Append("Each one after ");
            stringBuilder.Append(trapActivator.timeActivation);
            stringBuilder.Append("s.");
        }
        else
        {
            if (trapActivator.activateTrapComponentsAfterFirstTimer)
            {

            }
            else
            {
                stringBuilder.Append("Trap effects will occure once");
            }
        }

        stringBuilder.AppendLine();
        stringBuilder.Append("Trap effects:");

        activateText.text = stringBuilder.ToString();
    }






}
