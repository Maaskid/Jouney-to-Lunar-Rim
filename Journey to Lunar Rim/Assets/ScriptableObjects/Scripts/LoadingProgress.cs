using UnityEngine;

namespace ScriptableObjects.Scripts
{
    [CreateAssetMenu(fileName = "New Loading Progress")]
    public class LoadingProgress : ScriptableObject
    {
        public float spawnProgress;
        public bool scriptActive, isDone, playStartSequence, dead, missionAccomplished, retry;
        public int count;
        public SceneIndexes sceneToLoad;

        public float musicVolume = 5;
        public float sfxVolume = 5;

        public void ResetProgress()
        {
            spawnProgress = 0;
            count = 0;
            scriptActive = false;
            isDone = false;
            dead = false;
            missionAccomplished = false;

            if (!sceneToLoad.Equals(SceneIndexes.Level1) || retry)
            {
                playStartSequence = false;
            }
            else
            {
                playStartSequence = true;
            }
        }

        public void ResetVolume()
        {
            musicVolume = 5;
            sfxVolume = 5;
        }

        public void ResetRetry()
        {
            retry = false;
        }

        public void QuitReset()
        {
            spawnProgress = 0;
            scriptActive = false;
            isDone = false;
            playStartSequence = true;
            dead = false;
            missionAccomplished = false;
            retry = false;
            count = 0;
            sceneToLoad = SceneIndexes.Level1;

            musicVolume = 5;
            sfxVolume = 5;
        }
    }
}