using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteridField : MonoBehaviour
{
    private AsteroidSpawner asteroidSpawner;
    // Start is called before the first frame update
    void Start()
    {
        asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        asteroidSpawner.SpawnAsteroidField();
        Destroy(gameObject, 2);
        Debug.Log("WORKING!");
    }
}
