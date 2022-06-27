using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBubble : MonoBehaviour
{
    public GameObject popParticles;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage();
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            if(other.gameObject.name == "WeakSpot")
            {
                other.gameObject.GetComponentInParent<Boss>().CheckColl(other);
            }
            else
            {
                other.gameObject.GetComponent<Boss>().CheckColl(other);
            }
            
        }

        //GameObject party = Instantiate(popParticles, gameObject.transform.position, Quaternion.identity);
        
            
        Destroy(this.gameObject);
    }

}
