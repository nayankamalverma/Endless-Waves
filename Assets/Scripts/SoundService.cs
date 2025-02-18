using System;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    private static SoundManger instance;
    public static SoundManger Instance { get { return instance; } }

    [SerializeField]
    private AudioSource Music;
    [SerializeField]
    private AudioSource soundEffect;
    [SerializeField]
    private SoundType[] Sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(global::Sounds.Music);
    }
    public void PlayMusic(Sounds sound)
    {
        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            Music.clip = clip;
            Music.Play();
        }
    }

    public void Play(Sounds sound)
    {
        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            soundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("Audio clip not found for " + sound);
        }

    }

    private AudioClip getSoundClip(Sounds sound)
    {
        SoundType soundType = Array.Find(Sounds, audio => audio.soundType == sound);
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