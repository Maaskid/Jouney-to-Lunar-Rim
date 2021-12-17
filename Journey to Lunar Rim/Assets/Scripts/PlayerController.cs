using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("=== Ship Important Settings ===")]
    public GameObject playerCam;

    [Header("=== Ship Movement Settings ===")]
    public float speed = 5;
    public float sidewaysspeed = 5;
    public float shipSpeed = 0;

    [Header("=== Ship Values ===")]
    public int lives = 3;
    public float tank = 500;
    public float maxTank;
    [SerializeField, Range(0.5f, 5f)]
    public float tankConsumption = 0.5f;


    [Header("=== Collision ==")]
    public ParticleSystem rockExplosion;
    [SerializeField, Range(0.8f, 0.999f)]
    public float collisionAccele = 0.5f;
    [SerializeField, Range(1f, 5f)]
    public float collisionSpeed = 2f;

    public bool collisionState = false;

    void Start(){
        maxTank = tank;
    }

    void FixedUpdate()
    {
        HandleMovement();
        GameLost();
    }

    Vector3 GetDirection(){
        Vector3 direction;

        if (collisionState == false){
            direction = Vector3.forward;
        }
        else{
            direction = Vector3.back;
        }

        //man rotiert den Vorwärts Vector (0,0,1) mit der Rotation der Kamera. Man verschiebt den Spieler entlang der Vectors
        Vector3 forwardVector = Quaternion.Euler(playerCam.transform.localRotation.eulerAngles) * direction; //Vector3.forward oder Vector3.back

        return forwardVector;
    }

    float GetSpeed(){
        if (collisionState == false){
            shipSpeed = speed;
            SetFuel();
        }
        else{
            shipSpeed = shipSpeed * collisionAccele;
        }

        if(shipSpeed <= collisionSpeed){
            shipSpeed = speed;
            collisionState = false;
            Debug.Log("Reduce Speed");
        }

        return shipSpeed;
    }

    void HandleMovement(){

        transform.Translate(GetDirection() * GetSpeed() * Time.deltaTime);

        //Schwellenwert wann man nach links oder rechts bewegt. Innerhalb 15° nach links oder rechts geneigt passiert nichts. (Man nimmt die rotation der Kamera, nicht des Spielers)
        if(playerCam.transform.localRotation.eulerAngles.z < 15 || playerCam.transform.localRotation.eulerAngles.z > 345){
        }
        //wird über 15° geneigt wird nur geprüft, ob man den Kopf nach links oder rechts neigt und bewegt dann den Spieler
        else{
            if(playerCam.transform.localRotation.eulerAngles.z < 180){
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else{
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }

    void SetFuel(){
        if(tank > 0){
            tank -= tankConsumption;
        }
        else{
            tank = 0;
        }
        
        GetComponent<DisplayPlayerStats>().DisplayTankStats(tank, maxTank);
    }

    void GameLost(){
        if(lives == 0){
            speed = 0;
            Debug.Log("GAME LOST!!");
            return;
        }
        if(tank == 0){
            speed = 0;
            Debug.Log("GAME LOST!!");
            return;
        }
        return;
    }

    void OnCollisionEnter(Collision collision){
            //Debug.Log(collision.collider.name);
            if(collision.collider.tag == "Rock"){
                ParticleSystem part = Instantiate(rockExplosion, collision.collider.gameObject.transform.position, collision.collider.gameObject.transform.rotation);
                part.Play();

                Destroy(collision.collider.gameObject);

                collisionState = true;

                Debug.Log("IMPACT!!");
                lives = lives - 1;
                
                GetComponent<DisplayPlayerStats>().DisplaySchadenStats(lives);
        }
    }

}
