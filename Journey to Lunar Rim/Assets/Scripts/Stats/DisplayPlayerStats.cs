using System.Collections;
using ScriptableObjects.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Stats
{
    public class DisplayPlayerStats : MonoBehaviour
    {
        //public PlayerStats playerStats;
        [Header("__Tank__")]
        public Material[] tankMaterials;
        public MeshRenderer rendererLeft;

        [Header("__Schild__")]
        public Material[] schildMaterials;
        public MeshRenderer rendererRight;

        [Header("__Warning__")]
        public GameObject windowLeft;
        public GameObject windowMiddle;
        public GameObject windowRight;
        public Material wlMaterial;
        public Material wmMaterial;
        public Material wrMaterial;
        public Material alphaMaterial;
        
        [Header("__Story__")]
        public Image freyaCore;
        public Image ministerBaker;
        public GameObject dialogueScreen;
        public Chapter startSequence;
        public Chapter currentChapter;
        public Chapter deathSequence;
        public SceneIndexes nextScene;

        [Header("__Loading Progress__")]
        public LoadingProgress loadingProgress;
        
        
        private bool _isDone;
        private int _index;
        /**
         * Displays both, Tank and Schaden stats, once in the beginning
         */
        void Start()
        {
            DisplayTankStats();
            DisplaySchadenStats();
        }

        public void CheckToShowStartSequence()
        {
            if (loadingProgress.playStartSequence)
                StartCoroutine(ShowStartSequence());
        }

        /**
         * Called if TankStats change to update their display.
         * Fetches tankRuntime to decide which material to display.
         */
        public void DisplayTankStats()
        {
            float runtime = gameObject.GetComponent<PlayerController>().tank;
            float max = gameObject.GetComponent<PlayerController>().maxTank;
            
            // [.., 0] → Tank leer
            if (runtime <= 0)
            {
                // Debug.Log("GameOver");
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

            // Debug.Log("Lives: " + runtime);

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

        private IEnumerator ShowStartSequence()
        {
            dialogueScreen.SetActive(true);
            
            var isDone = false;
            var index = 0;
            
            while (!isDone)
            {
                yield return new WaitForSeconds(5f);
                index++;
                if (index == startSequence.container.Count)
                {
                    isDone = true;
                }
                else
                {
                    if (startSequence.container[index].speaker==SpeakerNames.Freya)
                    {
                        ministerBaker.gameObject.SetActive(false);
                        freyaCore.sprite = startSequence.container[index].sprite;
                        freyaCore.gameObject.SetActive(true);
                    }
                    else
                    {
                        freyaCore.gameObject.SetActive(false);
                        ministerBaker.sprite = startSequence.container[index].sprite;
                        ministerBaker.gameObject.SetActive(true);
                    }
                }
            }
            
            //Zustand für EndSequence vorbereiten
            freyaCore.gameObject.SetActive(false);
            freyaCore.sprite = currentChapter.container[1].sprite;
            ministerBaker.gameObject.SetActive(true);
            ministerBaker.sprite = currentChapter.container[0].sprite;
            dialogueScreen.SetActive(false);

            loadingProgress.playStartSequence = false;
        }
        
        public IEnumerator ShowDialogues()
        {
            var index = 0;
            
            if (currentChapter.container[index].speaker.Equals(SpeakerNames.Freya))
            {
                FreyaSpeaks(currentChapter, index);
            }
            else
            {
                BakerSpeaks(currentChapter, index);
            }
            dialogueScreen.SetActive(true);

            while (index < currentChapter.container.Count-1)
            {
                yield return new WaitForSeconds(5f);
                index++;
                if (currentChapter.container[index].speaker==SpeakerNames.Freya)
                {
                    FreyaSpeaks(currentChapter, index);
                }
                else
                {
                    BakerSpeaks(currentChapter, index);
                }
            }
            
            yield return new WaitForSeconds(5f);
            loadingProgress.sceneToLoad = nextScene;
            loadingProgress.ResetRetry();
            
            if (!currentChapter.chapterName.Equals(ChapterNames.Level6))
                SceneManager.LoadScene((int)SceneIndexes.LevelLoading);
            else
                SceneManager.LoadScene((int)nextScene);
            
        }
        
        public IEnumerator DeathSequence()
        {
            var counter = 0;

            freyaCore.gameObject.SetActive(true);
            freyaCore.sprite = deathSequence.container[counter].sprite;
            ministerBaker.gameObject.SetActive(false);
            dialogueScreen.SetActive(true);
            
            while (counter < deathSequence.container.Count-1)
            {
                yield return new WaitForSeconds(5f);
                counter++;

                if (deathSequence.container[counter].speaker.Equals(SpeakerNames.Baker))
                {
                    BakerSpeaks(deathSequence, counter);
                }
                else
                {
                    FreyaSpeaks(deathSequence, counter);
                }
            }

            yield return new WaitForSeconds(5f);
            Debug.Log("Dead");
            SceneManager.LoadScene((int) SceneIndexes.Death);
        }

        private void FreyaSpeaks(Chapter chapter, int index)
        {
            ministerBaker.gameObject.SetActive(false);
            freyaCore.sprite = chapter.container[index].sprite;
            freyaCore.gameObject.SetActive(true);
        }

        private void BakerSpeaks(Chapter chapter, int index)
        {
            freyaCore.gameObject.SetActive(false);
            ministerBaker.sprite = chapter.container[index].sprite;
            ministerBaker.gameObject.SetActive(true);
        }
        
    }
}