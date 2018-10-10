using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EnemyController : MonoBehaviour {

    /*
    public List<EnemyTypes> enemyTypes;
    public List<GameObject> enemyPrefabs;
    public enum EnemyTypes {pentagon };

    public List<GameObject> DisabledEnemyList { get; set; }

    private void Start()
    {
        EnableList();
    }

    private void EnableList()
    {
        DisabledEnemyList = new List<GameObject>();
        var tmp = Enum.GetValues(typeof(EnemyTypes));
        foreach (var a in tmp)
        {
            DisabledEnemyList.Add(new Tuple<int, List<GameObject>>((int)a, new List<GameObject>()));
        }
    }

    public GameObject SpownEnemy(EnemyTypes type,Vector2 position)
    {
        var enemyList = D
            isabledEnemyList.Where(a => a.Item1 == (int)type).FirstOrDefault().Item2;
        if(enemyList.Count != 0)
        {
            GameObject enemy = enemyList.Take(1).FirstOrDefault();
            enemy.transform.position = position;
            enemy.SetActive(true);
            return enemy;
        }
        else
        {
            return Instantiate(enemyPrefabs[(int)type], position, Quaternion.identity);
        }
    }
    */
}
