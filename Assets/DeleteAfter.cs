using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfter : MonoBehaviour
{
    public float timeTillDelete;

    void Start()
    {
        Destroy(gameObject, timeTillDelete);
    }
}
