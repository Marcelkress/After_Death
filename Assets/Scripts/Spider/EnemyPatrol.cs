using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform leftEdge;
    public Transform rightEdge;
    public Transform enemy;

    public float speed = 5f;
    private Vector3 initScale;
    private bool movingLeft;

    public Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    public void Update()
    {
        if (movingLeft)
        {
            if(enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if(enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }
    
    private void DirectionChange()
    {
        anim.SetBool("SpiderRun", false);

        movingLeft = !movingLeft;
    }
    
    private void MoveInDirection(int _direction)
    {
        anim.SetBool("SpiderRun", true);

        //Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * -_direction, initScale.y, initScale.z);

        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }



}
