using System;
using Audio;
using ScriptableObjects.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioClip[] clips;
    public LoadingProgress loadingProgress;

    public static AudioManager Instance;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);

        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            if (s.type.Equals(SoundType.Music))
            {
                s.source.volume = loadingProgress.musicVolume;
            }
            else
            {
                s.source.volume = loadingProgress.sfxVolume;
            }
            
            s.source.pitch = s.pitch;
            s.source.spatialBlend = s.spatialBlend;
            s.source.loop = s.loop;
            s.source.playOnAwake = false;
        }
    }

    private void Start()
    {
        Play(SoundNames.MenuTheme.ToString());
    }

    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null) return;
        s.source.Play();
    }
    public void PlayOneShot(string clipName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == clipName);
        AudioClip c = Array.Find(clips, clip => clip.name == clipName);
        if (c == null) return;
        s.source.PlayOneShot(c, 1f);
    }

    public void Stop(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found");
            return;
        }
        s.source.Stop();
    }

    public AudioSource GetSource(SoundNames soundName)
    {
        return Array.Find(sounds, sound => sound.name.Equals(soundName.ToString())).source;
    }

    public AudioClip GetClip(SoundNames clipName)
    {
        return Array.Find(clips, clip => clip.name.Equals(clipName.ToString()));
    }
    
    public void VolumeUp(SoundType type)
    {
        foreach (var sound in sounds)
        {
            if (sound.type.Equals(type))
                sound.source.volume += .2f;
        }
    }
    
    public void VolumeDown(SoundType type)
    {
        foreach (var sound in sounds)
        {
            if (sound.type.Equals(type))
                sound.source.volume -= .2f;
        }
    }

    public void SetVolume(SoundType type)
    {
        var soundOfType = Array.FindAll(sounds, sound => sound.type.Equals(type));

        if (type.Equals(SoundType.Sfx))
        {
            foreach (var sound in soundOfType)
            {
                sound.source.volume = loadingProgress.sfxVolume;
            }
        }
        else
        {
            foreach (var sound in soundOfType)
            {
                sound.source.volume = loadingProgress.musicVolume;
            }
        }
    }

    private void OnApplicationQuit()
    {
        loadingProgress.QuitReset();
    }
}
