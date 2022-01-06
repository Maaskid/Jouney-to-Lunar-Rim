using System;
using Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance; 
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            // Destroy(this);
            Destroy(gameObject);
        
        DontDestroyOnLoad(this);
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.spatialBlend = s.spatialBlend;
            s.source.loop = s.loop;
        }   
    }

    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null) return;
        s.source.Play();
    }

    public void Stop(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null) return;
        s.source.Stop();
    }

    public void VolumeUp(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null) return;
        s.source.volume += .5f;
    }
    
    public void VolumeDown(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null) return;
        s.source.volume -= .5f;
    }
}
