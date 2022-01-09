using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using ScriptableObjects.Scripts;
using Stats;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("=== Ship Important Settings ===")]
    public GameObject playerCam;
    public bool isEnd;
    
    [Header("=== Ship Movement Settings ===")]
    public float speed = 5;
    public float sidewaysspeed = 5;
    public float shipSpeed = 0;

    [Header("=== Ship Values ===")]
    public int lives = 3;
    int maxLives;
    public float tank = 500;
    public float maxTank;
    [SerializeField, Range(0f, 5f)]
    public float tankConsumption = 0.5f;

    [Header("=== Power Ups ===")]
    public int lifeGet = 1;
    float boostGet;
    public int boostDuration = 5;
    float boostTimer;
    bool isBoosting = false;


    [Header("=== Collision ==")]
    public ParticleSystem rockExplosion;
    [SerializeField, Range(0.8f, 0.999f)]
    public float collisionAccele = 0.5f;
    [SerializeField, Range(1f, 5f)]
    public float collisionSpeed = 2f;

    [Header("=== Other Stuff ===")]
    public GameObject endScreen;
    
    public bool collisionState;
    public bool beginJourney;
    public LoadingProgress loadingProgress;
    private AudioManager _audioManager;


    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    void Start(){
        maxTank = tank;
        maxLives = lives;
        boostGet = speed * 2;
    }

    void FixedUpdate()
    {
        if (!loadingProgress.isDone || loadingProgress.playStartSequence || loadingProgress.dead || loadingProgress.missionAccomplished)
            return;
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
        if (!isEnd)
        {
            if(collisionState){

                shipSpeed = shipSpeed * collisionAccele;
            
                if(shipSpeed <= collisionSpeed){
                    shipSpeed = speed;
                    collisionState = false;
                    //Debug.Log("Reduce Speed");
                }
            }
            else if(isBoosting){
                boostTimer += Time.deltaTime;

                if(boostTimer < boostDuration){
                    shipSpeed = boostGet;
                }
                else{
                    isBoosting = false;
                    boostTimer = 0;
                }
            }
            else{
                shipSpeed = speed;
                SetFuel();
            }   
        }
        else
        {
            shipSpeed = 0;
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
        GetComponent<DisplayPlayerStats>().DisplayTankStats();
    }

    void GameLost()
    {
        if (lives != 0 && tank != 0) return;
        
        speed = 0;
        // Debug.Log("GAME LOST!!");
        loadingProgress.dead = true;
        GetComponent<DisplayPlayerStats>().StartCoroutine(GetComponent<DisplayPlayerStats>().DeathSequence());
    }

    void OnCollisionEnter(Collision collision){
        Debug.Log("Kollidiert");
            Debug.Log(collision.collider.name);
            if(collision.collider.tag == "Rock"){
                _audioManager.PlayOneShot(SoundNames.MeteorCrash.ToString());
                ParticleSystem part = Instantiate(rockExplosion, collision.collider.gameObject.transform.position, collision.collider.gameObject.transform.rotation);
                part.Play();

                Destroy(collision.collider.gameObject);

                collisionState = true;

                Debug.Log("IMPACT!!");
                lives = lives - 1;
                
                GetComponent<DisplayPlayerStats>().DisplaySchadenStats();
            }

            if(collision.collider.tag == "Boost"){
                _audioManager.PlayOneShot(SoundNames.Warp.ToString());
                isBoosting = true;
                Destroy(collision.collider.gameObject);
            }

            if(collision.collider.tag == "Fuel"){
                _audioManager.PlayOneShot(SoundNames.ContainerCollected2.ToString());
                tank = maxTank;
                GetComponent<DisplayPlayerStats>().DisplayTankStats();
                Destroy(collision.collider.gameObject);
            }

            if(collision.collider.tag == "Shield"){
                _audioManager.PlayOneShot(SoundNames.ContainerCollected2.ToString());
                if(lives < maxLives){
                    lives += lifeGet;
                }
                GetComponent<DisplayPlayerStats>().DisplaySchadenStats();
                Destroy(collision.collider.gameObject);
            }

            if (collision.collider.tag.Equals("Artifact"))
            {
                if (_audioManager.GetSource(SoundNames.Warning).isPlaying)
                    _audioManager.Stop(SoundNames.Warning.ToString());

                _audioManager.PlayOneShot(SoundNames.ContainerCollected1.ToString());
                Debug.Log("Artifact eingesammelt");
                // Destroy(collision.collider.gameObject);
                collision.collider.enabled = false;
                collision.collider.transform.position = transform.GetChild(1).transform.position;
                isEnd = true;
                loadingProgress.missionAccomplished = true;
                GetComponent<DisplayPlayerStats>().StartCoroutine(GetComponent<DisplayPlayerStats>().ShowDialogues());
            }
    }

}
