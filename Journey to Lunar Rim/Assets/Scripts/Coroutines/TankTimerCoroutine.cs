using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

public class TankTimerCoroutine : MonoBehaviour
{
    // public GameObject objectToSpawn;
    // public Transform spawnPoint;
    // public float waitTime = 0.5f;

    private int _counter = 4;
    private IEnumerator _tankLeeren;
    
    IEnumerator TankLeeren()
    {
        while (true)
        {
            GetComponent<DisplayPlayerStats>().Tank(_counter);
            _counter--;
            yield return new WaitForSeconds(GetComponent<DisplayPlayerStats>().playerStats.verbrauchsZeit); // wait for one second and then start over
            if (_counter < 0)
            {
                break;
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _tankLeeren = TankLeeren();
        StartCoroutine(_tankLeeren);
    }
}
