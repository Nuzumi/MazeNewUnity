using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject stepZeroPanel;
    [SerializeField]
    private GameObject enemyWarningPanel;

    private PlayerControler playerControler;
    private bool showEnemyWarning = true;

    private void Start()
    {
        StartCoroutine("DelayedStart");
    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForEndOfFrame();
        player.SetActive(true);
        playerControler = player.GetComponent<PlayerControler>();
        ShowStep(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowStep(1);
        }
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


}
