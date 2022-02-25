using UnityEngine;
using System;

public class SoundManager : SingletonGeneric<SoundManager>
{
    public AudioSource audioEffects;
    public AudioSource audioMusic;
    public Sound[] AudioList;


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySoundEffects(SoundType soundType)
    {
        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            audioEffects.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("No Audio Clip got selected");
        }
    }

    public void PlayBackgroundMusic(SoundType soundType)
    {
        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            audioMusic.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("No Audio Clip got selected");
        }
    }

    public AudioClip GetSoundClip(SoundType soundType)
    {
        Sound sound = Array.Find(AudioList, item => item.soundType == soundType);
        if (sound != null)
        {
            return sound.audio;
        }
        return null;
    }

    public void StopSoundEffect()             // Sets the audio clip to null.
    {
        audioEffects.clip = null;
    }

}

[Serializable]
public class Sound
{
    public SoundType soundType;
    public AudioClip audio;
}

public enum SoundType
{
    BackgroundMusic1,
    ButtonClick,
    RoundWon,
    RoundLost,
    Draw
}
