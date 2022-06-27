using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class Boss_Attacks : MonoBehaviour
{
    
    public GameObject rocks;
    public GameObject spines;
    public ClawScript[] claws;

    public GameObject spineSprite;
    
    
    public Transform[] spineSpawns;
    public int ySizeEarthquake;
    public int xMinEarthquake;
    public int xMaxEarthquake;

    private GameObject player;
    private Color enragedColor;
    private SpriteRenderer sr;

    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        enragedColor = new Color(255, 0, 0);
        claws[0].gameObject.SetActive(true);
        claws[1].gameObject.SetActive(true);
        claws[2].gameObject.SetActive(false);
        claws[3].gameObject.SetActive(false);
        
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        foreach (var claw in claws)
        {
            var angle = Mathf.Rad2Deg * (claw.transform.position - player.transform.position).normalized;
            if(claw.minZ < 0)
            {
                angle.x -= 30;
            }
            else
            {
                angle.x += 30;
            }
            claw.transform.rotation = Quaternion.AngleAxis(Mathf.Clamp(angle.x,claw.minZ,claw.maxZ), new Vector3(0, 0, 1));
        }
    }

    public void ClawAttack(bool state)
    {
        foreach (var claw in claws)
        {
            if (state)
            {
                claw.Attack();
                claw.sr.sprite = claw.sprites[1];
                claw.ClawTransition();
            }
            
        }
    }



    public IEnumerator SpineAttack()
    {
        
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < spineSpawns.Length; i++)
        {
            var spine = Instantiate(spines, spineSpawns[i].position, spineSpawns[i].rotation);
            spine.GetComponent<Rigidbody2D>().AddRelativeForce(transform.up * 5, ForceMode2D.Impulse);
        }
    }

    public void Earthquake()
    {
        ProCamera2DShake.Instance.Shake("ScreenShake");

        for (int i = 0; i < 10; i++)
        {
            var random = Random.Range(xMinEarthquake, xMaxEarthquake);
            Instantiate(rocks, new Vector3(random, ySizeEarthquake), Quaternion.identity);
        }
    }


    public void EnragedChange()
    {
        sr.color = enragedColor;
        foreach (var i in claws)
        {
            i.GetComponent<SpriteRenderer>().color = enragedColor;
           
        }
        claws[2].gameObject.SetActive(true);
        claws[3].gameObject.SetActive(true);

    }
}
