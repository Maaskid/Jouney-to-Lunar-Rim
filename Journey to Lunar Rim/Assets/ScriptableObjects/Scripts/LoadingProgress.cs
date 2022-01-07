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

        public void ResetRetry()
        {
            retry = false;
        }
    }
}