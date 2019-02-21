using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSummary : MonoBehaviour {

    [SerializeField]
    private Text levelTime;
    [SerializeField]
    private Text enemiesKilled;
    [SerializeField]
    private Text starCollected;
    [SerializeField]
    private IntEvent onEnemiesKilld;
    [SerializeField]
    private NativeEvent onLevelPassed;
    [SerializeField]
    private GameObject EndGamePanel;

    private float timeSinceLevelStarted;
    private int newStarsCount;
    private int enemiesKilledCount= -1;

    private void OnEnable()
    {
        onEnemiesKilld.AddListener(OnEnemieKilld);
        onLevelPassed.AddListener(OnLevelPassed);
    }

    private void OnDisable()
    {
        onEnemiesKilld.RemoveListener(OnEnemieKilld);
        onLevelPassed.RemoveListener(OnLevelPassed);
    }

    private void OnLevelPassed()
    {
        timeSinceLevelStarted = Time.timeSinceLevelLoad;
        enemiesKilled.text = enemiesKilledCount.ToString();
        starCollected.text = "+ " + newStarsCount;
        int levelSecounds = ((int)timeSinceLevelStarted);
        levelTime.text = (levelSecounds / 60).ToString() + " : " + (levelSecounds % 60 > 9 ? (levelSecounds % 60).ToString() : "0" + levelSecounds % 60) + " min";
        if (SaveLoadDataController.LoadedData.playerLevel == 16)
            EndGamePanel.SetActive(true);
    }

    private void OnEnemieKilld(int enemyMaxHP)
    {
        enemiesKilledCount++;
        newStarsCount += enemyMaxHP;
    }
}
