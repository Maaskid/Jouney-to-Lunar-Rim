using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

namespace Coroutines
{
    public class TimedCoroutines : MonoBehaviour
    {
        
        private int _tankCounter = 3;

        private Coroutine _tankLeeren;
        private Coroutine _schadenReparieren;

        void Start()
        {
            _tankLeeren = StartCoroutine(TankLeeren());
        }
        
        IEnumerator TankLeeren()
        {
            while (true)
            {
                if (_tankCounter < 0)
                    break;
                
                GetComponent<DisplayPlayerStats>().TankLeeren(_tankCounter);
                GetComponentInChildren<HandleTankStats>().TankLeeren(_tankCounter);
                _tankCounter--;
                yield return
                    new WaitForSeconds(GetComponent<DisplayPlayerStats>().playerStats.verbrauchsZeit);
            }
        }

        public void TankFuellen()
        {
            StopCoroutine(_tankLeeren);
            _tankCounter = 3;
            _tankLeeren = StartCoroutine(TankLeeren());
        }

        public void SchadenReparierenStarten()
        {
            if (_schadenReparieren == null)
                _schadenReparieren = StartCoroutine(SchadenReparieren());
        }
        
        IEnumerator SchadenReparieren()
        {
            while (true)
            {
                if (GetComponent<DisplayPlayerStats>().playerStats.SchadenRuntime <= 0)
                    break;
                GetComponentInChildren<HandleSchadenStats>().SchadenReparieren();
                GetComponent<DisplayPlayerStats>().Schaden();
                
                yield return
                    new WaitForSeconds(GetComponent<DisplayPlayerStats>().playerStats.selbstReparaturZeit);
            }
        }
    }
}
