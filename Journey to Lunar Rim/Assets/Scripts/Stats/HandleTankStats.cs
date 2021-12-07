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

        public void TankLeeren(int current)
        {
            switch (current)
            {
                case 0:
                    playerStats.TankRuntime = playerStats.tankInit * 0f;
                    break;
                case 1:
                    playerStats.TankRuntime = playerStats.tankInit * .25f;
                    break;
                case 2:
                    playerStats.TankRuntime = playerStats.tankInit * .5f;
                    break;
                case 3:
                    playerStats.TankRuntime = playerStats.tankInit * 0.75f;
                    break;
                case 4:
                    playerStats.TankRuntime = playerStats.tankInit;
                    break;
            }
        }
        
        private void OnMouseDown()
        {
            TankFuellen();
        }
    }
}
