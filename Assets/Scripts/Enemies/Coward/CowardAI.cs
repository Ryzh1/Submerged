using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowardAI : Enemy
{


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rayLength = 1f;
        directionChangeTime = 3f;
        rb = GetComponent<Rigidbody2D>();
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
       

    }
    private void Update()
    {
        if(player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer < agroDistance)
            {

                StartCoroutine(Run());
            }
            else
            {

                speed = 0.5f;

            }
        }


        if (bubbleEffect.transform.localScale.x > 0.0f)
        {
           
            ResetBubble();
        }

    }
    


    void FixedUpdate()
    {

        CheckRayCast();      

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


    IEnumerator Run()
    {
        speed = 5;
        rb.MovePosition(Vector2.Lerp(rb.position, rb.position + movementPerSecond * Time.deltaTime, smoothTime));
        yield return new WaitForSeconds(1f);
    }


}

