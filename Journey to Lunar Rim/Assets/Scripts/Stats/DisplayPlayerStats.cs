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

        // Start is called before the first frame update
        void Start()
        {
            Tank(4);
            // UpdateTankanzeige();
            UpdateSchadensanzeige();
        }

        public void Tank(int counter)
        {
            rendererLeft.material = tankMaterials[counter];
        }
        public void UpdateSchadensanzeige()
        {
            float runtime = playerStats.SchadenRuntime;
            float max = playerStats.schadenMax;
            windowLeft.GetComponent<MeshRenderer>().material = alphaMaterial;
            windowRight.GetComponent<MeshRenderer>().material = alphaMaterial;

            if (runtime <= max * 0)
            {
                GetComponentsInChildren<MeshRenderer>()[0].material = tankMaterials[0];
                rendererRight.material = tankMaterials[0];
            }
            else if (runtime <= max * 0.25)
            {
                GetComponentsInChildren<MeshRenderer>()[0].material = tankMaterials[1];
                rendererRight.material = tankMaterials[1];
            }
            else if (runtime <= max * 0.5)
            {
                GetComponentsInChildren<MeshRenderer>()[0].material = tankMaterials[2];
                rendererRight.material = tankMaterials[2];
            }
            else if (runtime <= max * 0.75)
            {
                GetComponentsInChildren<MeshRenderer>()[0].material = tankMaterials[3];
                rendererRight.material = tankMaterials[3];
                windowLeft.GetComponent<MeshRenderer>().material = wlMaterial;
                windowRight.GetComponent<MeshRenderer>().material = wrMaterial;
            }
            else if (runtime <= max)
            {
                GetComponentsInChildren<MeshRenderer>()[0].material = tankMaterials[4];
                rendererRight.material = tankMaterials[4];
                windowLeft.GetComponent<MeshRenderer>().material = wlMaterial;
                windowRight.GetComponent<MeshRenderer>().material = wrMaterial;
            }
        }
    }
}