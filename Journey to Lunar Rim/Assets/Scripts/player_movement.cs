using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    [Header("=== Ship Important Settings ===")]
    public GameObject playerCam;

    [Header("=== Ship Movement Settings ===")]
    public int speed = 500;
    public int sidewaysspeed = 50;

    [Header("=== Ship Values ===")]
    public int lives = 3;

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement(){
        //man rotiert den Vorwärts Vector (0,0,1) mit der Rotation der Kamera. Man verschiebt den Spieler entlang der Vectors
        Vector3 forwardVector = Quaternion.Euler(playerCam.transform.localRotation.eulerAngles) * Vector3.forward;
        transform.Translate(forwardVector * speed * Time.deltaTime);

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
                Debug.Log("IMPACT!!");
                lives = lives - 1;
        }
    }
}
