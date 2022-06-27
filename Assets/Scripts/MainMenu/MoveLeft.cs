using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    float speed = 2f;
    SpriteRenderer sr;
    public Sprite[] plants;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        speed = Random.Range(1f, 2f);
        sr.sprite = plants[Random.Range(0, plants.Length)];
        Object.Destroy(gameObject, 30.0f);
    }
    void Update()
    {
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }
}
