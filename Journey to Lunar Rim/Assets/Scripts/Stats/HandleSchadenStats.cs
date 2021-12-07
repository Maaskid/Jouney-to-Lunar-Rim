using UnityEngine;

namespace Stats
{
    public class HandleSchadenStats : MonoBehaviour
    {
        public PlayerStats playerStats;
        public GameEvent.GameEvent gameEventSchadenGenommen;

        public void Damage()
        {
            playerStats.SchadenRuntime += 10;
            gameEventSchadenGenommen.Raise();
        }

        private void OnMouseDown()
        {
            Damage();
        }
    }
}
