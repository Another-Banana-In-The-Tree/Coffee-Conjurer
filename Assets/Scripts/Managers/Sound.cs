using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    //sound name
    public string name;
    //store audioclip
    public AudioClip clip;
    //range for inspector
    [Range(0f,1f)]
    public float volume;
    [Range(0.1f,3f)]
    public float pitch;
    //loop option in inspector
    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
