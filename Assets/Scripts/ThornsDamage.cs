using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsDamage : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    public Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }        
    }
}
