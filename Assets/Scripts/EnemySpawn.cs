using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemies;
    public int xMaxSize;
    public int yMaxSize;
    public int xMinSize;
    public int yMinSize;

    Vector3 targetPosition;
    LayerMask mask;
    Collider2D[] collide;
    private void Start()
    {
        mask = LayerMask.GetMask("Wall");
        Invoke(nameof(Spawn), 0.1f);

    }

    void Spawn()
    {
        var spawnLocations = new List<Vector2>();
        for (int i = xMinSize; i < xMaxSize; i += 2)
        {
            for (int j = yMinSize; j < yMaxSize; j += 2)
            {

                targetPosition = new Vector2(i,j);  
                collide = Physics2D.OverlapCircleAll(targetPosition, 0.2f, mask);
                if (!collide.Any())
                {
                    spawnLocations.Add(targetPosition);
                }
                
            }
        }

        foreach (var enemy in enemies)
        {

            for (int i = 0; i < 10; i++)
            {
                if (spawnLocations.Count() <= 0)
                {
                    return;
                }
                var random = Random.Range(0, spawnLocations.Count() - 1);               
                Instantiate(enemy, spawnLocations[random], Quaternion.identity);
                spawnLocations.RemoveAt(random);
            }

        }

    }


}
