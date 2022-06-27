using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float recievedDamage;
    public float speed;
    protected Rigidbody2D rb;
    public float smoothTime;
    public float agroDistance;
    public float maxRange;
    protected Vector2 movementPerSecond;
    public Transform player;
    protected float latestDirectionChangeTime;
    protected float directionChangeTime;
    protected bool newDir; //For sprite flip
    protected float rayLength;
    protected Vector2 movementDirection;


    public GameObject bubbleEffect;
    public GameObject bubbleObject;



    void Die()
    {
        Destroy(this.gameObject);
    }
    //Collision with the player projectile


    public void TakeDamage()
    {
        Vector3 scale = bubbleEffect.transform.localScale;
        if (scale.x > 0.05)
        {
            Instantiate(bubbleObject, gameObject.transform.position, Quaternion.identity);
            Die();
        }
        else
        {

            scale.x += recievedDamage;
            scale.y += recievedDamage;
            bubbleEffect.transform.localScale = scale;
        }
    }
    //Slowly make the bubble reset to 0;
    public void ResetBubble()
    {
        Vector3 scale = bubbleEffect.transform.localScale;
        scale.x -= 0.005f * Time.deltaTime;
        scale.y -= 0.005f * Time.deltaTime;
        bubbleEffect.transform.localScale = scale;
    }

    
    public void CheckDirection(float movement)
    {
        
        if (movement > 0 && newDir)
        {
            newDir = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (movement < 0 && newDir)
        {
            newDir = false;
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

    public void ChasingDirection(int rotation)
    {
        if(rotation == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }


}

