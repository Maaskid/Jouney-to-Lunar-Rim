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

    public int spawnAngle = 45;
    public float spawnFrequence = 3;
    float spawnTimer = 0;

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

        spawnTimer += Time.deltaTime;

        if(spawnTimer < spawnFrequence){

            if(GameObject.FindGameObjectsWithTag("Rock").Length <= numberOfAsteroids){
                //Debug.Log(GameObject.FindGameObjectsWithTag("Rock").Length);
                SpawnNewAsteroids();
            }
        
        DestroyFarAsteroids();

        spawnTimer = 0;
        }
    }

    //Erstellt und platziert die Meteoriten
    void Place(int size)
    {
        GameObject[] gameObjects = new GameObject[numberOfAsteroids];
        List<Vector3> positions = new List<Vector3>(numberOfAsteroids);

        for (int i = 0; i < numberOfAsteroids; i++)
        {
            gameObjects[i] = asteroids[Random.Range(0, asteroids.Count)];
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

        //Damuss man die Rotation der Camera nehmen
        Vector3 playerForward = player.transform.GetChild(0).transform.forward;
        Vector3 randomRotation = new Vector3(Random.Range(-spawnAngle , spawnAngle), Random.Range(-spawnAngle , spawnAngle), Random.Range(-spawnAngle , spawnAngle));

        Vector3 newDirection = playerForward.normalized + randomRotation.normalized;

        //Debug.Log("Direction: " + newDirection.ToString());
        //Debug.Log("Forward: " + player.transform.GetChild(0).transform.forward);

        Vector3 targetPosition = transform.position + (newDirection.normalized * radius);

        //Debug.Log("transform.position: " + transform.position);

        Instantiate(newAsteroid, targetPosition, Random.rotation);
        //Debug.Log("Spawnt neu an: " + targetPosition.ToString());

    }
}
