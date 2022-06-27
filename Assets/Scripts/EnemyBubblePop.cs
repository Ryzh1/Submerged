using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBubblePop : MonoBehaviour
{
    PlayerMovement player;
    public GameObject popParticles;
    public GameManager gm;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player.gameObject)
        {
            player.currentOxygen += 10;
            gm.enemiesDefeated += 1;
            FindObjectOfType<AudioManager>().Play("EnemyPop", 1f);
            FindObjectOfType<AudioManager>().Play("EnemyPopBubbles", 1f);
            Instantiate(popParticles, gameObject.transform.position, Quaternion.identity);
            GameObject.Destroy(gameObject);
        }
    }
}
