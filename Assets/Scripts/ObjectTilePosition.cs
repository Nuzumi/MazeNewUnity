using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectTilePosition : MonoBehaviour {


    public Node currentTile;
    public bool active;
    public Folower folowerScript;

    private List<Tuple<EnemyMovement,int>> objectsToNotify;

    private void Start()
    {
        objectsToNotify = new List<Tuple<EnemyMovement,int>>();
    }

    private void Update()
    {
        if(active && currentTile != null )
        {
            CheckPosition();
        }
    }

    private void CheckPosition()
    {
        Vector2 difference = transform.position - currentTile.transform.position;
        if (Mathf.Abs(difference.x) > 0.5f || Mathf.Abs(difference.y) > 0.5f)
        {
            if (difference.x > 0.5f)
            {
                currentTile = currentTile.neighboursWithDirection[(int)Node.WalsDirection.right];
            }
            else
            {
                if (difference.x < -0.5f)
                {
                    currentTile = currentTile.neighboursWithDirection[(int)Node.WalsDirection.left];
                }
                else
                {
                    if (difference.y > 0.5f)
                    {
                        currentTile = currentTile.neighboursWithDirection[(int)Node.WalsDirection.top];
                    }
                    else
                    {
                        currentTile = currentTile.neighboursWithDirection[(int)Node.WalsDirection.down];
                    }
                }
            }

            if(folowerScript != null)
            {
                folowerScript.CurrentNode = currentTile;
            }

            foreach(var g in objectsToNotify)
            {
                g.Item1.SetsCurrentNodeState(currentTile, g.Item2);
            }
        }
    }

    public bool CheckIfPathIsClear(Node.WalsDirection direction)
    {
        if (currentTile.neighboursWithDirection[(int)direction] != null) 
        return currentTile.neighboursWithDirection[(int)direction].neighboursToGo.Contains(currentTile.gameObject);

        return false;
    }

    public bool CheckIfPathIsClear(Node node)
    {
        if(node != null)
        {
            if (node == currentTile)
                return true;
            return currentTile.neighboursToGo.Contains(node.gameObject);
        }
        return false;
    }

    public bool CheckIfIsConnection(Node node)
    {
        if(node != null)
        {
            if (node == currentTile)
                return true;
            return currentTile.neighbours.Contains(node.gameObject);
        }
        return false;
    }

    public void AddObjectToNotify(EnemyMovement toNotify,int number)
    {
        objectsToNotify.Add(new Tuple<EnemyMovement, int>(toNotify,number));
        toNotify.SetsCurrentNodeState(currentTile, number);
    }

    public void RemoveObjectToNitify(EnemyMovement toRemove)
    {
        objectsToNotify.Remove(objectsToNotify.Where(r => r.Item1 == toRemove).FirstOrDefault());
    }

    public void SetTile(Node node)
    {
        if (active)
        {
            currentTile = node;
        }
        else
        {
            currentTile = node;
            if(objectsToNotify!= null && objectsToNotify.Count != 0)
            {
                foreach (var g in objectsToNotify)
                {
                    g.Item1.SetsCurrentNodeState(currentTile, g.Item2);
                }
            }
        }
    }
}
