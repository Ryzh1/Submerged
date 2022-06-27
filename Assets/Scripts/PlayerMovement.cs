using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public float moveHorizontal;
    public float moveVertical;
    public float mSpeed;
    public SpriteRenderer sr;
    
    public Camera cam;

    public GameObject alert;

    public ParticleSystem particles;
    public Rigidbody2D rb;
    private Vector2 movement;
    Vector2 mousePos;

    public float maxOxygen;
    public float currentOxygen;
    public float oxygenDepriciationRate;

    public Image OxygenBar;

    public GameObject deathParticle;

    public Material matWhite;
    public Material matDefault;



    void Start()
    {
        //particles = GetComponent<ParticleSystem>();
        currentOxygen = maxOxygen;
        sr = GetComponentInChildren<SpriteRenderer>();
        matWhite = Resources.Load("Whiteflash", typeof(Material)) as Material;
        matDefault = sr.material;

    }
    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical).normalized;
        

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //particles.emissionRate = oxygenAmount;
        
        if (movement != Vector2.zero)
        {
            currentOxygen -= oxygenDepriciationRate * Time.deltaTime;
            
            

            if (!particles.isPlaying) 
            {
                particles.Play();
                FindObjectOfType<AudioManager>().Play("PlayerEngine", 0.5f);
                FindObjectOfType<AudioManager>().Play("Propeller", 1f);
            }
  
        }
        else if(movement == Vector2.zero)
        {
            particles.Stop();
            FindObjectOfType<AudioManager>().Stop("Propeller");
            FindObjectOfType<AudioManager>().Stop("PlayerEngine");
        }

        if(currentOxygen < 0)
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath", 1f);
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if(rb.rotation > 0 || rb.rotation < -180)
        {
            sr.flipY = true;
        }
        else
        {

            sr.flipY = false;
        }

        if(currentOxygen/maxOxygen <= 0.25f)
        {
            alert.SetActive(true);
        }
        else
        {
            alert.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        rb.AddForce(movement * mSpeed);
        OxygenBar.fillAmount = currentOxygen / maxOxygen;
    }


    public void TakeDamage(float damage)
    {
        sr.material = matWhite;
        currentOxygen -= damage;
        Invoke("ResetMaterial", 0.1f);
    }
    void ResetMaterial()
    {
        sr.material = matDefault;
    }

}
