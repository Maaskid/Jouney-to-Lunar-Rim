using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInSpace : MonoBehaviour
{
    Vector3 targetRotation;
    void Start(){
        targetRotation = new Vector3(Random.Range(2, 30), Random.Range(2, 30), Random.Range(2, 30));
    }
    
    void Update(){
        transform.Rotate(targetRotation*Time.deltaTime);    
    }
}
