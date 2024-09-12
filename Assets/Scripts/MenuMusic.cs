using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource loopableSource;
    public void Awake()
    {
        double dspNow = AudioSettings.dspTime + 1;
        audioSource.PlayScheduled(dspNow);
        //StartCoroutine(PlayEngineSound());
        loopableSource.PlayScheduled(dspNow + audioSource.clip.length);
    }
}
