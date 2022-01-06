using UnityEngine;

namespace ScriptableObjects.Scripts
{
    [CreateAssetMenu(fileName = "New Loading Progress")]
    public class LoadingProgress : ScriptableObject
    {
        public float spawnProgress;
        public bool scriptActive, isDone, startSequencePlayed;
        public int count;
        public SceneIndexes sceneToLoad;

        public void ResetProgress()
        {
            spawnProgress = 0;
            count = 0;
            scriptActive = false;
            isDone = false;

            if (!sceneToLoad.Equals(SceneIndexes.Level1))
            {
                startSequencePlayed = true;
            }
            else
            {
                startSequencePlayed = false;
            }
        }
    }
}