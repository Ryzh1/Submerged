using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFloat : MonoBehaviour
{
    public Transform topLimit, bottomLimit;

    public bool goUp = true;

    float speed = 0.2f;

    void Update()
    {
        if(goUp)
        {
            transform.position += (topLimit.position - transform.position).normalized * (speed * Time.deltaTime);
            //transform.position = Vector2.Lerp(transform.position, topLimit.position, speed * Time.deltaTime); 

        }
        else
        {
            transform.position += (bottomLimit.position - transform.position).normalized * (speed * Time.deltaTime);
            //transform.position = Vector2.Lerp(transform.position, bottomLimit.position, speed * Time.deltaTime);
        }

        if (transform.position.y >= 0.45)
        {
            goUp = false;
        }

        if (transform.position.y <= -0.45)
        {
            goUp = true;
        }
    }
}
