using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveAI : Enemy
{

    void Start()
    {
        directionChangeTime = 3f;
        rayLength = 1f;
        rb = GetComponent<Rigidbody2D>();
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
    }
    void FixedUpdate()
    {
        CheckRayCast();

    }
    private void Update()
    {
        if (bubbleEffect.transform.localScale.x > 0.0f)
        {

            ResetBubble();
        }

    }

    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * speed;
        newDir = true;
        CheckDirection(movementDirection.x);
    }

    void CheckRayCast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movementDirection, rayLength);
        Debug.DrawRay(transform.position, movementDirection * 2, Color.green);
        if (hit.collider != null)
        {
            
            if (!hit.collider.CompareTag("PlayerProjectile"))
            {

                calcuateNewMovementVector();
            }
            else
            {
                Movement();
            }
           

        }
        Movement();

    }

    void Movement()
    {
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();

        }
        rb.MovePosition(Vector2.Lerp(rb.position, rb.position + movementPerSecond * Time.deltaTime, smoothTime));
        //transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        //transform.position.y + (movementPerSecond.y * Time.deltaTime));
    }


}

