using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAI : Enemy
{
    private Transform target;

    
    private void Start()
    {
        directionChangeTime = 3f;
        rayLength = 1f;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
        
    }
    private void Update()
    {
        if (bubbleEffect.transform.localScale.x > 0.0f)
        {

            ResetBubble();
        }
    }

    void FixedUpdate()
    {       
        
        if(target != null)
        {
            if (Vector2.Distance(rb.position, target.position) > maxRange)
            {
                CheckRayCast();

            }
            else if (Vector2.Distance(rb.position, target.position) > agroDistance && Vector2.Distance(rb.position, target.position) < maxRange)
            {
                if (target.position.x >= rb.position.x)
                {
                    ChasingDirection(0);
                }
                else
                {
                    ChasingDirection(180);
                }
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                // rb.MovePosition(Vector2.Lerp(rb.position, target + movementPerSecond * Time.deltaTime, smoothTime));

            }
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
