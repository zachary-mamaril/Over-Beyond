using UnityEngine.Audio;
using UnityEngine;
using System;

public class Audiomanager : MonoBehaviour
{
    public sound[] sounds;//array allows us to always add new sounds

    void Awake()//awake method is like start, but is called before start
    {
        foreach(sound s in sounds)//for each sound in sounds
        {
            //add these components to the inspector
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }
    public void Play(string name)
    {
        
       sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
