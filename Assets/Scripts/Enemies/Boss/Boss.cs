using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Slider healthBar;
    public int health;
    private int checkHealth;
    public bool isInvulnerable = false;
    public bool enraged;
    private Animator anim;
    
    public Transform weakspot;

    public GameObject massiveChest;
    public GameObject oxygenBubble;
    public int numberOfHits;
    public int maxNumberOfHits = 3;
    private bool isDead;

    public GameObject bubbleSpawner;


    private void Start()
    {
        numberOfHits = 0;
        checkHealth = health;
        anim = GetComponent<Animator>();
        isDead = false;
    }
    private void Update()
    {
        healthBar.value = health;

    }


    public void CheckColl(Collider2D col)
    {
        if(col.GetType() == typeof(CircleCollider2D))
        {
            TakeDamage(50);
            numberOfHits++;

            if (numberOfHits >= maxNumberOfHits)
            {
                weakspot.gameObject.SetActive(false);
                numberOfHits = 0;
                StartCoroutine("WeakSpotTimer");
            }
        }
        else
        {
            TakeDamage(5);
        }

        if (checkHealth - health >= 100)
        {
            checkHealth = health;
            Instantiate(oxygenBubble, transform.position, transform.rotation);
        }
    }
    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
        {
            return;
        }


        health -= damage;
        if(health <= 500 && !enraged)
        {
            enraged = true;
            Instantiate(bubbleSpawner, new Vector2(0, -7), Quaternion.identity);
            GetComponent<Animator>().SetBool("isEnraged", true);
        }


        if(health <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }


    IEnumerator WeakSpotTimer()
    {
        yield return new WaitForSeconds(Random.Range(5, 15));
        int rand = Random.Range(1, 3);
        switch (rand)
        {
            case 1:
                weakspot.transform.localPosition = new Vector3(0, 0.6f,0);
                break;
            case 2:
                weakspot.transform.localPosition = new Vector3(0.6f, -0.15f,0);
                break;
            case 3:
                weakspot.transform.localPosition = new Vector3(-0.6f, -0.15f,0);
                break;
            case 4:
                weakspot.transform.localPosition = new Vector3(0.55f, 0.75f, 0);
                break;
            case 5:
                weakspot.transform.localPosition = new Vector3(-0.55f, 0.75f, 0);
                break;


        }
        weakspot.gameObject.SetActive(true);
    }

    void Die()
    {
        Instantiate(massiveChest, transform.position, Quaternion.identity);
        Destroy(this.gameObject, 1f);
        anim.SetTrigger("dead");
    }
}
