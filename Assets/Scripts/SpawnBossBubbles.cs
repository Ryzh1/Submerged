using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBossBubbles : MonoBehaviour
{
    public GameObject bossBubbles;


    void Start()
    {
        Instantiate(bossBubbles, new Vector2(transform.position.x + Random.Range(-7, 7), transform.position.y + Random.Range(-1, 1)), Quaternion.identity);
        Instantiate(bossBubbles, new Vector2(transform.position.x + Random.Range(-7, 7), transform.position.y + Random.Range(-1, 1)), Quaternion.identity); 
    }
}


