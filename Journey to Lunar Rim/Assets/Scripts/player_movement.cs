using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    [Header("=== Ship Important Settings ===")]
    public GameObject playerCam;

    [Header("=== Ship Movement Settings ===")]
    public float speed = 5;
    public float sidewaysspeed = 5;
    public float shipSpeed = 0;

    [Header("=== Ship Values ===")]
    public int lives = 3;

    [Header("=== Collision ==")]
    public ParticleSystem rockExplosion;
    [SerializeField, Range(0.8f, 0.999f)]
    public float collisionAccele = 0.5f;
    [SerializeField, Range(1f, 5f)]
    public float collisionSpeed = 2f;

    public bool collisionState = false;

    void FixedUpdate()
    {
        HandleMovement();
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

        if(lives <= 0){
            speed = 0;
        }
    }

    void OnCollisionEnter(Collision collision){
            Debug.Log(collision.collider.name);
            if(collision.collider.tag == "Rock"){
                Instantiate(rockExplosion, collision.collider.gameObject.transform.position, collision.collider.gameObject.transform.rotation);
                
                Destroy(collision.collider.gameObject);

                collisionState = true;

                Debug.Log("IMPACT!!");
                lives = lives - 1;
        }
    }

}
