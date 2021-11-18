using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public int speed = 500;
    public int sidewaysspeed = 50;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(transform.localEulerAngles.z < 15 || transform.localEulerAngles.z > 345){
        }
        else{
            if(transform.localEulerAngles.z < 180){
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else{
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }
}
