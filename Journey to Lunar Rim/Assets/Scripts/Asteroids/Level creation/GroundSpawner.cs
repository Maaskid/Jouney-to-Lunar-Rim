using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{

    public GameObject groundTile;

    private Vector3 nextSpawnPoint;
    public void SpawnTile()
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity, transform);
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
