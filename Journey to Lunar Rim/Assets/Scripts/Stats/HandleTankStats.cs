using UnityEngine;

namespace Stats
{
    public class HandleTankStats : MonoBehaviour
    {
        public PlayerStats playerStats;
        public GameEvent.GameEvent gameEventTankFuellen;

        /**
         * Called by OnMouseDown over Quad to fill tank up.
         * TODO change to OnCollisionEnter on TankGameObject.
         */
        public void TankFuellen()
        {
            playerStats.TankRuntime = playerStats.tankInit;
            //GetComponentInParent<DisplayPlayerStats>().DisplayTankStats();
            gameEventTankFuellen.Raise();
        }

        /**
         * Called in intervals by the TankLeeren Coroutine.
         * Subtracts 25% of max/initial filling or sets the tank on empty if lower boundary is crashed.
         */
        public void TankLeeren()
        {
            playerStats.TankRuntime -= playerStats.tankInit * .25f;

            if (playerStats.TankRuntime < 0)
            {
                playerStats.TankRuntime = 0;
            }
        }
        
        private void OnMouseDown()
        {
            TankFuellen();
        }
    }
}
