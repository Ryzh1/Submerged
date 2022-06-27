using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlants : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject plantToSpawn;
    public GameObject PlantContainer;

    private void Start()
    {
        SpawnPlant();
    }

    void SpawnPlant()
    {
        Instantiate(plantToSpawn, spawnPoint.position, Quaternion.identity).transform.SetParent(PlantContainer.transform); ;
        Invoke("SpawnPlant", Random.Range(1f, 2f));
    }
}
