using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

    public bool Hiden;
    public string Seed;
    [SerializeField]
    private int circlesPercent;//percent of tiles to modify to get circles in maze
    [SerializeField]
    private GameObjectListEvent mazeCreated;

    private List<GameObject> nodesList;
    private GameObject startNode;
    private List<GameObject> frontierNode;
    private List<GameObject> visitedNodes;
    private System.Random rand;

    private void Start()
    {
        if(Seed == string.Empty)
        {
            rand = new System.Random();
        }
        else
        {
            rand = new System.Random(Seed.GetHashCode());
        }
       
        frontierNode = new List<GameObject>();
        visitedNodes = new List<GameObject>();
        StartCoroutine("InitializeWithDelay");
        
    }

    private IEnumerator InitializeWithDelay()
    {
        yield return new WaitForFixedUpdate();
        Initialize();
        Debug.Log(nodesList.Count);
    }

    public void Initialize()
    {
        nodesList = new List<GameObject>(GameObject.FindGameObjectsWithTag("tile"));
        MakeMaze();
        CreateCirclesInMaze();
        RenderMaze();
        mazeCreated.Invoke(nodesList);
    }

    private void MakeMaze()//prime
    {
        startNode = nodesList[rand.Next(nodesList.Count)];
        GameObject currentNode = startNode;
        visitedNodes.Add(currentNode);
        Node currentNodeClass = currentNode.GetComponent<Node>();
        currentNodeClass.Visited = true;

        foreach(GameObject n in currentNodeClass.neighbours)
        {
            frontierNode.Add(n);
        } 

        while(frontierNode.Count != 0)
        {
            GameObject nextNode = RemoveRandomNode(frontierNode);
            nextNode.GetComponent<Node>().Visited = true;
            AddEdge(nextNode);
            visitedNodes.Add(nextNode);
            foreach(GameObject n in nextNode.GetComponent<Node>().neighbours)
            {
                if (!n.GetComponent<Node>().Visited && !frontierNode.Contains(n))
                {
                    frontierNode.Add(n);
                }
            }
        }
    }

    private void CreateCirclesInMaze()
    {
        int tilesToModify = nodesList.Count * circlesPercent / 100;
        Debug.Log(tilesToModify + " tiles to modify");
        List<Node> candidatesToModify = nodesList.Select(n => n.GetComponent<Node>()).Where(n => n.neighboursToGo.Count <= 2).ToList();

        for(int i = 0; i < tilesToModify; i++)
        {
            Node circleNode = RemoveRandomNode(candidatesToModify);
            GameObject secoundNode = TakeRandomNode(circleNode.neighbours);
            AddNodeToWallList(circleNode.gameObject, secoundNode);
            candidatesToModify = candidatesToModify.Where(n => n.neighboursToGo.Count == 1).ToList();
        }
    }

    private T RemoveRandomNode<T>(List<T> list)
    {
        T value = list[rand.Next(list.Count)];
        list.Remove(value);
        return value;
    }

    private T TakeRandomNode<T>(List<T> list)
    {
        return list[rand.Next(list.Count - 1)];
    }

    private void AddEdge(GameObject node)
    {
        List<GameObject> tmp = new List<GameObject>();
        foreach(GameObject n in node.GetComponent<Node>().neighbours)
        {
            if (n.GetComponent<Node>().Visited)
            {
                tmp.Add(n);
            }
        }

        GameObject secoundNode = tmp[rand.Next(tmp.Count)];

        AddNodeToWallList(node, secoundNode);
    }

    private void AddNodeToWallList(GameObject node1, GameObject node2)
    {
        node1.GetComponent<Node>().neighboursToGo.Add(node2);
        node2.GetComponent<Node>().neighboursToGo.Add(node1);

        if (node1.transform.position.x > node2.transform.position.x)
        {
            node1.GetComponent<Node>().wals[(int)Node.WalsDirection.left] = true;
            node2.GetComponent<Node>().wals[(int)Node.WalsDirection.right] = true;
        }
        else
        {
            if (node1.transform.position.x < node2.transform.position.x)
            {
                node1.GetComponent<Node>().wals[(int)Node.WalsDirection.right] = true;
                node2.GetComponent<Node>().wals[(int)Node.WalsDirection.left] = true;
            }
            else
            {
                if (node1.transform.position.y > node2.transform.position.y)
                {
                    node1.GetComponent<Node>().wals[(int)Node.WalsDirection.down] = true;
                    node2.GetComponent<Node>().wals[(int)Node.WalsDirection.top] = true;
                }
                else
                {
                    node1.GetComponent<Node>().wals[(int)Node.WalsDirection.top] = true;
                    node2.GetComponent<Node>().wals[(int)Node.WalsDirection.down] = true;
                }
            }
        }
    }

    private void RenderMaze()
    {
        foreach(GameObject t in nodesList)
        {
            for(int i = 0; i < 4; i++)
            {
                if (t.GetComponent<Node>().wals[i])
                {
                    t.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
                }
            }

            if (Hiden)
            {
                t.transform.GetChild(0).gameObject.SetActive(false);
            }          
            t.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}
