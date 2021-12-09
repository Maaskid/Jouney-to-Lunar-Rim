using Coroutines;
using UnityEngine;

namespace Stats
{
    public class HandleSchadenStats : MonoBehaviour
    {
        public PlayerStats playerStats;
        public GameEvent.GameEvent gameEventSchadenGenommen;

        /**
         * Called by OnMouseDown over Quad to damage ship.
         * TODO change to OnCollisionEnter on Asteroid.
         *
         * Adds a damage value of 10, if damage greater than 100 put it back to 100 to avoid crashing upper boundary.
         * If damage occured start SchadenReparieren coroutine in TimedCoroutines.
         */
        public void SchadenErhoehen()
        {
            if (playerStats.SchadenRuntime >= 100)
            {
                playerStats.SchadenRuntime = 100;
            }
            else
            {
                playerStats.SchadenRuntime += 10;
            }
            GetComponentInParent<TimedCoroutines>().SchadenReparierenStarten();
            gameEventSchadenGenommen.Raise();
        }

        /**
         * Called in intervals by SchadenReparierenCoroutine.
         * Lowers the damage by 25% of max allowed damage.
         * If damage is less than 0 set it back to zero to avoid crashing lower boundary.
         */
        public void SchadenReparieren()
        {
            playerStats.SchadenRuntime -= playerStats.schadenMax * 0.25f;
            if (playerStats.SchadenRuntime < 0)
            {
                playerStats.SchadenRuntime = 0;
            }
        }
        
        private void OnMouseDown()
        {
            SchadenErhoehen();
        }
    }
}
