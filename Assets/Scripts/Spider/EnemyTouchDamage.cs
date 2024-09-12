    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTouchDamage : MonoBehaviour
{
    [SerializeField] private float damage = 1f;

    public PlayerController pC;
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Vector2 normal = collision.GetContact(0).normal;
            //collision.rigidbody.velocity += normal * 5;

            collision.gameObject.GetComponent<Health>().TakeDamage(damage, false);

        }

    }
}