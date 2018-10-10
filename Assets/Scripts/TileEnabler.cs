using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEnabler : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "tile")
        {
            if (!collision.gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
