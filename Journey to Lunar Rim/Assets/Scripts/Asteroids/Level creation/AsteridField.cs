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
        asteroidSpawner = GetComponentInParent<AsteroidSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("WORKING!");
        String dir = other.tag switch
        {
            "ColliderRight" => "NextSpawnRight",
            "ColliderLeft" => "NextSpawnLeft",
            "ColliderTop" => "NextSpawnTop",
            "ColliderDown" => "NextSpawnDown",
            "ColliderBack" => "NextSpawnBack",
            "ColliderFront" => "NextSpawnFront",
            _ => "NONE"
        };
        asteroidSpawner.SpawnAsteroidField(dir);
        Destroy(gameObject, 2);
    }
}
