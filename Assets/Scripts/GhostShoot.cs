using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostShoot : MonoBehaviour
{
    public Projectile projectileTemplate;
    private Projectile[] projectiles = new Projectile[237];  // array of projectiles
    public List<Projectile> projectileList = new List<Projectile>();
    public Vector3 offset;
    public PlayerController PC;

    private float lastShot;
    public float shootCoolDown = 0.3f;
    

    // Update is called once per frame
    public void Shoot1(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && Time.time - lastShot >= shootCoolDown)
        {
        
            int dirFactor = 1;
            if (PC.facingLeft)
            {
                dirFactor = -1;
            }
            Projectile newProjectile = Instantiate(projectileTemplate);
            newProjectile.transform.position = transform.position + offset * dirFactor;
            newProjectile.Setup(10, Vector2.right * dirFactor);
            projectileList.Add(newProjectile);
            lastShot = Time.time;
        }
    }
}
