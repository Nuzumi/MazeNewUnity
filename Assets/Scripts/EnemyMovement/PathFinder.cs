using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder 
{
    private PriorityQueue<Node> openList;

    public List<Node> GetPath(Node from, Node to)
    {
        FindPath(from, to);
        return Path(from, to);
    }


    private void FindPath(Node from, Node to)
    {
        openList = new PriorityQueue<Node>();

        openList.Enque(from);
        while (!openList.IsEmpty)
        {
            Node q = openList.Deque();
            foreach (var n in q.neighbours)
            {
                Node node = n.GetComponent<Node>();
                if(n == to)
                {
                    to.Parent = q;
                    break;
                }

                float distance = 1 + q.Distance;
                float heuristicDistance = node.SqrDistanceToNode(to);

                if((node.State == NodeState.InOpenList || node.State == NodeState.InClosedList) && node.Distance < distance + heuristicDistance )
                {
                    break;
                }

                node.Parent = q;
                node.Distance = 1 + q.Distance;
                node.HeuristicDistance = node.SqrDistanceToNode(to);
                if (node.State != NodeState.None)
                    openList.RemoveElement(node);
                openList.Enque(node);
                node.State = NodeState.InOpenList;
            }
            q.State = NodeState.InClosedList;
            
        }
    }

    private List<Node> Path(Node from, Node to)
    {
        List<Node> result = new List<Node>
        {
            to
        };

        Node current = to;
        while(current.Parent != null)
        {
            result.Add(current.Parent);
            current = current.Parent;
        }

        result.Reverse();
        return result;
    }
}


