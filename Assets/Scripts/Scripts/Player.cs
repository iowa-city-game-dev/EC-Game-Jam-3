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
        BoxCollider2D col;
        Rigidbody2D rb;

        private PlayerState _state;

        //physics
        public float speed = 5f;
        public float jumpHeight = 1000f;
        float maxAcceleration = 10f;

        bool isGrounded;
        bool isPaused;

        public LayerMask platformLayer;
        public Transform groundCheck;

        private void Start()
        {
            _state = PlayerState.Idle;
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<BoxCollider2D>();

            col.enabled = true;
            isGrounded = true;
            isPaused = false;
        }

        private void FixedUpdate()
        {
            if (!isPaused)
            {
                HandlePlayerState();
            }
        }

        private void Update()
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, platformLayer);
            if (hit.transform && hit.transform.CompareTag("Spikes"))
            {
                //watch out for spikes
                _state = PlayerState.Death;
            }
            else if (hit.transform && hit.transform.CompareTag("Goal"))
            {
                _state = PlayerState.PlayerWin;
            }
        }

        private void HandlePlayerState()
        {
            switch (_state)
            {
                case PlayerState.Idle:
                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                    {
                        //handle Idle State Change
                        _state = PlayerState.Walking;
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow) && rb.velocity.y == 0f)
                    {
                        rb.AddForce(new Vector2(0, jumpHeight));
                        isGrounded = false;
                        _state = PlayerState.Jumping;
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        _state = PlayerState.Ducking;
                    }

                    break;

                case PlayerState.Walking:
                    //handle walking State Change
                    if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                    {
                        _state = PlayerState.Idle;
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow) && rb.velocity.y == 0f)
                    {
                        rb.AddForce(new Vector2(0, jumpHeight));
                        isGrounded = false;
                        _state = PlayerState.Jumping;
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        _state = PlayerState.Ducking;
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
                        if (hit.collider.transform.CompareTag(("Platforms")))
                        {
                            isGrounded = true;
                        }
                    }

                    //handle Jumping State Change
                    if (isGrounded)
                    {
                        _state = PlayerState.Idle;
                    }

                    break;

                case PlayerState.Landing:
//handle Landing State Change
                    _state = PlayerState.Idle;

                    break;

                case PlayerState.Ducking:
                    //handle Ducking State Change
                    if (Input.GetKeyUp(KeyCode.DownArrow))
                    {
                        _state = PlayerState.Idle;
                    }

//handle Ducking code
                    break;

                case PlayerState.Death:
                    Debug.Log("player dead");
                    StartCoroutine(HandlePlayerDeath());
                    break;

                case PlayerState.PlayerWin:
                    Debug.Log("*** Player Win ***");
                    StartCoroutine(HandlePlayerWin());
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

        IEnumerator HandlePlayerWin()
        {
            col.enabled = false;
            isPaused = true;
            
            yield return new WaitForSeconds(0.5f);
            Notify(Event.PLAYER_WIN);
        }
    }

    enum PlayerState
    {
        Idle,
        Walking,
        Jumping,
        Landing,
        Ducking,
        Death,
        PlayerWin
    }
}