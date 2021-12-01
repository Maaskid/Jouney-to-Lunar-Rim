using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaceAsteroids : MonoBehaviour
{
    public int x, y, z;
    public int offX, offY, offZ;
    public int numberOfAsteroids;

    public List<GameObject> asteroids;
    // Start is called before the first frame update
    void Start()
    {
        Place(x, y, z);
    }

    void Place(int x, int y, int z)
    {
        GameObject[] gameObjects = new GameObject[numberOfAsteroids];
        List<Vector3> positions = new List<Vector3>(numberOfAsteroids);

        for (int i = 0; i < numberOfAsteroids; i++)
        {
            gameObjects[i] = asteroids[Random.Range(0, asteroids.Count - 1)];
        } 
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            int xPos = Random.Range(0, x);
            int yPos = Random.Range(0, y);
            int zPos = Random.Range(0, z);
            for (int j = 0; j < i; j++)
            {
                int counter = 0;
                Vector3 colliderMax = gameObjects[i - 1].GetComponent<Collider>().bounds.max;
            
                while (!(xPos + colliderMax.magnitude < colliderMax.x && xPos - colliderMax.magnitude < colliderMax.x) && counter < 30)
                {
                    xPos = Random.Range(0, x);
                    counter++;
                }

                counter = 0;

                while (!(yPos + colliderMax.magnitude < colliderMax.y && yPos + colliderMax.magnitude < colliderMax.y) && counter < 30)
                {
                    yPos = Random.Range(0, y);
                    counter++;
                }

                counter = 0;
            
                while (!(zPos + colliderMax.magnitude < colliderMax.z && zPos + colliderMax.magnitude < colliderMax.z) && counter < 30)
                {
                    zPos = Random.Range(0, z);
                    counter++;
                }
            }
            positions.Add(new Vector3(xPos + offX, yPos + offY, zPos + offZ+ gameObject.transform.GetChild(0).position.z));
            Debug.Log(gameObject.transform.GetChild(0));
        }

        Vector3[] pos = positions.ToArray();

        for (int i = 0; i < numberOfAsteroids; i++)
        {
            Instantiate(gameObjects[i], pos[i], Random.rotation, transform);
        }
    }
}
