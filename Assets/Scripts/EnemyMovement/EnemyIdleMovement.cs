using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyIdleMovement : MonoBehaviour
{
    [SerializeField]
    private FloatIntGameObjectEvent setTimer;
    [SerializeField]
    private IntGameObjectEvent timerResponse;

    private ObjectTilePosition objectTilePosition;
    private bool idleMove;
    private Vector2 movementForce;
    private Rigidbody2D rb;
    private List<Node> currentRoute;
    private Vector2 vectorToNextNode;
    private ActualUnitStatistic statistic;
    private float centerOffset = 0.1f;

    private void OnEnable()
    {
        timerResponse.AddListener(TimerResponseMethode);
    }

    private void OnDisable()
    {
        timerResponse.RemoveListener(TimerResponseMethode);
    }

    private void Start()
    {
        statistic = GetComponent<ActualUnitStatistic>();
        currentRoute = new List<Node>();
        rb = GetComponent<Rigidbody2D>();
        objectTilePosition = GetComponent<ObjectTilePosition>();
        idleMove = true;
        objectTilePosition.GetActiveTile().CanGoTo = false;
        EstablisheMovement();
    }

    private Node GetRandomNodeToGo()
    {
        List<Node> posibleNodes = objectTilePosition.GetActiveTile().neighboursToGo.
            FindAll(n => n.GetComponent<Node>().CanGoTo).
            Select(n => n.GetComponent<Node>()).ToList();
        if(posibleNodes.Count > 0)
            return posibleNodes[Random.Range(0, posibleNodes.Count())];
        return null;
    }

    private void EstablisheMovement()
    {
        Node target = GetRandomNodeToGo();
        if(target != null)
        {
            if (target.IsPathBegining)
            {
                currentRoute = target.GetRoutFromThisNode(objectTilePosition.GetActiveTile());
            }
            else
            {
                target.CanGoTo = false;
                currentRoute.Add(target);
            }

            movementForce = GetVectorToNextNode().normalized;
            vectorToNextNode = GetVectorToNextNode();
        }
        else
        {
            movementForce = Vector2.zero;
            setTimer.Invoke(1, 1, gameObject);
        }
    }

    private void SetNextMove()
    {
        currentRoute.RemoveAt(0);
        if (currentRoute.Count == 0)
        {
            EstablisheMovement();
        }
        objectTilePosition.GetActiveTile().CanGoTo = true;
    }

    private Vector2 GetVectorToNextNode()
    {
        return (currentRoute[0].transform.position - transform.position);
    }

    private void TimerResponseMethode(int dummy, GameObject caller)
    {
        if (caller == gameObject)
            EstablisheMovement();
    }

    private void Update()
    {
        if(currentRoute.Count != 0)
        {
            vectorToNextNode = GetVectorToNextNode();
            movementForce = vectorToNextNode.normalized;
            if (vectorToNextNode.sqrMagnitude < centerOffset)
            {
                SetNextMove();
            }
        }
    }

    private void FixedUpdate()
    {
        if (idleMove)
        {
            rb.AddForce(movementForce * statistic.SpeedModifier);
            Vector2 forceVersor = movementForce.normalized;
            float rot_z = Mathf.Atan2(forceVersor.y, forceVersor.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
    }
}
