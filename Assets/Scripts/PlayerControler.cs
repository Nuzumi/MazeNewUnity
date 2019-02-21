using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class PlayerControler : MonoBehaviour {

    public BoolEvent playerMoveStateChange;
    public float velocity;
    public float rollVelocityMultiplier;
    public float rollTime;

    public bool CanMove { get; set; }

    private Vector2 movementDirection;
    private Rigidbody2D rb;
    private bool makeingRoll;
    private float rollStartTime;
	
	void Start ()
    {
        CanMove = true;
        rb = GetComponent<Rigidbody2D>();
	}

    private void OnEnable()
    {
        playerMoveStateChange.AddListener(ChangePlayerMoveState);
    }

    private void OnDisable()
    {
        playerMoveStateChange.RemoveListener(ChangePlayerMoveState);
    }


    void Update ()
    {
        if (CanMove)
        {
            if (Input.GetKeyDown(KeyCode.Space) && movementDirection != Vector2.zero && !makeingRoll)
            {
                makeingRoll = true;
                rollStartTime = Time.timeSinceLevelLoad;
                velocity *= rollVelocityMultiplier;
            }

            if (gameObject.activeSelf && CanMove && !makeingRoll)
            {
                movementDirection.x = CnInputManager.GetAxis("Horizontal");
                movementDirection.y = CnInputManager.GetAxis("Vertical");
            }
            else
            {
                if (makeingRoll)
                {
                    if (rollStartTime + rollTime < Time.timeSinceLevelLoad)
                    {
                        velocity /= rollVelocityMultiplier;
                        makeingRoll = false;
                        rb.velocity = Vector2.zero;
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!makeingRoll)
        {
            rb.velocity = movementDirection.normalized * velocity * Time.deltaTime;
        }
        else
        {
            rb.AddForce(movementDirection.normalized * velocity * Time.deltaTime * 3);
        }
    }

    private void ChangePlayerMoveState(bool state)
    {
        CanMove = state;
        movementDirection = Vector3.zero;
    }

    public void MakeRoll()
    {
        if( movementDirection != Vector2.zero && !makeingRoll)
        {
            makeingRoll = true;
            rollStartTime = Time.timeSinceLevelLoad;
            velocity *= rollVelocityMultiplier;
        }
    }
}
