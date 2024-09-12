using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    private float creationTime;
    public LayerMask EnemyLayer;
    public int attackDamage = 1;


    public void Setup(float speed, Vector2 direction)
    {
        // setup this projectile
        this.speed = speed;
        this.direction = direction;

        // save time of creation
        creationTime = Time.time;

        if (direction.x < 0)
        {
            Vector3 currentScale = transform.localScale;
            currentScale.x *= -1;

            transform.localScale = currentScale;
        }
    }

    private void Update()
    {

        if (creationTime + 5 < Time.time)
        {
            Destroy(gameObject);
        }

        transform.position = transform.position + (Vector3)direction * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SpiderBoss")
        {
            Debug.Log("Ghost Hit");

            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(attackDamage);           
        }
        else if (collision.gameObject.tag == "Player")
        {            
            collision.gameObject.GetComponent<Health>().TakeDamage(attackDamage, false);
        }
        else if (collision.gameObject.tag == "BabySpider")
        {
            Debug.Log("Ghost Hit");

            collision.gameObject.GetComponent<BabySpider>().TakeDamage(attackDamage);
        }
        Destroy(gameObject);
    }

}