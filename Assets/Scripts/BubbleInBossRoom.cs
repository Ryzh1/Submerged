using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleInBossRoom : MonoBehaviour
{
    PlayerMovement player;
    public GameObject popParticles;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player.gameObject)
        {
            player.currentOxygen += 10;
            FindObjectOfType<AudioManager>().Play("EnemyPop", 1f);
            FindObjectOfType<AudioManager>().Play("EnemyPopBubbles", 1f);
            Instantiate(popParticles, gameObject.transform.position, Quaternion.identity);
            GameObject.Destroy(gameObject);
        }
    }
}
