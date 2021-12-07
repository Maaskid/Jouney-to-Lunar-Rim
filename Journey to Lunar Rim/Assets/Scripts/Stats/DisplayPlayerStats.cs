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
            Tank(4);
            UpdateSchadensanzeige();
        }

        public void Tank(int counter)
        {
            rendererLeft.material = tankMaterials[counter];
            switch (counter)
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