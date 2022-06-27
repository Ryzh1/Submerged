using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawScript : MonoBehaviour
{
    public float minZ;
    public float maxZ;
    private int clawDamage = 10;
    
    public SpriteRenderer sr;
    public Sprite[] sprites;
    public Transform OverlapPoint;

    public float attackRange = 1f;
    public LayerMask attackMask;


    private void Start()
    {
    
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[0];
    }

    public void Attack()
    {


        Collider2D colInfo = Physics2D.OverlapCircle(OverlapPoint.position, attackRange, attackMask);
        if (colInfo != null)
        {

            colInfo.GetComponent<PlayerMovement>().TakeDamage(clawDamage);
        }
    }



    void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(OverlapPoint.position, attackRange);
    }



    IEnumerator ChangeSprite()
    {
        yield return new WaitForSeconds(0.5f);
        sr.sprite = sprites[0];
    }

    internal void ClawTransition()
    {
        if (gameObject.activeSelf == true)
        {
            StartCoroutine("ChangeSprite");
        }
        
    }
}
