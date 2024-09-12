using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthpoint : MonoBehaviour
{
    [SerializeField] private float healthvalue = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().AddHealth(healthvalue);

            GameManager.instance.HealthPickupSound();

            gameObject.SetActive(false);
           
        }
    }

}
