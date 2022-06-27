using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBubble : MonoBehaviour
{
    PlayerMovement player;
    public GameObject chest;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player.gameObject)
        {
            player.currentOxygen += 10;
            //FindObjectOfType<AudioManager>().Play("EnemyPop", 1f);
            //FindObjectOfType<AudioManager>().Play("EnemyPopBubbles", 1f);
            Instantiate(chest, gameObject.transform.position, Quaternion.identity);
            GameObject.Destroy(gameObject);
        }
    }
}
