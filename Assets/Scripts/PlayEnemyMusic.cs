using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEnemyMusic : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip SoundToPlay;
    public AudioClip BossMusic;
    public float Volume;
    AudioSource Audio;
    public bool alreadyPlayed = false;
    void Start()
    {
        // Get audio component
        Audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter()
    {
        // Play audio once
        if (!alreadyPlayed)
        {
            Audio.Play();
            alreadyPlayed = true;
        }
    }

    public void PlayBossMusic()
    {
        // Stop enemy music and play boss music
        if (Audio.isPlaying)
        {
            Audio.Stop();

        }
        Audio.clip = BossMusic;
        Audio.Play();
    }

    public void StopMusic() // Stop playing audio
    {
        if(Audio.isPlaying)
        {
            Audio.Stop();
        }

        else
        {
            Debug.Log("AudioSource not playing");
        }

    }
}
