using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public GameObject playerCam;
    public int speed = 500;
    public int sidewaysspeed = 50;

    private bool dead = false;

    private float counter = 0;

    private float counter2 = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
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

        counter += Time.deltaTime;
        if (counter > 5)
        {
            dead = true;
            counter = 0;
        }

        if (dead)
        {
            counter2 += Time.deltaTime;
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            if (counter2 > 2)
               
            {
                dead = false;
                FindObjectOfType<AudioManager>().Stop("PlayerDeath");
                counter2 = 0;
            }
        }
    }
}
