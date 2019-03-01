using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Subject
{
    //components
    //BoxCollider2D col;
    Rigidbody2D rb;
    private PlayerState state;
    //physcis
    public float Speed = 5f;
    public float JumpHeight = 1000f;
    float maxAcceleration = 10f;

    bool isGrounded;
    public LayerMask platformLayer;
    public Transform groundCheck;

    private void Start()
    {
        state = PlayerState.idle;
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
    }

    private void FixedUpdate()
    {
       
        handlePlayerState();
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, platformLayer);
        if (hit.transform != null && hit.transform.CompareTag("Spikes"))
        {
            //watch out for spikes
            state = PlayerState.death;
        }
    }

    void handlePlayerState()
    {
        switch (state)
        {
            case PlayerState.idle:
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                {
 //handle Idle State Change
                    state = PlayerState.walking;
                }else if(Input.GetKeyDown(KeyCode.UpArrow) && rb.velocity.y == 0f){
                    rb.AddForce(new Vector2(0, JumpHeight));
                    isGrounded = false;
                    state = PlayerState.jumping;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    state = PlayerState.ducking;
                }
               
                break;
            case PlayerState.walking:
 //handle walking State Change
                if (Input.GetKeyUp(KeyCode.LeftArrow)|| Input.GetKeyUp(KeyCode.RightArrow))
                {
                    state = PlayerState.idle;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow) && rb.velocity.y == 0f)
                {
                    rb.AddForce(new Vector2(0, JumpHeight));
                    isGrounded = false;
                    state = PlayerState.jumping;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    state = PlayerState.ducking;
                }
//handle walking code
                Accelerate();
                break;
            case PlayerState.jumping:
                //handle Jumping code

                Debug.DrawRay(transform.position, Vector2.down * 1f, Color.green);
                Vector2 targetSize = new Vector2(0.1f, 0.1f);
                RaycastHit2D hit = Physics2D.CircleCast(groundCheck.position, 0.3f, Vector2.down, 0f, platformLayer);
                //RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down,0.5f,platformLayer);

                if (hit.transform != null)
                {
                        if (hit.collider.transform.tag == ("Platforms"))
                    {
                        isGrounded = true;
                    }
                }

                //handle Jumping State Change
                if (isGrounded)
                {
                    state = PlayerState.idle;
                }


                break;
            case PlayerState.landing:
//handle Landing State Change
                state = PlayerState.idle;
                
                break;
            case PlayerState.ducking:
 //handle Ducking State Change
                if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    state = PlayerState.idle;
                }
//handle Ducking code
                break;
            case PlayerState.death:
                Debug.Log("player dead");
                StartCoroutine(HandlePlayerDeath());
                break;
        }

    }


    void Accelerate()
    { //Handles speeding up of movement

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * Input.GetAxis("Horizontal"), 1f, platformLayer);
        //if (hit.transform != null && rb.velocity.y != 0)
        //{
        //    //rb.velocity += Physics2D.gravity;
        //    rb.velocity = new Vector2(maxAcceleration * Input.GetAxis("Horizontal"), - 3f);
        //}

        if (rb.velocity.magnitude < maxAcceleration)
        {
            rb.velocity += Vector2.right * Speed * Input.GetAxis("Horizontal");
            //rb.AddForce(Vector2.right * Speed * Input.GetAxis("Horizontal"));
        }
        else
        {

            rb.velocity = new Vector2(maxAcceleration* Input.GetAxis("Horizontal"), rb.velocity.y);
        }
    }
    IEnumerator HandlePlayerDeath()
    {
        rb.velocity = Vector2.zero;
        transform.RotateAroundLocal(Vector3.up, 0.5f);
        yield return new WaitForSeconds(0.5f);
        notify(Event.PLAYER_DEATH);

    }

}

enum PlayerState{ 
    idle,
    walking, 
    jumping,
    landing,
    ducking,
    death
}
