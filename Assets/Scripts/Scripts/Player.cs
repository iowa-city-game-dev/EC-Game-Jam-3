using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;
using Event = Managers.Event;

namespace Scripts
{
    public class Player : Subject
    {
        //components
        //BoxCollider2D col;
        Rigidbody2D rb;

        private PlayerState state;

        //physics
        public float speed = 5f;
        public float jumpHeight = 1000f;
        float maxAcceleration = 10f;

        bool isGrounded;
        public LayerMask platformLayer;
        public Transform groundCheck;

        private void Start()
        {
            state = PlayerState.Idle;
            rb = GetComponent<Rigidbody2D>();
            isGrounded = true;
        }

        private void FixedUpdate()
        {
            HandlePlayerState();
        }

        private void Update()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, platformLayer);
            if (hit.transform != null && hit.transform.CompareTag("Spikes"))
            {
                //watch out for spikes
                state = PlayerState.Death;
            }
        }

        private void HandlePlayerState()
        {
            switch (state)
            {
                case PlayerState.Idle:
                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                    {
                        //handle Idle State Change
                        state = PlayerState.Walking;
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow) && rb.velocity.y == 0f)
                    {
                        rb.AddForce(new Vector2(0, jumpHeight));
                        isGrounded = false;
                        state = PlayerState.Jumping;
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        state = PlayerState.Ducking;
                    }

                    break;

                case PlayerState.Walking:
                    //handle walking State Change
                    if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                    {
                        state = PlayerState.Idle;
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow) && rb.velocity.y == 0f)
                    {
                        rb.AddForce(new Vector2(0, jumpHeight));
                        isGrounded = false;
                        state = PlayerState.Jumping;
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        state = PlayerState.Ducking;
                    }

//handle walking code
                    Accelerate();
                    break;

                case PlayerState.Jumping:
                    //handle Jumping code

                    Debug.DrawRay(transform.position, Vector2.down * 1f, Color.green, 2.0f);
                    var targetSize = new Vector2(0.1f, 0.1f);
                    var hit = Physics2D.CircleCast(groundCheck.position, 0.3f, Vector2.down, 0f, platformLayer);
                    //RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down,0.5f,platformLayer);

                    if (hit.transform)
                    {
                        if (hit.collider.transform.tag == ("Platforms"))
                        {
                            isGrounded = true;
                        }
                    }

                    //handle Jumping State Change
                    if (isGrounded)
                    {
                        state = PlayerState.Idle;
                    }

                    break;

                case PlayerState.Landing:
//handle Landing State Change
                    state = PlayerState.Idle;

                    break;

                case PlayerState.Ducking:
                    //handle Ducking State Change
                    if (Input.GetKeyUp(KeyCode.DownArrow))
                    {
                        state = PlayerState.Idle;
                    }

//handle Ducking code
                    break;

                case PlayerState.Death:
                    Debug.Log("player dead");
                    StartCoroutine(HandlePlayerDeath());
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        void Accelerate()
        {
            //Handles speeding up of movement

            //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * Input.GetAxis("Horizontal"), 1f, platformLayer);
            //if (hit.transform != null && rb.velocity.y != 0)
            //{
            //    //rb.velocity += Physics2D.gravity;
            //    rb.velocity = new Vector2(maxAcceleration * Input.GetAxis("Horizontal"), - 3f);
            //}

            if (rb.velocity.magnitude < maxAcceleration)
            {
                rb.velocity += Vector2.right * speed * Input.GetAxis("Horizontal");
                //rb.AddForce(Vector2.right * Speed * Input.GetAxis("Horizontal"));
            }
            else
            {
                rb.velocity = new Vector2(maxAcceleration * Input.GetAxis("Horizontal"), rb.velocity.y);
            }
        }

        IEnumerator HandlePlayerDeath()
        {
            rb.velocity = Vector2.zero;
            transform.RotateAroundLocal(Vector3.up, 0.5f);
            yield return new WaitForSeconds(0.5f);
            Notify(Event.PLAYER_DEATH);
        }
    }

    enum PlayerState
    {
        Idle,
        Walking,
        Jumping,
        Landing,
        Ducking,
        Death
    }
}