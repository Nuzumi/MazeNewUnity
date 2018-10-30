using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ChestControler : MonoBehaviour {

    [SerializeField]
    private NativeEvent levelPassed;
    [SerializeField]
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Helper.distance(transform.position,collision.transform.position) < 1.3f )
        {
            levelPassed.Invoke();
            player.GetComponent<PlayerControler>().CanMove = false;
            SaveLoadDataController.LoadedData.playerLevel++;
            SaveLoadDataController.SaveData();
        }
    }
}
