using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;
    public EnemyHealthBar healthBar;

    //Anim
    public Animator anim;

    [Header("Audio")]
    public float targetVolume = 0;
    public float fadeDuration = 10;
    public float volumeStart = 1;
    public EventChanger eventChanger;

    [Header("Disable Scripts")]
    public EnemyTouchDamage enemyTouch;
    public EnemyShoot shoot;
    public EnemyPatrol enemyPatrol;

    [Header("Reload")]
    public float delayBeforeLoading = 10f;

    private Collider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);

        myCollider = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            Die();
            myCollider.enabled = false;
        }  
    }

    void Die()
    {
        Debug.Log("Enemy Dead");

        //Die Animation
        anim.Play("SpiderDie");

        //Stop Music
        
        //Disable Enemy
        enemyPatrol.enabled = false;
        enemyTouch.enabled = false;
        shoot.enabled = false;

        StartCoroutine(DelayDeath());      
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(delayBeforeLoading);
        GameManager.instance.LoadScene0();
    }
}



