﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPoints : MonoBehaviour {

    [SerializeField]
    private IntEvent addPoints;
    [SerializeField]
    private Text coinText;

    private int points;
    private SaveLoadData loadedData;

    private void Start()
    {
        loadedData = SaveLoadDataController.LoadedData;
        points = loadedData.playerPoints;
        coinText.text = points.ToString();
    }

    private void OnEnable()
    {
        addPoints.AddListener(AddPoints);
    }

    private void OnDisable()
    {
        addPoints.RemoveListener(AddPoints);    
    }

    private void AddPoints(int points)
    {
        this.points += points;
        coinText.text = this.points.ToString();
        loadedData.playerPoints += points;
    }
}
