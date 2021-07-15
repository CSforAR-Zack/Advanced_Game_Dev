using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracks : MonoBehaviour
{
    public AudioClip[] tracks = null;
    public AudioSource targetSource = null;

    public void AudioClip(int track)
    {
        if (targetSource.clip != tracks[track])
        {
            targetSource.clip = tracks[track];
            targetSource.Play();
        }
    }
}