using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Loading Progress")]
    public class LoadingProgress : ScriptableObject
    {
        public float sceneProgess, spawnProgress;
        public bool scriptActive, isDone;
        public int count;

        public void Reset()
        {
            sceneProgess = 0;
            spawnProgress = 0;
            count = 0;
            scriptActive = false;
            isDone = false;
        }
    }
}