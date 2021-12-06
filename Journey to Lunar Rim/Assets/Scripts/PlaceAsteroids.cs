using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        xOff = transform.position.x;
        yOff = transform.position.y;
        zOff = transform.position.z;
        Place(size);
        CreateTriggerBox(size);
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

    //Erstellt, skaliert und platziert die Rand Kollider
    void CreateTriggerBox(int size){
        //Create Colliders
        BoxCollider norCol = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        BoxCollider easCol = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        BoxCollider souCol = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        BoxCollider wesCol = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        BoxCollider topCol = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        BoxCollider botCol = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;

        norCol.isTrigger = true;
        souCol.isTrigger = true;
        easCol.isTrigger = true;
        wesCol.isTrigger = true;
        topCol.isTrigger = true;
        botCol.isTrigger = true;

        //Set specific Size for the colliders
        //norden und süden, muss größe z kleiner sein
        norCol.size = new Vector3(size, size, size*collisionBoxSizeMod);
        souCol.size = new Vector3(size, size, size*collisionBoxSizeMod);
        //osten und westen muss x kleiner sein
        easCol.size = new Vector3(size*collisionBoxSizeMod, size, size);
        wesCol.size = new Vector3(size*collisionBoxSizeMod, size, size);
        //oben und unten muss y kleiner sein
        topCol.size = new Vector3(size, size*collisionBoxSizeMod, size);
        botCol.size = new Vector3(size, size*collisionBoxSizeMod, size);

        //Setzt Position der Collider
        norCol.center = new Vector3(0, 0, ( (size/2) - (size*collisionBoxSizeMod)/2 ) );
        souCol.center = new Vector3(0, 0, -( (size/2) - (size*collisionBoxSizeMod)/2 ));
        easCol.center = new Vector3(( (size/2) - (size*collisionBoxSizeMod)/2 ), 0, 0);
        wesCol.center = new Vector3(-( (size/2) - (size*collisionBoxSizeMod)/2 ), 0, 0);
        topCol.center = new Vector3(0, ( (size/2) - (size*collisionBoxSizeMod)/2 ), 0);
        botCol.center = new Vector3(0, -( (size/2) - (size*collisionBoxSizeMod)/2 ), 0);
    }

    void OnTriggerEnter(Collider col){
        Debug.Log(col);
    }
}
