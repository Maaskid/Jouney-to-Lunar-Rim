using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsColliders : MonoBehaviour
{
    public GameObject theParent;
    public enum ColliderName {North, East, South, West, Top, Bot}
    public ColliderName colName;
    int size;
    float collisionBoxSizeMod;

    // Start is called before the first frame update
    void Start()
    {
        SetTriggerBox(size, colName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Erstellt, skaliert und platziert die Rand Kollider
    void SetTriggerBox(int size, ColliderName colName){
        //Debug.Log("Hier colName:" + colName);
        
        //Set specific Size for the colliders
        //norden und süden, muss größe z kleiner sein
        //osten und westen muss x kleiner sein
        //oben und unten muss y kleiner sein

        //Setzt Position der Collider
        switch(colName){
        case ColliderName.North:
        transform.localScale = new Vector3(size, size, size*collisionBoxSizeMod);
        transform.position = new Vector3(0, 0, ( (size/2) - (size*collisionBoxSizeMod)/2 ) );
        //Debug.Log("Set North");
        break;

        case ColliderName.East:
        transform.localScale = new Vector3(size*collisionBoxSizeMod, size, size);
        transform.position = new Vector3(( (size/2) - (size*collisionBoxSizeMod)/2 ), 0, 0);
        //Debug.Log("Set East");
        break;

        case ColliderName.South:
        transform.localScale = new Vector3(size, size, size*collisionBoxSizeMod);
        transform.position = new Vector3(0, 0, -( (size/2) - (size*collisionBoxSizeMod)/2 ));
        //Debug.Log("Set South");
        break;

        case ColliderName.West:
        transform.localScale = new Vector3(size*collisionBoxSizeMod, size, size);
        transform.position = new Vector3(-( (size/2) - (size*collisionBoxSizeMod)/2 ), 0, 0);
        //Debug.Log("Set West");
        break;

        case ColliderName.Top:
        transform.localScale = new Vector3(size, size*collisionBoxSizeMod, size);
        transform.position = new Vector3(0, ( (size/2) - (size*collisionBoxSizeMod)/2 ), 0);
        //Debug.Log("Set Top");
        break;

        case ColliderName.Bot:
        transform.localScale = new Vector3(size, size*collisionBoxSizeMod, size);
        transform.position = new Vector3(0, -( (size/2) - (size*collisionBoxSizeMod)/2 ), 0);
        //Debug.Log("Set Bot");
        break;
        }

    }
    
    void OnTriggerEnter(Collider other){
        Debug.Log("Is Drinne");

        //public PlaceAsteroids newField = new PlaceAsteroids(size, new Vector3(100, 100, 100), 100, 0.2);

        switch(colName){
            case ColliderName.North:
            Debug.Log("Places North");
            break;

            case ColliderName.East:
            Debug.Log("Places East");
            break;

            case ColliderName.South:
            Debug.Log("Places South");
            break;

            case ColliderName.West:
            Debug.Log("Places West");
            break;

            case ColliderName.Top:
            Debug.Log("Places Top");
            break;

            case ColliderName.Bot:
            Debug.Log("Places Bot");
            break;
        }
    }
}
