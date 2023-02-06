using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] float moveSpeed;
    private float moveDir = 1;
    private bool facingRight = true;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] float circleRadius; // Checks for layers of ground and wall
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    private bool checkGround;
    private bool checkWall;
    
    
    [Header("Other")]
    private Rigidbody2D enemyRB;
    private Animator enemyAnim;
    private bool isMovingIgnoreCol;
    private float playerAttackOpeningCounter = 0f;
    public float playerOpeningAllowance;
    private Movement checkDashing;
    private Collider2D enemyColl;
    private Collider2D playerColl;
    public bool startCount;
    public float counter;
    public GameObject particle;
    public AudioSource death;
    public AudioSource jump;



    [Header("Jump")]
    [SerializeField] float jumpHeight;
    [SerializeField] Transform player;
    [SerializeField] Transform groundCheckLeap;
    [SerializeField] Vector2 boxSize;
    private bool attackTrue;
    private bool isGrounded;

    [Header("LOS")]

    [SerializeField] Vector2 los;
    [SerializeField] LayerMask playerLayer;
    private bool canSeePlayer;

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        checkDashing = player.GetComponent<Movement>();
        playerColl = player.GetComponent<Collider2D>();
        enemyColl = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
      
        //Layer checks 
        checkGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);
        checkWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, wallLayer);
        isGrounded = Physics2D.OverlapBox(groundCheckLeap.position, boxSize, 0, groundLayer);
        canSeePlayer = Physics2D.OverlapBox(transform.position, los, 0, playerLayer);
        
        AnimationControl(); //Leap and Flip tied to anim
        
        if(!canSeePlayer && isGrounded)
        {
            Patrol();
            
        }
        if(startCount)
        {
            counter += Time.deltaTime;
            if (counter <= 0.15f) // Mess around with this value
            {
                Physics2D.IgnoreCollision(enemyColl, playerColl, false);
            }
            else if (counter >= 0.15f)
            {
                Physics2D.IgnoreCollision(enemyColl, playerColl, true);

            }
        }

        if(!isGrounded)
        {
            if (playerAttackOpeningCounter <= playerOpeningAllowance) //This is to allow the player a time frame to to attack if mid air with the enemy. 

            {
                attackTrue = false;
                playerAttackOpeningCounter += Time.deltaTime;
              

            }

            else
            {
                attackTrue = true;
                if(enemyColl && playerColl != null)
                Physics2D.IgnoreCollision(enemyColl, playerColl, false);

            }
        }

        else
        {
            attackTrue = false;
            playerAttackOpeningCounter = 0;
        }

   




    }

    void AnimationControl()
    {
        enemyAnim.SetBool("canSeePlayer", canSeePlayer);
        enemyAnim.SetBool("isGrounded", isGrounded);
    }
    void Patrol()
    {
        isMovingIgnoreCol = true;
        if(!checkGround || checkWall)
        {
            if(facingRight)
            {
                Flip();
            }

            else if (!facingRight)
            {
                Flip();
            }
        }
        enemyRB.velocity = new Vector2(moveSpeed * moveDir, enemyRB.velocity.y);
     
    }

    void LeapAttack() //Anim events
    {
        isMovingIgnoreCol = false;
        if (player != null)
        {
            float distanceFromPlayer = player.position.x - transform.position.x;
            jump.Play();


            if (isGrounded)
            {
                enemyRB.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);


            }

        }



    }

    void Flip() // Flips enemy 
    {
        moveDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    void FlipTowardPlayer() //Anim events
    {
        if (player != null)
        {

            float playerPos = player.position.x - transform.position.x;
            if (playerPos > 0 && facingRight)
            {
                Flip();
            }

            else if (playerPos < 0 && !facingRight)
            {
                Flip();
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Player")
       {
            startCount = true;
            enemyRB.constraints = RigidbodyConstraints2D.FreezeAll;
            
            
            
        }

        if (attackTrue)
        {
            
            if (other.gameObject.name == "Player")
            {
                death.Play();
                other.gameObject.SetActive(false);
                enemyRB.constraints = RigidbodyConstraints2D.None;

            }
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            enemyRB.constraints = RigidbodyConstraints2D.None;
            startCount = false;
            counter = 0;

        }



    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(groundCheckLeap.position, boxSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, los);
    }

    private void OnDisable()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
    }

}

