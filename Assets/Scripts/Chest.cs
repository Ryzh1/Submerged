using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRen;
    public GameManager gm;

    public bool finalChest = false;

    bool isOpen = false;

    void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(finalChest)
            {
                SceneManager.LoadScene(5);
            }

            if(!isOpen)
            {
                spriteRen.sprite = sprites[1];
                gm.chestsFound++;
                FindObjectOfType<AudioManager>().Play("ChestOpen", 1f);
                isOpen = true;
            }
        }      
    }
}
