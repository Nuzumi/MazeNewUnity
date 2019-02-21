using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ChestControler : MonoBehaviour {

    [SerializeField]
    private NativeEvent levelPassed;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private EnemyController enemyController;
    [SerializeField]
    private IntEvent addPoints;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Helper.distance(transform.position,collision.transform.position) < 1.3f )
        {
            audioSource.Play();
            if (enemyController != null)
            {
                addPoints.Invoke(enemyController.enemiesCount);
            }
            SaveLoadDataController.LoadedData.playerLevel++;
            SaveLoadDataController.SaveData();
            levelPassed.Invoke();
            player.GetComponent<PlayerControler>().CanMove = false;
            
        }
    }
}
