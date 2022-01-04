using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Stats
{
    public class DisplayPlayerStats : MonoBehaviour
    {
        //public PlayerStats playerStats;

        public Material[] tankMaterials;
        public Material[] schildMaterials;
        public Sprite[] dialogSprites;

        public Image freya, baker, dialogbox;
        
        public GameObject windowLeft;
        public GameObject windowMiddle;
        public GameObject windowRight;

        public MeshRenderer rendererLeft;
        public MeshRenderer rendererRight;
        public Material wlMaterial;
        public Material wmMaterial;
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
            float runtime = gameObject.GetComponent<PlayerController>().tank;
            float max = gameObject.GetComponent<PlayerController>().maxTank;
            
            // [.., 0] → Tank leer, TODO invoke GameOverscreen
            if (runtime <= 0)
            {
                Debug.Log("GameOver");
                rendererLeft.material = tankMaterials[0];
                ShowWarning(1); // show warning
            }
            // [0+, 25%] → Tank 1/4 = 1
            else if (0 < runtime && runtime <= max * 0.25) // max * 0 is always 0
            {
                rendererLeft.material = tankMaterials[1];
                ShowWarning(1); // show warning
            }
            // [25+%, 50%] → Tank 1/2 = 2
            else if (max * 0.25 < runtime && runtime <= max * 0.5)
            {
                rendererLeft.material = tankMaterials[2];
            }
            // [50+%, 75%] → Tank 3/4 = 3
            else if (max * 0.5 < runtime && runtime <= max * 0.75)
            {
                rendererLeft.material = tankMaterials[3];
            }
            // [75+%, 100%] → Tank 1 = 4
            else if (max * 0.75 < runtime && runtime <= max)
            {
                rendererLeft.material = tankMaterials[4];
            }
        }

        /**
         * Called if SchadenStats change to update their display.
         * Fetches schadenRuntime to decide which material to display.
         */
        public void DisplaySchadenStats()
        {
            int runtime = gameObject.GetComponent<PlayerController>().lives;

            Debug.Log("Lives: " + runtime);

            switch(runtime){
                case 0:
                    rendererRight.material = schildMaterials[0];
                    ShowWarning(1);
                break;
                case 1:
                    rendererRight.material = schildMaterials[1];
                    ShowWarning(1);
                break;
                case 2:
                    rendererRight.material = schildMaterials[2];
                    ShowWarning(0);
                break;
                case 3:
                    rendererRight.material = schildMaterials[3];
                    ShowWarning(0);
                break;
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
                    windowMiddle.GetComponent<MeshRenderer>().material = alphaMaterial;
                    windowRight.GetComponent<MeshRenderer>().material = alphaMaterial;
                    break;
                case 1: // show warning
                    windowLeft.GetComponent<MeshRenderer>().material = wlMaterial;
                    windowMiddle.GetComponent<MeshRenderer>().material = wmMaterial;
                    windowRight.GetComponent<MeshRenderer>().material = wrMaterial;
                    break;
                default:
                    windowLeft.GetComponent<MeshRenderer>().material = alphaMaterial;
                    windowMiddle.GetComponent<MeshRenderer>().material = alphaMaterial;
                    windowRight.GetComponent<MeshRenderer>().material = alphaMaterial;
                    break;
            }
        }
        
        public IEnumerator ShowDialogues(int level)
        {
            var isDone = false;
            var index = 0;
            while (!isDone)
            {
                yield return new WaitForSeconds(3f);
                index++;
                dialogbox.sprite = dialogSprites[index];

                if (index==dialogSprites.Length-1)
                {
                    isDone = true;
                }
            }
        } 
    }
}