using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EnemyController : MonoBehaviour {

    [SerializeField]
    private List<GameObject> enemiesList;
    [SerializeField]
    private List<int> enemiesOccurance;
    [SerializeField]
    private int enemiesCount;
    [SerializeField]
    private GameObjectListEvent mazeCreated;
    
    private int enemiesOccuranceSum;
    private System.Random rand;

    private void Start()
    {
        rand = new System.Random();
        enemiesOccuranceSum = enemiesOccurance.Sum();
    }

    private void OnEnable()
    {
        mazeCreated.AddListener(SetEnemiesPositions);
    }

    private void OnDisable()
    {
        mazeCreated.RemoveListener(SetEnemiesPositions);
    }

    private void SetEnemiesPositions(List<GameObject> nodes)
    {
        List<GameObject> nodesList = new List<GameObject>(nodes);
        List<Node> nodesWhereEnemiesCanBePlaced = nodesList.Select(go => go.GetComponent<Node>()).Where(n => n.neighboursToGo.Count >= 2).ToList();
        for(int i = 0; i < enemiesCount; i++)
        {
            int randomValue = rand.Next(enemiesOccuranceSum);
            int sum = 0;
            for(int j = 0; j< enemiesList.Count; j++)
            {
                sum += enemiesOccurance[j];
                if(randomValue < sum)
                {
                    var choosenNode = nodesWhereEnemiesCanBePlaced[rand.Next(nodesWhereEnemiesCanBePlaced.Count)];
                    SetEnemiePosition(enemiesList[j], choosenNode);
                    nodesWhereEnemiesCanBePlaced.Remove(choosenNode);
                    break;
                }
            }
        }
    }

    private void SetEnemiePosition(GameObject enemy, Node node)
    {
        var enemyInstance = Instantiate(enemy, transform);
        enemyInstance.transform.position = node.transform.position;
        ObjectTilePosition objectTilePosition = enemyInstance.GetComponent<ObjectTilePosition>();
        if(objectTilePosition != null)
        {
            objectTilePosition.SetTile(node);
        }
        else
        {
            enemyInstance.GetComponent<EnemiesGroup>().SetEnemiesTile(node);
        }
    }
}
