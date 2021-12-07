using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stats
{
    public class DisplayPlayerStats : MonoBehaviour
    {
        public PlayerStats playerStats;

        public Material[] tankMaterials;
        public GameObject windowRight;
        public GameObject windowLeft;

        public MeshRenderer rendererLeft;
        public MeshRenderer rendererRight;
        public Material wlMaterial;
        public Material wrMaterial;
        public Material alphaMaterial;

        void Start()
        {
            TankLeeren(4);
            // UpdateSchadensanzeige();
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

            // [.., 0]
            if (runtime <= 0) // max * 0 is always 0
            {
                rendererRight.material = tankMaterials[0];
            }
            // [0+%, 25%]
            else if (0 < runtime && runtime <= max * 0.25)
            {
                rendererRight.material = tankMaterials[1];
            }
            // [25+%, 50%]
            else if (max * 0.25 < runtime && runtime <= max * 0.5)
            {
                rendererRight.material = tankMaterials[2];
            }
            // [50+%, 75%]
            else if (max * 0.5 < runtime && runtime <= max * 0.75)
            {
                rendererRight.material = tankMaterials[3];
                windowLeft.GetComponent<MeshRenderer>().material = wlMaterial;
                windowRight.GetComponent<MeshRenderer>().material = wrMaterial;
            }
            // [75+%, 100%]
            else if (max * 0.75 < runtime && runtime <= max)
            {
                rendererRight.material = tankMaterials[4];
                windowLeft.GetComponent<MeshRenderer>().material = wlMaterial;
                windowRight.GetComponent<MeshRenderer>().material = wrMaterial;
            }
        }
        
        /*public void UpdateSchadensanzeige()
        {
            float runtime = playerStats.SchadenRuntime;
            float max = playerStats.schadenMax;

            // [.., 0]
            if (runtime <= 0) // max * 0 is always 0
            {
                GetComponentsInChildren<MeshRenderer>()[0].material = tankMaterials[0];
                rendererRight.material = tankMaterials[0];
            }
            // [0+%, 25%]
            else if (0 < runtime && runtime <= max * 0.25)
            {
                GetComponentsInChildren<MeshRenderer>()[0].material = tankMaterials[1];
                rendererRight.material = tankMaterials[1];
            }
            // [25+%, 50%]
            else if (max * 0.25 < runtime && runtime <= max * 0.5)
            {
                GetComponentsInChildren<MeshRenderer>()[0].material = tankMaterials[2];
                rendererRight.material = tankMaterials[2];
            }
            // [50+%, 75%]
            else if (max * 0.5 < runtime && runtime <= max * 0.75)
            {
                GetComponentsInChildren<MeshRenderer>()[0].material = tankMaterials[3];
                rendererRight.material = tankMaterials[3];
                windowLeft.GetComponent<MeshRenderer>().material = wlMaterial;
                windowRight.GetComponent<MeshRenderer>().material = wrMaterial;
            }
            // [75+%, 100%]
            else if (max * 0.75 < runtime && runtime <= max)
            {
                GetComponentsInChildren<MeshRenderer>()[0].material = tankMaterials[4];
                rendererRight.material = tankMaterials[4];
                windowLeft.GetComponent<MeshRenderer>().material = wlMaterial;
                windowRight.GetComponent<MeshRenderer>().material = wrMaterial;
            }
        }*/
    }
}