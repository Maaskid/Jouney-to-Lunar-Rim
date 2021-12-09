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

        void Start()
        {
            TankLeeren(3);
            Schaden();
        }

        public void TankLeeren(int counter)
        {
            rendererLeft.material = tankMaterials[counter];
        }

        public void Schaden()
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