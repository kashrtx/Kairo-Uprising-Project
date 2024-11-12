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
        Audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter()
    {
        if (!alreadyPlayed)
        {
            Audio.Play();
            alreadyPlayed = true;
        }
    }

    public void PlayBossMusic()
    {
        if (Audio.isPlaying)
        {
            Audio.Stop();

        }
        Audio.clip = BossMusic;
        Audio.Play();
    }

    public void StopMusic()
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
