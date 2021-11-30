using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{

    public GameObject groundTile;
    public GameObject asteroids;

    private Vector3 nextSpawnPoint;
    private GameObject a;

    public void SpawnAsteroids()
    {
        a = Instantiate(asteroids, nextSpawnPoint, Quaternion.identity);
    }
    public void SpawnTile()
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnTile();
        SpawnTile();
        SpawnTile();
        SpawnTile();
        SpawnTile();
    }
}
