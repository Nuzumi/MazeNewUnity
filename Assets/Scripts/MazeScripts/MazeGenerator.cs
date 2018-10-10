using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

    public bool Hiden;
    public string Seed;
    public GameObject characterControler;

    private GameObject startNode;
    private List<GameObject> nodesList;
    private List<Tuple<GameObject, GameObject>> edgesList;
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
        edgesList = new List<Tuple<GameObject, GameObject>>();
        visitedNodes = new List<GameObject>();

        StartCoroutine("InitializeWithDelay");
        
    }

    private IEnumerator InitializeWithDelay()
    {
        yield return new WaitForFixedUpdate();
        Initialize();
    }

    public void Initialize()
    {
        nodesList = new List<GameObject>(GameObject.FindGameObjectsWithTag("tile"));
        MakeMaze();
        RenderMaze();
        characterControler.GetComponent<CharacterControler>().SetChcaractersStartPoints(nodesList);
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

    private GameObject RemoveRandomNode(List<GameObject> list)
    {
        GameObject value = list[rand.Next(list.Count)];
        list.Remove(value);
        return value;
    }

    private GameObject TakeRandomNode(List<GameObject> list)
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
        edgesList.Add(new Tuple<GameObject, GameObject>(node,secoundNode));

        node.GetComponent<Node>().neighboursToGo.Add(secoundNode);
        secoundNode.GetComponent<Node>().neighboursToGo.Add(node);

        if (node.transform.position.x > secoundNode.transform.position.x)
        {
            node.GetComponent<Node>().wals[(int)Node.WalsDirection.left] = true;
            secoundNode.GetComponent<Node>().wals[(int)Node.WalsDirection.right] = true;
        }
        else
        {
            if (node.transform.position.x < secoundNode.transform.position.x)
            {
                node.GetComponent<Node>().wals[(int)Node.WalsDirection.right] = true;
                secoundNode.GetComponent<Node>().wals[(int)Node.WalsDirection.left] = true;
            }
            else
            {
                if (node.transform.position.y > secoundNode.transform.position.y)
                {
                    node.GetComponent<Node>().wals[(int)Node.WalsDirection.down] = true;
                    secoundNode.GetComponent<Node>().wals[(int)Node.WalsDirection.top] = true;
                }
                else
                {
                    node.GetComponent<Node>().wals[(int)Node.WalsDirection.top] = true;
                    secoundNode.GetComponent<Node>().wals[(int)Node.WalsDirection.down] = true;
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
