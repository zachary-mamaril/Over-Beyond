using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]//to appear in unity inspector
public class sound
{
    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(0f,3f)]
    public float pitch;
    [HideInInspector] //value we populate automatically/ a public variable wont appear in the inspector
    public AudioSource source;
}
