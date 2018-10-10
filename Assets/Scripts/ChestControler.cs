using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ChestControler : MonoBehaviour {

    public GameObject winInage;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Helper.distance(transform.position,collision.transform.position) < 1.3f )
        {
            winInage.SetActive(true);
            player.GetComponent<PlayerControler>().CanMove = false;
        }
    }
}
