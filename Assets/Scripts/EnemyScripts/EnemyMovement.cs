using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EnemyMovement : MonoBehaviour{

    private GameObject player;
    private List<GameObject> playerFollowers;
    private bool follow; 
    private Vector2 forceTaApply; 
    private Rigidbody2D rb;
    private Node[] playerNodes;
    private ObjectTilePosition objectTilePosition;
    private int lastIndex;
    private int lostCounter;
    private ActualUnitStatistic actualUnitStatistic;
    private int lostCounterMaxMultiplier = 3;

    private void Start()
    {
        actualUnitStatistic = GetComponent<ActualUnitStatistic>();
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
            for(int i =0;i<actualUnitStatistic.PlayerFollowerCount; i++)
            {
                GameObject folower = tmp.GetComponent<Folower>().follower;
                playerFollowers.Add(folower);
                folower.GetComponent<ObjectTilePosition>().AddObjectToNotify(this, i + 1);
                tmp = folower;
            }

            follow = true;

            if (transform.parent.name.Contains("Group"))
            {
                transform.parent.SendMessage("SetEnemiesTrigger", collision);
            }
        }
    }


    private void Update()
    {
        if (follow)
        {
            int followerToFollow = -1;
            for(int i = 0; i < actualUnitStatistic.PlayerFollowerCount + 1; i++)
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
                forceTaApply = diff * actualUnitStatistic.SpeedModifier;
                lostCounter = 0;
            }
            else
            {

                lostCounter++;
                if(lostCounter == actualUnitStatistic.LostCounterMax * lostCounterMaxMultiplier)
                {
                    StopFollowing();
                }
            }
        }
    }

    private void StopFollowing()
    {
        follow = false;
        lostCounter = 0;
        forceTaApply = Vector2.zero;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0.4f;
        foreach (GameObject o in playerFollowers)
        {
            o.GetComponent<ObjectTilePosition>().RemoveObjectToNitify(this);
        }
        playerFollowers.Clear();
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

    public void SimulateOnTriggerEnter(Collider2D collision)
    {
        OnTriggerEnter2D(collision);
    }

}
