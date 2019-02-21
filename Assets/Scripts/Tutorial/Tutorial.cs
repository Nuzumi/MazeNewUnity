using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject stepZeroPanel;
    [SerializeField]
    private GameObject enemyWarningPanel;
    
    private bool showEnemyWarning = true;

    private void Start()
    {
        StartCoroutine("DelayedStart");
    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForEndOfFrame();
        player.SetActive(true);
        ShowStep(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowStep(1);
        }
    }

    public void SetPlayerName(Text playerName)
    {
        SaveLoadDataController.LoadedData.playerName = playerName.text;
    }

    public void ShowStep(int stepNumber)
    {
        switch (stepNumber)
        {
            case 0:
                stepZeroPanel.SetActive(true);
                break;

            case 1:
                if (showEnemyWarning)
                {
                    showEnemyWarning = false;
                    enemyWarningPanel.SetActive(true);
                }
                break;
        }
    }

    public void SkipTutorial()
    {
        SaveLoadDataController.LoadedData.playerLevel++;
        SceneManager.LoadScene("Menu");
    }


}
