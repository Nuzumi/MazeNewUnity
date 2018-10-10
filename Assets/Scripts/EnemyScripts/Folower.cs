using System.Collections.Generic;
using UnityEngine;

public class Folower : MonoBehaviour {

    public GameObject follower = null;
    public float timeWait;

    public Node CurrentNode { get; set; }

    private Queue<Tuple<Vector2,Node>> positionQueue;
    private bool hasFollower;
    private bool follow;
    private ObjectTilePosition oTilePosition;
    private Folower folowerFolowerScript;

    private void Start()
    {
        if(follower != null)
        {
            hasFollower = true;
            positionQueue = new Queue<Tuple<Vector2, Node>>();
            oTilePosition = follower.GetComponent<ObjectTilePosition>();
            folowerFolowerScript = follower.GetComponent<Folower>();
        }     
    }

    private void Update()
    {
        if (hasFollower)
        {
            positionQueue.Enqueue(new Tuple<Vector2, Node>(transform.position,CurrentNode));

            if (follow || Time.timeSinceLevelLoad > timeWait)
            {
                follow = true;
                var dequeue = positionQueue.Dequeue();
                follower.transform.position = dequeue.Item1;
                folowerFolowerScript.CurrentNode = dequeue.Item2;
                oTilePosition.SetTile(dequeue.Item2);
            }
        }
    }
}
