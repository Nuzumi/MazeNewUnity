﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour {

    [SerializeField]
    private Image hpBar;
    [SerializeField]
    private Text hpNumber;

    private int maxHealthPoint;
    public int MaxHealthPoint
    {
        get { return maxHealthPoint; }
        set
        {
            maxHealthPoint = value;
            ActualHealthPoint = value;
        }
    }
    public float ActualHealthPoint
    {
        set
        {
            hpBar.fillAmount = value / MaxHealthPoint;
            hpNumber.text = value + "/" + MaxHealthPoint;
        }
    }

}
