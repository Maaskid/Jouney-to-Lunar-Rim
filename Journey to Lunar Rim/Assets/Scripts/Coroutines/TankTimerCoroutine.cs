using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

public class TankTimerCoroutine : MonoBehaviour
{

    private int _counter = 4;
    private IEnumerator _tankLeeren;
    
    IEnumerator TankLeeren()
    {
        while (true)
        {
            if (_counter < 0)
                break;
            GetComponent<DisplayPlayerStats>().Tank(_counter);
            _counter--;
            yield return new WaitForSeconds(GetComponent<DisplayPlayerStats>().playerStats.verbrauchsZeit); // wait for one second and then start over
        }
    }

    public void TankFuellen()
    {
        StopCoroutine(_tankLeeren);
        _counter = 4;
        StartCoroutine(_tankLeeren);
    }
    
    void Start()
    {
        _tankLeeren = TankLeeren();
        StartCoroutine(_tankLeeren);
    }
}
