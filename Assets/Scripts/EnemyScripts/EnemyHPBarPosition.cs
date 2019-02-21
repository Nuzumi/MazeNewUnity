using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBarPosition : MonoBehaviour {

    private GameObject enemy;
    private Vector3 vectorToEnemy;


    private void Start()
    {
        enemy = transform.parent.gameObject;
        transform.SetParent(null);
        vectorToEnemy = new Vector3(0, 0.3f, 0); //transform.position - enemy.transform.position; 
    }

    private void Update()
    {
        transform.position = enemy.transform.position + vectorToEnemy;
        if (!enemy.activeSelf)
            gameObject.SetActive(false);
    }
}
