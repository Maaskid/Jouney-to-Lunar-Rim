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
    void Awake()
    {
        theParent = transform.parent.parent.gameObject;
        size = theParent.GetComponent<PlaceAsteroids>().size;
        collisionBoxSizeMod = theParent.GetComponent<PlaceAsteroids>().collisionBoxSizeMod;
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
        BoxCollider coll = GetComponent<BoxCollider>();
        coll.enabled = true;
    }

    void disableCloseHitbox(Transform newArea){
        List<GameObject> areTooClose = new List<GameObject>();

        foreach(GameObject allAstCol1 in GameObject.FindGameObjectsWithTag("AsteroidCollision")){
                            
            BoxCollider col1 = allAstCol1.GetComponent<BoxCollider>();
            //Debug.Log("col1 ist: " + col1);
            BoxCollider col2 = null;
            
            foreach(GameObject allAstCol2 in GameObject.FindGameObjectsWithTag("AsteroidCollision")){
                
                if(allAstCol1 == allAstCol2){
                    Debug.Log("Da war was gleich!");
                }
                else if(allAstCol1.transform.parent.parent == allAstCol2.transform.parent.parent){
                    Debug.Log("Alabama!");
                }
                else{
                    
                    col2 = allAstCol2.GetComponent<BoxCollider>();
                    //Debug.Log("col2 ist: " + col2);

                    float distance = Vector3.Distance(allAstCol1.transform.position, allAstCol2.transform.position);
                    //Debug.Log("Distanz: " + distance + " Bei All1: + " + col1 + " und All2: " + col2);

                    if(distance <= size*collisionBoxSizeMod+1){
                        areTooClose.Add(allAstCol1);
                        areTooClose.Add(allAstCol2);
                        break;
                    }
                }
        
            }

            if(areTooClose.Contains(allAstCol1)){
                col1.enabled = false;
            }
            else{
                //col1.enabled = true;
            }
        }
        
        foreach(var x in areTooClose){
            Debug.Log(x.ToString());
        }

    }
    
    void OnTriggerEnter(Collider other){
        //Debug.Log("Is Drinne");

        Vector3 newPosition = theParent.transform.position;

        GameObject newAsteroids = Instantiate(theParent, new Vector3(0,0,0), theParent.transform.rotation);

        int numOfAst = theParent.GetComponent<PlaceAsteroids>().numberOfAsteroids;

        for(int i = 1; i <= numOfAst; i++){
            Destroy(newAsteroids.transform.GetChild(i).gameObject);
        }

        Transform allCollFromNew = newAsteroids.transform.GetChild(0);
        //BoxCollider specificCol = allCollFromNew.GetChild(0).GetComponent<BoxCollider>();

        //BoxCollider coll = GetComponent<BoxCollider>();
        //coll.enabled = false;

        switch(colName){
            case ColliderName.North:
            newPosition.z = newPosition.z + size;
            newAsteroids.transform.position = newPosition;
            //Süd Collider vom neuen AsteoridenObjekt
            //specificCol = allCollFromNew.GetChild(2).GetComponent<BoxCollider>();
            Debug.Log("Places North");
            break;

            case ColliderName.East:
            newPosition.x = newPosition.x + size;
            newAsteroids.transform.position = newPosition;
            //West Collider vom neuen AsteoridenObjekt
            //specificCol = allCollFromNew.GetChild(3).GetComponent<BoxCollider>();
            Debug.Log("Places East");
            break;

            case ColliderName.South:
            newPosition.z = newPosition.z - size;
            newAsteroids.transform.position = newPosition;
            //Nord Collider vom neuen AsteoridenObjekt
            //specificCol = allCollFromNew.GetChild(0).GetComponent<BoxCollider>();
            Debug.Log("Places South");
            break;

            case ColliderName.West:
            newPosition.x = newPosition.x - size;
            newAsteroids.transform.position = newPosition;
            //Ost Collider vom neuen AsteoridenObjekt
            //specificCol = allCollFromNew.GetChild(1).GetComponent<BoxCollider>();
            Debug.Log("Places West");
            break;

            case ColliderName.Top:
            newPosition.y = newPosition.y + size;
            newAsteroids.transform.position = newPosition;
            //Bot Collider vom neuen AsteoridenObjekt
            //specificCol = allCollFromNew.GetChild(5).GetComponent<BoxCollider>();
            Debug.Log("Places Top");
            break;

            case ColliderName.Bot:
            newPosition.y = newPosition.y - size;
            newAsteroids.transform.position = newPosition;
            //Top Collider vom neuen AsteoridenObjekt
            //specificCol = allCollFromNew.GetChild(4).GetComponent<BoxCollider>();
            Debug.Log("Places Bot");
            break;
        }
        
        disableCloseHitbox(allCollFromNew);
        //Debug.Log("Places at: " + newPosition.ToString());
        //specificCol.enabled = false;
    }
}
