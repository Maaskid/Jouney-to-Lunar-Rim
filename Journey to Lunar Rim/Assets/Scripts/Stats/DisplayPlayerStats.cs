using UnityEngine;

namespace Stats
{
    public class DisplayPlayerStats : MonoBehaviour
    {
        public PlayerStats playerStats;

        public Material[] tankMaterials;
        public Material[] schildMaterials;
        
        public GameObject windowRight;
        public GameObject windowLeft;

        public MeshRenderer rendererLeft;
        public MeshRenderer rendererRight;
        public Material wlMaterial;
        public Material wrMaterial;
        public Material alphaMaterial;

        /**
         * Displays both, Tank and Schaden stats, once in the beginning
         */
        void Start()
        {
            DisplayTankStats();
            DisplaySchadenStats();
        }

        /**
         * Called if TankStats change to update their display.
         * Fetches tankRuntime to decide which material to display.
         */
        public void DisplayTankStats()
        {
            float runtime = playerStats.TankRuntime;
            float max = playerStats.tankInit;
            
            // [.., 0] → Tank leer, TODO invoke GameOverscreen
            if (runtime <= 0)
            {
                Debug.Log("GameOver");
            }
            // [0+, 25%] → Tank 1/4 = 1
            else if (0 < runtime && runtime <= max * 0.25) // max * 0 is always 0
            {
                rendererLeft.material = tankMaterials[0];
            }
            // [25+%, 50%] → Tank 1/2 = 2
            else if (max * 0.25 < runtime && runtime <= max * 0.5)
            {
                rendererLeft.material = tankMaterials[1];
            }
            // [50+%, 75%] → Tank 3/4 = 3
            else if (max * 0.5 < runtime && runtime <= max * 0.75)
            {
                rendererLeft.material = tankMaterials[2];
            }
            // [75+%, 100%] → Tank 1 = 4
            else if (max * 0.75 < runtime && runtime <= max)
            {
                rendererLeft.material = tankMaterials[3];
            }
        }

        /**
         * Called if SchadenStats change to update their display.
         * Fetches schadenRuntime to decide which material to display.
         */
        public void DisplaySchadenStats()
        {
            float runtime = playerStats.SchadenRuntime;
            float max = playerStats.schadenMax;

            // [0, 33%] → Schild volle Stärke = 3
            if (0 <= runtime && runtime < max * 0.33) // max * 0 is always 0
            {
                rendererRight.material = schildMaterials[3];
                ShowWarning(0); // hide warning
            }
            // [33+%, 66%]
            else if (max * 0.33 <= runtime && runtime < max * 0.66)
            {
                rendererRight.material = schildMaterials[2];
                ShowWarning(0); // hide warning
            }
            // [66+%, 100%]
            else if (max * 0.66 <= runtime && runtime < max)
            {
                rendererRight.material = schildMaterials[1];
                ShowWarning(1); // show warning
            }
            // [100+%, ..] → Schaden gravierend = 0, TODO Leben abziehen
            else if (max <= runtime)
            {
                rendererRight.material = schildMaterials[0];
                ShowWarning(1); // show warning
            }
        }

        /**
         * Extra function for better readability within calling functions.
         * Either shows or hides warning on the windows, depenting on the current damage.
         */
        private void ShowWarning(int decision)
        {
            switch (decision)
            {
                case 0: // hide warning
                    windowLeft.GetComponent<MeshRenderer>().material = alphaMaterial;
                    windowRight.GetComponent<MeshRenderer>().material = alphaMaterial;
                    break;
                case 1: // show warning
                    windowLeft.GetComponent<MeshRenderer>().material = wlMaterial;
                    windowRight.GetComponent<MeshRenderer>().material = wrMaterial;
                    break;
                default:
                    windowLeft.GetComponent<MeshRenderer>().material = alphaMaterial;
                    windowRight.GetComponent<MeshRenderer>().material = alphaMaterial;
                    break;
            }
        }
    }
}