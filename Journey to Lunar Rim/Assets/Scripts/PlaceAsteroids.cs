using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    [System.Serializable]
public class PlaceAsteroids : MonoBehaviour
{
    public int size;
    float xOff;
    float yOff;
    float zOff;
    public int numberOfAsteroids;
    [SerializeField, Range(0.1f, 0.5f)]
    public float collisionBoxSizeMod = 0.1f;

    public List<GameObject> asteroids;
    
    public PlaceAsteroids(int size, Vector3 position, int amountAsteroids, float collBoxMod){
        this.size = size;
        this.xOff = position.x;
        this.yOff = position.y;
        this.zOff = position.z;
        this.numberOfAsteroids = amountAsteroids;
        this.collisionBoxSizeMod = collBoxMod;
    }

    void Awake()
    {
        xOff = transform.position.x;
        yOff = transform.position.y;
        zOff = transform.position.z;
        Place(size);
    }

    //Erstellt und platziert die Meteoriten
    void Place(int size)
    {
        GameObject[] gameObjects = new GameObject[numberOfAsteroids];
        List<Vector3> positions = new List<Vector3>(numberOfAsteroids);

        for (int i = 0; i < numberOfAsteroids; i++)
        {
            gameObjects[i] = asteroids[Random.Range(0, asteroids.Count - 1)];
        } 
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            float xPos = Random.Range(0, size);
            float yPos = Random.Range(0, size);
            float zPos = Random.Range(0, size);
            for (int j = 0; j < i; j++)
            {
                int counter = 0;
                Vector3 colliderMax = gameObjects[i - 1].GetComponent<Collider>().bounds.max;
            
                while (!(xPos + colliderMax.magnitude < colliderMax.x && xPos - colliderMax.magnitude < colliderMax.x) && counter < 30)
                {
                    xPos = Random.Range(0, size);
                    counter++;
                }

                counter = 0;

                while (!(yPos + colliderMax.magnitude < colliderMax.y && yPos + colliderMax.magnitude < colliderMax.y) && counter < 30)
                {
                    yPos = Random.Range(0, size);
                    counter++;
                }

                counter = 0;
            
                while (!(zPos + colliderMax.magnitude < colliderMax.z && zPos + colliderMax.magnitude < colliderMax.z) && counter < 30)
                {
                    zPos = Random.Range(0, size);
                    counter++;
                }
                
            }
            positions.Add(new Vector3(xPos + xOff - size/2, yPos + yOff - size/2, zPos + zOff - size/2));
        }

        Vector3[] pos = positions.ToArray();

        for (int i = 0; i < numberOfAsteroids; i++)
        {
            Instantiate(gameObjects[i], pos[i], Random.rotation, this.transform);
        }
    }
}
