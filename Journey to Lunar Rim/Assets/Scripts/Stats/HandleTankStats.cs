using UnityEngine;

namespace Stats
{
    public class HandleTankStats : MonoBehaviour
    {
        public PlayerStats playerStats;
        public GameEvent.GameEvent gameEventSpritVerbraucht;

        public void TankLeeren()
        {
            playerStats.TankRuntime -= 5;
            gameEventSpritVerbraucht.Raise();
        }

        private void OnMouseDown()
        {
            TankLeeren();
        }
    }
}
