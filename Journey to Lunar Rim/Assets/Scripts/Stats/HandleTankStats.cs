using UnityEngine;

namespace Stats
{
    public class HandleTankStats : MonoBehaviour
    {
        public PlayerStats playerStats;
        public GameEvent.GameEvent gameEventTankFuellen;

        public void TankFuellen()
        {
            playerStats.TankRuntime = playerStats.tankInit;
            gameEventTankFuellen.Raise();
        }

        private void OnMouseDown()
        {
            TankFuellen();
        }
    }
}
