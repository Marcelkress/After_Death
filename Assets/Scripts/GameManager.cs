using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
public enum GameState
{
    None = 0,
    Menu = 1,
    Play = 2,
    Death = 3,
    End = 4,
}

public class GameManager : MonoBehaviour
{
    public KeyCode reloadKey;

    public static GameManager instance;
    public GameState gameState;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioSource loopableSource;
    public AudioSource healthPickupSource;

    [Header("FadeScenes")]
    public Image fadeImage;
    public Animator fadeAnimator;

    public void Awake()
    {
        instance = this;
        double dspNow = AudioSettings.dspTime + 1;
        audioSource.PlayScheduled(dspNow);
        //StartCoroutine(PlayEngineSound());
        loopableSource.PlayScheduled(dspNow + audioSource.clip.length);
    }

    public void StartGame()
    {
        gameState = GameState.Play;

        //audioSource.Play();
    }

    public void Update()
    {
        if(Input.GetKeyDown(reloadKey))
        {
            ReloadNow();
        }
    }

    public void HealthPickupSound()
    {
        healthPickupSource.Play();
    }

    public void ReloadGame()
    {
       gameState = GameState.Death;

       Invoke("ReloadNow", 3);
    }

    void ReloadNow()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene0()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadScene3()
    {
        StartCoroutine(Fading());        
    }

    IEnumerator Fading()
    {
        fadeAnimator.SetBool("Fade", true);
        yield return new WaitUntil(() => fadeImage.color.a == 1);
        SceneManager.LoadScene(3);
    }

}