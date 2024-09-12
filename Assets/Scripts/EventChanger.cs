using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChanger : MonoBehaviour
{
    [Header("Camera")]
    public Camera Cam1;
    public Camera Cam2;
    public Canvas Canvas;
    public int Collisions;
    public RectTransform spiderHealthBar;

    [Header("Audio")]
    public AudioClip newMusic;
    public float targetVolume = 1;
    public float fadeDuration = 10;
    public float volumeStart = 0;
    public AudioClip loopableMusic;

    List<Rigidbody2D> collided = new List<Rigidbody2D>();
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.tag == "Player" && !collided.Contains(collision.attachedRigidbody))
       {
           Collisions++;
           collided.Add(collision.attachedRigidbody);
       }
       
       if (Collisions > 1)
       {
            Cam2.gameObject.SetActive(true);
            Cam1.gameObject.SetActive(false);

            Canvas.worldCamera = Cam2;

            GameManager.instance.audioSource.Stop();
            GameManager.instance.audioSource.clip = newMusic;
            GameManager.instance.audioSource.loop = false;

            GameManager.instance.loopableSource.clip = loopableMusic;
            GameManager.instance.loopableSource.loop = true;
            GameManager.instance.loopableSource.Stop();

            double dspNow = AudioSettings.dspTime+.1f;
            GameManager.instance.audioSource.PlayScheduled(dspNow);
            GameManager.instance.loopableSource.PlayScheduled(dspNow + GameManager.instance.audioSource.clip.length);
            GameManager.instance.audioSource.volume = volumeStart;
            StartCoroutine(FadeVolume(fadeDuration, GameManager.instance.audioSource, volumeStart, targetVolume));

            spiderHealthBar.gameObject.SetActive(true);
       }        
    }

    public IEnumerator FadeVolume (float duration, AudioSource aS, float start, float end)
    {
        float startTime = Time.time;
        
        while (Time.time - startTime < duration)
        {
            aS.volume = Mathf.Lerp(start, end, (Time.time - startTime )/ duration);

            yield return null;
        }
        aS.volume = end;
    }
}
