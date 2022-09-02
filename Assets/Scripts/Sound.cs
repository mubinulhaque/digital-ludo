using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; //Name of the sound

    public AudioClip clip; //Audio clip that plays the sound

    [Range(0f, 1f)] public float volume = 1f; //Volume of the sound

    public bool loop; //Whether the clip can loop

    [HideInInspector] public AudioSource source; //Configured in GameManager
}
