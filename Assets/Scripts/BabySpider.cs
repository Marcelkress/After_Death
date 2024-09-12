using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySpider : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    //Anim
    public Animator anim;

    [Header("Disable Scripts")]
    public EnemyTouchDamage enemyTouch;
    public EnemyShoot shoot;
    public EnemyPatrol enemyPatrol;

    public float delayBeforeLoading = 10f;

    private Collider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        myCollider = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
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
        gameObject.SetActive(false);

    }
}
