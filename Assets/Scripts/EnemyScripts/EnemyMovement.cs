using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public int playerFollowersCount;
    public float speed;
    public int lostCounterMax;

    private GameObject player;
    private List<GameObject> playerFollowers;
    public bool follow; // zmienic potem na privvate
    private Vector2 forceTaApply; 
    private Rigidbody2D rb;
    private Node[] playerNodes;
    private ObjectTilePosition objectTilePosition;
    private int lastIndex;
    private int lostCounter;

    private void Start()
    {
        playerNodes = new Node[8];
        objectTilePosition = GetComponent<ObjectTilePosition>();
        playerFollowers = new List<GameObject>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!follow && collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            GameObject tmp = player;
            playerFollowers.Add(player);
            player.GetComponent<ObjectTilePosition>().AddObjectToNotify(this,0);
            for(int i =0;i<playerFollowersCount; i++)
            {
                GameObject folower = tmp.GetComponent<Folower>().follower;
                playerFollowers.Add(folower);
                folower.GetComponent<ObjectTilePosition>().AddObjectToNotify(this, i + 1);
                tmp = folower;
            }

            follow = true;
        }
    }


    private void Update()
    {
        if (follow)
        {
            int followerToFollow = -1;
            for(int i = 0; i < playerFollowersCount+1; i++)
            {
                if (objectTilePosition.CheckIfPathIsClear(playerNodes[i]))
                {
                    followerToFollow = i;
                    break;
                }
            }

            if (followerToFollow != -1)
            {
                Vector3 diff = playerFollowers[followerToFollow].transform.position - transform.position;
                diff.Normalize();
                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                forceTaApply = Helper.getVersor(transform.position, playerFollowers[followerToFollow].transform.position) * speed;
            }
            else
            {

                Tuple<float, int> toFollow = new Tuple<float, int>(Mathf.Infinity, 8);
                for (int i = 0; i < playerFollowersCount+1; i++)
                {
                    float value = Mathf.Sqrt(Helper.distance(transform.position,playerFollowers[i].transform.position));
                    if (value <= toFollow.Item1)
                    {
                        toFollow.Item1 = value;
                        toFollow.Item2 = i;
                    }
                }

                int index = toFollow.Item2 == 0 ? 0 : toFollow.Item2 - 1;
                if(index == lastIndex)
                {
                    lostCounter++;
                    if(lostCounter >= lostCounterMax)
                    {
                        follow = false;
                        lostCounter = 0;
                        forceTaApply = Vector2.zero;
                        rb.velocity = Vector2.zero;
                        foreach(GameObject o in playerFollowers)
                        {
                            o.GetComponent<ObjectTilePosition>().RemoveObjectToNitify(this);
                        }
                        playerFollowers.Clear();
                    }
                }
                else
                {
                    lostCounter = 0;
                    lastIndex = index;
                    Vector3 diff = playerFollowers[index].transform.position - transform.position;
                    diff.Normalize();
                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                    forceTaApply = Helper.getVersor(transform.position, playerFollowers[index].transform.position) * speed;
                }   

            }
        }
    }

    private void FixedUpdate()
    {
        if (forceTaApply != Vector2.zero)
        {
            rb.AddForce(forceTaApply);
        }
    }

    public void SetsCurrentNodeState(Node node,int number)
    {
        playerNodes[number] = node;
    }


}
