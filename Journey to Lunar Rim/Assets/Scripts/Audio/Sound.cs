using UnityEngine;

namespace Audio
{
     [System.Serializable]
     public class Sound
     {
          public string name;

          public SoundType type;
          
          public AudioClip clip;

          public bool loop;
          // [Range(0f, 1f)] public float volume;
          [Range(.1f, 3f)] public float pitch;
          [Range(0f, 1f)] public float spatialBlend;

          [HideInInspector]
          public AudioSource source;

     }
}
