using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGroup : MonoBehaviour {

    [SerializeField]
    private List<GameObject> enemiesInGroup;

    public void SetEnemiesTile(Node node)
    {
        foreach(var e in enemiesInGroup)
        {
            e.GetComponent<ObjectTilePosition>().SetTile(node);
        }
    }

    public void SetEnemiesTrigger(Collider2D collision)
    {
        foreach(var e in enemiesInGroup)
        {
            e.GetComponent<EnemyMovement>().SimulateOnTriggerEnter(collision);
        }
    }
}
