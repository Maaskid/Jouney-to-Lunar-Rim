using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stats
{
    public class DisplayPlayerStats : MonoBehaviour
    {
        public PlayerStats playerStats;

        public Slider tankSlider;
        public Slider schadenSlider;
        public Material[] tankMaterials;
        public GameObject windowRight;
        public GameObject windowLeft;
        public Material wlMaterial;
        public Material wrMaterial;
        public Material alphaMaterial;

        // Start is called before the first frame update
        void Start()
        {
            UpdateTankanzeige();
            UpdateSchadensanzeige();
        }

        // Update is called once per frame
        public void UpdateTankanzeige()
        {
            float runtime = playerStats.TankRuntime;
            float init = playerStats.tankInit;
            tankSlider.value = playerStats.TankRuntime;

            if (runtime <= init * 0)
            {
                GetComponentsInChildren<MeshRenderer>()[0].material = tankMaterials[0];
            }
            else if (runtime <= init * 0.25)
            {
                GetComponentsInChildren<MeshRenderer>()[0].material = tankMaterials[1];
            }
            else if (runtime <= init * 0.5)
            {
                GetComponentsInChildren<MeshRenderer>()[0].material = tankMaterials[2];
            }
            else if (runtime <= init * 0.75)
            {
                GetComponentsInChildren<MeshRenderer>()[0].material = tankMaterials[3];
            }
            else if (runtime <= init)
            {
                GetComponentsInChildren<MeshRenderer>()[0].material = tankMaterials[4];
            }
        }
        public void UpdateSchadensanzeige()
        {
            float runtime = playerStats.SchadenRuntime;
            float max = playerStats.schadenMax;
            schadenSlider.value = playerStats.SchadenRuntime;
            windowLeft.GetComponent<MeshRenderer>().material = alphaMaterial;
            windowRight.GetComponent<MeshRenderer>().material = alphaMaterial;

            if (runtime <= max * 0)
            {
                GetComponentsInChildren<MeshRenderer>()[1].material = tankMaterials[0];
            }
            else if (runtime <= max * 0.25)
            {
                GetComponentsInChildren<MeshRenderer>()[1].material = tankMaterials[1];
            }
            else if (runtime <= max * 0.5)
            {
                GetComponentsInChildren<MeshRenderer>()[1].material = tankMaterials[2];
            }
            else if (runtime <= max * 0.75)
            {
                GetComponentsInChildren<MeshRenderer>()[1].material = tankMaterials[3];
                windowLeft.GetComponent<MeshRenderer>().material = wlMaterial;
                windowRight.GetComponent<MeshRenderer>().material = wrMaterial;
            }
            else if (runtime <= max)
            {
                GetComponentsInChildren<MeshRenderer>()[1].material = tankMaterials[4];
                windowLeft.GetComponent<MeshRenderer>().material = wlMaterial;
                windowRight.GetComponent<MeshRenderer>().material = wrMaterial;
            }
        }
    }
}