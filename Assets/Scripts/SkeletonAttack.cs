using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkeletonAttack : MonoBehaviour
{
    public Animator anim;
    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public LayerMask EnemyLayers;
    public int attackDamage = 10;

    private float lastHit;
    public float hitCoolDown = 0.9f;

    public void Attack(InputAction.CallbackContext context)
    {      
        if(context.phase == InputActionPhase.Started && Time.time - lastHit >= hitCoolDown)
        {
            //Play animation
            anim.SetTrigger("Attack");

            //Detect enemies in range
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);

            //Apply damage
            foreach(Collider2D enemy in hitEnemies)
            {
                Debug.Log("Skeleton Hit" + enemy.name);

                if (enemy.gameObject.tag == "SpiderBoss")
                {
                    enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
                }
                else if (enemy.gameObject.tag == "BabySpider")
                {
                    enemy.GetComponent<BabySpider>().TakeDamage(attackDamage);
                }
            }
            lastHit = Time.time;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }

}
    
    
