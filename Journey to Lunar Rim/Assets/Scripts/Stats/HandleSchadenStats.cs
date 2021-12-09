using Coroutines;
using UnityEngine;

namespace Stats
{
    public class HandleSchadenStats : MonoBehaviour
    {
        public PlayerStats playerStats;
        public GameEvent.GameEvent gameEventSchadenGenommen;

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
