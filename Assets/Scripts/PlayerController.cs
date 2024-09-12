using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public enum CharacterType
{
    Ghost = 0,
    Skeleton = 1,
}

public class PlayerController : MonoBehaviour
{
    public static List<PlayerController> players = new List<PlayerController>();


    public CharacterType characterType;
    public Rigidbody2D theRB;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;
    public bool facingLeft = true;
    //public bool isGrounded;
    private float inputX;

    public float extraHeight;
    private BoxCollider2D boxCollider;

    public LayerMask whatIsGround;
    public Animator anim;
    private SpawnPoint currentSpawnPoint = null;
    private int indexOfSpawnPoint = -1;
    private Vector3 startPos;
    public float groundRadius = .55f;

    private void Awake()
    {
        if (players.Find(p => p.characterType == characterType) == null)
        {
            players.Add(this);
        }
        boxCollider = GetComponent<BoxCollider2D>();
        startPos = transform.position;
    }

    private void OnDestroy()
    {
        players.Remove(this);
    }

    public void MoveToLastSpawnPoint()
    {
        if(currentSpawnPoint != null)
        {
            transform.position = currentSpawnPoint.transform.position;
        }
        else
        {
            transform.position = startPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState != GameState.Play)
        {
            theRB.velocity = Vector2.zero;
            return;
        }

        theRB.velocity = new Vector2(inputX * moveSpeed, theRB.velocity.y);

        //check if on the ground
        //isGrounded = Physics2D.OverlapCircle(transform.position, groundRadius, whatIsGround);

        if(inputX > 0 && facingLeft)
        {
            Flip();
        }
        if(inputX < 0 && !facingLeft)
        {
            Flip();
        }

        //MOVEMENT ANIMATION
        if (anim)
        anim.SetFloat("Speed", Mathf.Abs(inputX));

        if (currentSpawnPoint == null)
        {
            SpawnPoint firstBigger = SpawnPoint.spawnPoints.Find(s => s.transform.position.x > transform.position.x);
            if (firstBigger != null)
            {
                int indexOfFirstBigger = SpawnPoint.spawnPoints.IndexOf(firstBigger);
                if (indexOfFirstBigger > 0)
                {
                    currentSpawnPoint = SpawnPoint.spawnPoints[indexOfFirstBigger - 1];
                    indexOfSpawnPoint = indexOfFirstBigger - 1;
                } 
            }
        }
        else
        {
            if(indexOfSpawnPoint < SpawnPoint.spawnPoints.Count - 1)
            {
                if (transform.position.x >= SpawnPoint.spawnPoints[indexOfSpawnPoint + 1].transform.position.x)
                {
                    currentSpawnPoint = SpawnPoint.spawnPoints[indexOfSpawnPoint + 1];
                    indexOfSpawnPoint++;
                }
            }
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeight, whatIsGround);

        return raycastHit.collider != null;
    }

    public void Die()
    {
        anim.Play("Death");
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {  
        if ((IsGrounded() && context.phase == InputActionPhase.Started)) /* || (!isGrounded && context.phase == InputActionPhase.Canceled))*/
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1f;
        gameObject.transform.localScale = currentScale;
        facingLeft = !facingLeft;
    }

}

 


