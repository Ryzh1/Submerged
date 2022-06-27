using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    public Camera cam;
    Vector2 mousePos;

    bool reloading = false;
    bool bulletBool = true;
    
    public ParticleSystem shootParticles;


    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            FireBubbles();
        }

    }


    void FireBubbles()
    {
        FindObjectOfType<AudioManager>().Play("PlayerShoot", Random.Range(0.9f, 1.2f));
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        shootParticles.Play();
    }

    IEnumerator Wait(float howeverLong, bool theBool)
    {
        yield return new WaitForSeconds(howeverLong);
        theBool = !theBool;
    }
}
