using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidField;

    private Vector3 nextSpawnPoint;
    private List<GameObject> levelParts;
    public void SpawnAsteroidField(String dir)
    {
        GameObject temp = Instantiate(asteroidField, nextSpawnPoint, Quaternion.identity, transform);
        int index = dir switch
        {
            "NextSpawnRight" => 0,
            "NextSpawnLeft" => 1,
            "NextSpawnTop" => 2,
            "NextSpawnDown" => 3,
            "NextSpawnBack" => 4,
            "NextSpawnFront" => 5,
            _ => -1
        };
        nextSpawnPoint = temp.transform.GetChild(index).transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnAsteroidField("NextSpawnFront");
        }
    }
}
