using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState { None, InClosedList, InOpenList}

public class Node : MonoBehaviour , IComparable<Node>
{

    public enum WalsDirection { left,top,right,down}
    public bool Visited { get; set; }
    public List<GameObject> neighbours;
    public Node[] neighboursWithDirection;
    public List<GameObject> neighboursToGo;

    public bool[] wals;

    public float WholeDistance
    {
        get { return Distance + HeuristicDistance; }
    }
    public Node Parent { get; set; }

    public float Distance { get; set; }
    public float HeuristicDistance { get; set; }
    public NodeState State { get; set; }

    private bool canGoTo;
    public bool  CanGoTo
    {
        get { return canGoTo; }
        set
        {
            canGoTo = value;
            spriteRenderer.color = canGoTo ? Color.green : Color.red;
        }
    }
    public Node CanGoFromDirection { get; set; }
    public bool IsPathBegining
    {
        get { return neighboursToGo.Count == 2; }
    }

    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        neighboursWithDirection = new Node[4];
        neighbours = new List<GameObject>();
        neighboursToGo = new List<GameObject>();
        Visited = false;
        wals = new bool[4]; // 0-left  1-top  2-right   3-down
        CanGoTo = true;
    }


    public void addNeighbour(GameObject node)
    {
        if (!neighbours.Contains(node))
        {
            neighbours.Add(node);
            node.GetComponent<Node>().addNeighbour(gameObject);
        }
    }

    public void deleteNeighbour(GameObject node)
    {
        if (neighbours.Contains(node))
        {
            neighbours.Remove(node);
            node.GetComponent<Node>().deleteNeighbour(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "tile")
        {
            if (!neighbours.Contains(collision.gameObject))
            {
                neighbours.Add(collision.gameObject);
                AssignNeighbour(collision.gameObject);
            }
        }
    }

    private void AssignNeighbour(GameObject neighbour)
    {
        Vector2 difference = neighbour.transform.position - transform.position;
        if(difference == Vector2.left)
        {
            neighboursWithDirection[(int)WalsDirection.left] = neighbour.GetComponent<Node>();
        }
        else
        {
            if(difference == Vector2.right)
            {
                neighboursWithDirection[(int)WalsDirection.right] = neighbour.GetComponent<Node>();
            }
            else
            {
                if(difference == Vector2.up)
                {
                    neighboursWithDirection[(int)WalsDirection.top] = neighbour.GetComponent<Node>();
                }
                else
                {
                    neighboursWithDirection[(int)WalsDirection.down] = neighbour.GetComponent<Node>();
                }
            }
        }
    }

    public float SqrDistanceToNode(Node other)
    {
        return (other.transform.position - transform.position).sqrMagnitude;
    }

    public float SqrDistanceToNode(GameObject other)
    {
        return (other.transform.position - transform.position).sqrMagnitude;
    }

    public int CompareTo(Node other)
    {
        if (WholeDistance < other.WholeDistance)
            return -1;
        else if (WholeDistance > other.WholeDistance)
            return 1;
        else
            return 0;
    }

    public void ClearNode()
    {
        State = NodeState.None;
        Distance = 0;
        HeuristicDistance = 0;
        Parent = null;
    }

    public List<Node> GetRoutFromThisNode(Node oppositeNode)
    {
        List<Node> result = new List<Node>();
        Node previousNode = oppositeNode;
        Node currentNode = this;
        result.Add(this);
        while(currentNode.neighboursToGo.Count == 2)
        {
            Node tmpCurrentNode = currentNode;
            GameObject tmpCurrent = currentNode.neighboursToGo.Find(n => n.GetComponent<Node>() != previousNode);
            if(tmpCurrent != null)
            {
                currentNode = tmpCurrent.GetComponent<Node>();
            }
            else
            {
                currentNode = previousNode;
            }

            previousNode = tmpCurrentNode;
            currentNode.CanGoFromDirection = previousNode;
            currentNode.CanGoTo = false;
            result.Add(currentNode);
        }
        return result ;
    }



}
