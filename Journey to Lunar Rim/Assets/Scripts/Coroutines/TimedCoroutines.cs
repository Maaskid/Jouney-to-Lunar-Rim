using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

namespace Coroutines
{
    public class TimedCoroutines : MonoBehaviour
    {
/*        private Coroutine _tankLeeren;
        private Coroutine _schadenReparieren;*/

        
        //___TANK_______________________________________________________________________________________________________
        /**
         * Starts TankLeeren coroutine immediately when scene is loaded
         */
 /*       void Start()
        {
            _tankLeeren = StartCoroutine(TankLeerenCoroutine());
        }
        
        /**
         * Waits verbrauchsZeit, then continues to empty the tank and display changes. Breaks once tank is empty.
         */
/*        IEnumerator TankLeerenCoroutine()
        {
            while (true)
            {
                if (GetComponent<DisplayPlayerStats>().playerStats.TankRuntime <= 0)
                    break;

                yield return
                    new WaitForSeconds(GetComponent<DisplayPlayerStats>().playerStats.verbrauchsZeit);
                GetComponentInChildren<HandleTankStats>().TankLeeren();
                GetComponent<DisplayPlayerStats>().DisplayTankStats();
            }
        }

        /**
         * Restarts coroutine if tank is refilled,
         * Currently via Quad and onMouseDown TODO change to Asteroid and onCollisionEnter
         */
/*        public void RestartTankCoroutine()
        {
            StopCoroutine(_tankLeeren);
            _tankLeeren = StartCoroutine(TankLeerenCoroutine());
        }

        //___SCHADEN____________________________________________________________________________________________________
        /**
         * Called if ship is damaged TODO insert detection if damage is fully repaired and stop coroutine
         */
/*        public void SchadenReparierenStarten()
        {
            if (_schadenReparieren == null)
                _schadenReparieren = StartCoroutine(SchadenReparierenCoroutine());
        }
        
        /**
         * Repairs damage and displays result. Waits selbstReparaturZeit
         */
 /*       IEnumerator SchadenReparierenCoroutine()
        {
            while (true)
            {
                if (GetComponent<DisplayPlayerStats>().playerStats.SchadenRuntime <= 0)
                    break;
                
                GetComponentInChildren<HandleSchadenStats>().SchadenReparieren();
                GetComponent<DisplayPlayerStats>().DisplaySchadenStats();
                
                yield return
                    new WaitForSeconds(GetComponent<DisplayPlayerStats>().playerStats.selbstReparaturZeit);
            }
        }
    */}
}
