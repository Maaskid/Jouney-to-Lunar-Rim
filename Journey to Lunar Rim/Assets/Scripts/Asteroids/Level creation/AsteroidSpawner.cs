using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidField;

    private Vector3 nextSpawnPoint;
    private List<GameObject> levelParts;
    public void SpawnAsteroidField()
    {
        GameObject temp = Instantiate(asteroidField, nextSpawnPoint, Quaternion.identity, transform);
        nextSpawnPoint = temp.transform.GetChild(0).transform.position;
        nextSpawnPoint.z += 200;
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnAsteroidField();   
        SpawnAsteroidField();   
        SpawnAsteroidField();   
    }
}
