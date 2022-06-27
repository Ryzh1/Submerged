using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineProjectile : MonoBehaviour
{
    public float damage;

    void Update()
    {
        Destroy(this.gameObject, 5f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {

            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
