using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjects.Scripts
{
    [CreateAssetMenu(fileName = "Chapter", menuName = "ChapterSystem/Chapter")]
    public class Chapter : ScriptableObject
    {
        public ChapterNames chapterName;
        public List<ChapterDialoge> container = new List<ChapterDialoge>();
    }

    [System.Serializable]
    public class ChapterDialoge
    {
        public Sprite sprite;
        public SpeakerNames speaker;
        public ChapterDialoge(Sprite sprite)
        {
            this.sprite = sprite;
        }
    }
}
