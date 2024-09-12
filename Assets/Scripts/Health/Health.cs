using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public PlayerController pC;
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }

    private bool dead;
    public Animator anim;
    public AudioSource audioSource;
    public float delayBeforeLoading = 4f;
  
    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage, bool moveToLastSpawnPoint = true)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            audioSource.Play();
            //take damage animation her!!!
            if (moveToLastSpawnPoint)
            {
                pC.MoveToLastSpawnPoint();
            }            
        }
        else
        {
            if (!dead)
            {
                //Player death animation her!!!

                pC.Die();

                audioSource.Play();
                GetComponent<PlayerController>().enabled = false;
                dead = true;
                StartCoroutine(DelayDeath()); 
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(delayBeforeLoading);
        GameManager.instance.LoadScene2();
    }
}
