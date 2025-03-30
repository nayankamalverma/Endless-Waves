using System;
using Assets.Scripts.Utilities;
using UnityEngine;

public class SoundService : GenericMonoSingleton<SoundService>
{
    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private AudioSource soundEffect;
    [SerializeField]
    private SoundType[] sounds;

    private void Start()
    {
        PlayMusic(global::Sounds.Music);
    }
    public void PlayMusic(Sounds sound)
    {
        AudioClip clip = GetSoundClip(sound);
        if (clip != null)
        {
            music.clip = clip;
            music.Play();
        }
    }

    public void Play(Sounds sound)
    {
        AudioClip clip = GetSoundClip(sound);
        if (clip != null)
        {
            soundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("Audio clip not found for " + sound);
        }

    }

    private AudioClip GetSoundClip(Sounds sound)
    {
        SoundType soundType = Array.Find(sounds, audio => audio.soundType == sound);
        if (soundType != null)
        {
            return soundType.soundClip;
        }
        return null;
    }
}

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
}

public enum Sounds
{
    Music,
    ButtonClick,
    Shoot,
    GameOver    
}