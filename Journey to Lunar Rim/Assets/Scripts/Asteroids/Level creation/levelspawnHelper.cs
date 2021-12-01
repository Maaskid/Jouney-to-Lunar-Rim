using UnityEngine;

public class levelspawnHelper : MonoBehaviour
{
    public GameObject asteroidField;
    private Vector3 nextSpawnpoint;

    public void SpawnField()
    {
        GameObject temp = Instantiate(asteroidField);
        nextSpawnpoint = temp.transform.GetChild(1).transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnField();
        SpawnField();
    }
}
