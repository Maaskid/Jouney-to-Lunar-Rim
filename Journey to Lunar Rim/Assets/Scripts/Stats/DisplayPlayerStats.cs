using System;
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

        private AudioManager _audioManager;

        private bool _warnTank, _warnSchaden;

        private void Awake()
        {
            _audioManager = FindObjectOfType<AudioManager>();
        }

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
            {
                _audioManager.PlayOneShot(SoundNames.Audiolog.ToString());
                StartCoroutine(ShowStartSequence());
            }
        }

        /**
         * Called if TankStats change to update their display.
         * Fetches tankRuntime to decide which material to display.
         */
        public void DisplayTankStats()
        {
            var runtime = gameObject.GetComponent<PlayerController>().tank;
            var max = gameObject.GetComponent<PlayerController>().maxTank;
            
            if (runtime <= 0)
            {
                rendererLeft.material = tankMaterials[0];
                _warnTank = true;// show warning
            }
            
            else if (0 < runtime && runtime <= max * 0.25)
            {
                rendererLeft.material = tankMaterials[1];
                _warnTank = true;// show warning
            }
            
            else if (max * 0.25 < runtime && runtime <= max * 0.5)
            {
                rendererLeft.material = tankMaterials[2];
                _warnTank = false;
            }
            
            else if (max * 0.5 < runtime && runtime <= max * 0.75)
            {
                rendererLeft.material = tankMaterials[3];
                _warnTank = false;
            }
            
            else if (max * 0.75 < runtime && runtime <= max)
            {
                rendererLeft.material = tankMaterials[4];
                _warnTank = false;
            }
            Warn();
        }

        /**
         * Called if SchadenStats change to update their display.
         * Fetches schadenRuntime to decide which material to display.
         */
        public void DisplaySchadenStats()
        {
            var runtime = gameObject.GetComponent<PlayerController>().lives;

            switch(runtime){
                case 0:
                    rendererRight.material = schildMaterials[0];
                    _warnSchaden = true;
                break;
                case 1:
                    rendererRight.material = schildMaterials[1];
                    _warnSchaden = true;
                break;
                case 2:
                    rendererRight.material = schildMaterials[2];
                    _warnSchaden = false;
                break;
                case 3:
                    rendererRight.material = schildMaterials[3];
                    _warnSchaden = false;
                break;
            }
            
            Warn();
        }


        private void Warn()
        {
            if (_warnSchaden || _warnTank)
            {
                if (!_audioManager.GetSource(SoundNames.Warning).isPlaying)
                    _audioManager.PlayOneShot(SoundNames.Warning.ToString());
                
                windowLeft.GetComponent<MeshRenderer>().material = wlMaterial;
                windowMiddle.GetComponent<MeshRenderer>().material = wmMaterial;
                windowRight.GetComponent<MeshRenderer>().material = wrMaterial;
            }
            else
            {
                if (_audioManager.GetSource(SoundNames.Warning).isPlaying)
                    _audioManager.Stop(SoundNames.Warning.ToString());
                windowLeft.GetComponent<MeshRenderer>().material = alphaMaterial;
                windowMiddle.GetComponent<MeshRenderer>().material = alphaMaterial;
                windowRight.GetComponent<MeshRenderer>().material = alphaMaterial;
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
            _audioManager.PlayOneShot(SoundNames.Audiolog.ToString());
        }
        
        public IEnumerator ShowDialogues()
        {
            yield return new WaitForSeconds(_audioManager.GetClip(SoundNames.ContainerCollected1).length);
            _audioManager.PlayOneShot(SoundNames.Audiolog.ToString());
            yield return new WaitForSeconds(_audioManager.GetClip(SoundNames.Audiolog).length);
            
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
            
            _audioManager.PlayOneShot(SoundNames.Audiolog.ToString());

            yield return new WaitForSeconds(1.5f);
        }
        
        public IEnumerator DeathSequence()
        {
            _audioManager.Stop(SoundNames.Warning.ToString());
            yield return new WaitForSeconds(1f);
            
            _audioManager.PlayOneShot(SoundNames.Audiolog.ToString());
            yield return new WaitForSeconds(_audioManager.GetClip(SoundNames.Audiolog).length);
            
            _audioManager.PlayOneShot(SoundNames.GameOver.ToString());
            
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
            _audioManager.PlayOneShot(SoundNames.Audiolog.ToString());
            yield return new WaitForSeconds(_audioManager.GetClip(SoundNames.Audiolog).length);
            Debug.Log("Dead");
            yield return new WaitForSeconds(1.5f);
            _audioManager.Stop(SoundNames.InGameTheme.ToString());
            _audioManager.Play(SoundNames.MenuTheme.ToString());
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