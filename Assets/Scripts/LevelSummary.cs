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

    private float timeSinceLevelStarted;
    private int newStarsCount;
    private int enemiesKilledCount;

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
        levelTime.text = timeSinceLevelStarted.ToString();
    }

    private void OnEnemieKilld(int enemyMaxHP)
    {
        enemiesKilledCount++;
        newStarsCount += enemyMaxHP;
    }
}
