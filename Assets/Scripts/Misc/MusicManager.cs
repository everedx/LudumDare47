using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    [SerializeField] AudioSource source1;
    [SerializeField] AudioSource source2;
    [SerializeField] AudioSource cameraSFXSource;
    [SerializeField] float durationInSeconds;

    int iterationIndex;
    AudioSource activeSource;

    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        iterationIndex = 0;
        activeSource = source1;
        activeSource.Play();
    }

    // Update is called once per frame
    void Update()
    {



        if (Math.Floor(Time.timeSinceLevelLoad / durationInSeconds) > iterationIndex)
        {
            if (activeSource == source1)
                activeSource = source2;
            else
                activeSource = source1;

            activeSource.Play();
            iterationIndex++;
        }

    }

    public void setVolume(float value)
    {
        activeSource.volume = value;
    }

    public float GetMusicVolume()
    {
       return  activeSource.volume;
    }

    public void PlaySFX(AudioClip clip)
    {
        cameraSFXSource.PlayOneShot(clip);
    }

    public void PauseMusic()
    {
        source1.Pause();
        source2.Pause();
    }

    public void ResumeMusic()
    {
        source1.UnPause();
        source2.UnPause();
    }
}

