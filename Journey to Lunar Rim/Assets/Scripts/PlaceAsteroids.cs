using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    [System.Serializable]
public class PlaceAsteroids : MonoBehaviour
{

    public GameObject player;

    public int radius;
    public int numberOfAsteroids;

    public List<GameObject> asteroids;

    SphereCollider s_collider;

    void Start()
    {
        s_collider = gameObject.AddComponent<SphereCollider>();
        s_collider.radius = radius;
        s_collider.isTrigger = true;
        Place(radius*2);
        DestroyFarAsteroids();
    }

    void Update(){
        this.transform.position = player.transform.position;

        SpawnNewAsteroids();
        
        DestroyFarAsteroids();
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
            positions.Add(new Vector3(xPos + this.transform.position.x - radius, yPos + this.transform.position.y - radius, zPos + this.transform.position.z - radius));
        }

        Vector3[] pos = positions.ToArray();

        for (int i = 0; i < numberOfAsteroids; i++)
        {
            GameObject newAsteroid = Instantiate(gameObjects[i], pos[i], Random.rotation);
        }
    }

    void DestroyFarAsteroids(){
        foreach(GameObject asteroid in GameObject.FindGameObjectsWithTag("Rock")){
            float distance = Vector3.Distance(player.transform.position, asteroid.transform.position);
            if(distance > radius){
                Destroy(asteroid.gameObject);
                //Debug.Log("Destroyed because Distance: "+ distance);
            }
        }
        foreach(GameObject shield in GameObject.FindGameObjectsWithTag("Shield")){
            float distance = Vector3.Distance(player.transform.position, shield.transform.position);
            if(distance > radius){
                Destroy(shield.gameObject);
                //Debug.Log("Destroyed because Distance: "+ distance);
            }
        }
        foreach(GameObject boost in GameObject.FindGameObjectsWithTag("Boost")){
            float distance = Vector3.Distance(player.transform.position, boost.transform.position);
            if(distance > radius){
                Destroy(boost.gameObject);
                //Debug.Log("Destroyed because Distance: "+ distance);
            }
        }
        foreach(GameObject fuel in GameObject.FindGameObjectsWithTag("Fuel")){
            float distance = Vector3.Distance(player.transform.position, fuel.transform.position);
            if(distance > radius){
                Destroy(fuel.gameObject);
                //Debug.Log("Destroyed because Distance: "+ distance);
            }
        }
    }

    void SpawnNewAsteroids(){

        GameObject newAsteroid = asteroids[Random.Range(0, asteroids.Count - 1)];

        Vector3 rayDirection = player.transform.GetChild(0).rotation.eulerAngles;

        //Debug.Log("Direction: " + rayDirection.ToString());

        Ray r = new Ray(transform.position, rayDirection);

        //Debug.Log("transform.position: " + transform.position);

        //Instantiate(newAsteroid, r.GetPoint(radius - 1), Random.rotation);
        Debug.Log("Spawnt neu an: " + r.GetPoint(500000000));
        Debug.DrawRay(transform.position, rayDirection);

    }
}
