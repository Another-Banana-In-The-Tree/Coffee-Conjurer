using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    //Create Array for sounds
    public Sound[] sounds;
    private List<Sound> bgm = new List<Sound>();
    private float volumeMod = 1f;
    private Sound currentMusic;
    private Sound nextInQueue;
    private int indexer = 0;
    private float timer;

    void Awake()
    {
        foreach(Sound s in sounds)
        {
            //get the Audiosource from the AudioManager GameObject
            //add things to inspector
            if (s.name.Contains("Theme"))
            {
                bgm.Add(s);
            }
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        //play Theme on start
        ChangeBackgroundMusic();
    }


    private void ChangeBackgroundMusic()
    {
        
        currentMusic = bgm[indexer % bgm.Count];
        print("playing" + currentMusic.name);
        indexer++;
        Play(currentMusic);
    }

    private void Update()
    {
        timer += Time.unscaledDeltaTime;
        if ((currentMusic.clip.length - currentMusic.source.time)/currentMusic.pitch <= 0)
        {
            timer = 0;
            ChangeBackgroundMusic();
        }
    }


    public void Play (string name)
    {
        //find the name of the sound if nothing comes back return null.
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null)
        {
            return;  
        }
        //play sound
        
        s.source.Play();
    }

    public float GetAudioLength(string name)
    {
        //find the name of the sound if nothing comes back return null.
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            return 0;
        }
        //play sound

        return s.source.clip.length;
    }

    private void Play(Sound s)
    {
        s.source.Play();
    }

    public void SetVolume(float mod)
    {
        volumeMod = mod;
        foreach(Sound s in sounds)
        {
            s.source.volume = s.volume * volumeMod;
        }
    }
}

