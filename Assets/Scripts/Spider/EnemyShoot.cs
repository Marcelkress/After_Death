using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyShoot : MonoBehaviour
{
    public Transform shootPos;
    public Projectile projectileTemplate;
    private Projectile[] projectiles = new Projectile[237];  // array of projectiles
    public List<Projectile> projectileList = new List<Projectile>();
    public Vector3 offset;
    public AudioSource aS;
    public float shootingDistance;
    public float shootCoolDown;
    private float lastShot;
    public float speed;

    private void Update()
    {
        foreach(PlayerController pC in PlayerController.players)
        {
            if(Vector2.Distance(pC.transform.position, transform.position) < shootingDistance && Time.time - lastShot >= shootCoolDown)
            {
                Shoot(pC);
            }
        }
    }

    // Update is called once per frame
    public void Shoot(PlayerController player)
    {
        aS.Play();

        Projectile newProjectile = Instantiate(projectileTemplate);
        newProjectile.transform.position = shootPos.position;
        Vector2 direction = ((Vector2)((player.transform.position - transform.position))).normalized;
        newProjectile.Setup(speed, direction);
        projectileList.Add(newProjectile);
        lastShot = Time.time;

        newProjectile.transform.Rotate(Vector3.forward, Vector2.SignedAngle(newProjectile.transform.right, direction) + ((Mathf.Sign(transform.localScale.x) * .5f + .5f)) * 180);


    }


}
