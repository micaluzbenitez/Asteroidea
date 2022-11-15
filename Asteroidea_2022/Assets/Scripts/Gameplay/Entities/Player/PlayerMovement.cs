using System;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerMovement : PlayerFall
    {
        [Header("Move data"), Tooltip("Horizontal movement speed")]
        [SerializeField] private float moveSpeed = 2f;

        [Header("Jump data"), Tooltip("Jump speed")]
        [SerializeField] private float jumpUpSpeed = 5f;
        [Tooltip("Number of jumps allowed in the air")]
        [SerializeField] private int allowJumpTimesOnAir = 0;

        [SerializeField] private Joystick joystick = null;

        private float horizontalInput = 0;
        private bool faceRight = true;
        private bool jumpPressed = false;
        private int airJumpCount = 0;

        private PlayerStats playerStats = null;
        private PlayerEnemies playerEnemies = null;
        private Rigidbody2D rigidBody = null;

        // Properties
        public float HorizontalInput { get => horizontalInput; }

        private void Awake()
        {
            playerStats = GetComponent<PlayerStats>();
            playerEnemies= GetComponent<PlayerEnemies>();
            rigidBody = GetComponent<Rigidbody2D>();
            Debug.Log(Application.platform.ToString());
        }

        private void Update()
        {
            if(Application.platform == RuntimePlatform.Android)
            {
                /// Move
                horizontalInput = joystick.Horizontal;
                /// Jump
                jumpPressed = joystick.Vertical >= 0.97f;
            }
            
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            {
                /// Move
                horizontalInput = Input.GetAxisRaw("Horizontal");
                /// Jump
                if (Input.GetButtonDown("Jump")) jumpPressed = true;
            }
        }

        private void FixedUpdate()
        {
            Move();
            CheckJump();
            PlayerExpression();
        }

        private void Move()
        {
            rigidBody.velocity = new Vector2(horizontalInput * moveSpeed, rigidBody.velocity.y);
            if (horizontalInput > 0 && !faceRight || horizontalInput < 0 && faceRight) Flip();
        }

        private void CheckJump()
        {
            if (jumpPressed)
            {
                if (isGrounded) /// Ground jump
                {
                    Jump();
                }
                else
                {
                    if (airJumpCount < allowJumpTimesOnAir) /// Air jump
                    {
                        Jump();
                        airJumpCount++;
                    }
                }
            }
        }

        private void Jump()
        {
            rigidBody.velocity = Vector2.up * jumpUpSpeed;
            jumpPressed = false;
            isGrounded = false;
        }

        private void Flip()
        {
            faceRight = !faceRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        private void PlayerExpression()
        {
            if (!playerEnemies.GetDamageState())
            {
                if (!isGrounded && isFalling) playerStats.ChangePlayerState(PlayerStats.STATE.FALLING);
                else if (horizontalInput != 0) playerStats.ChangePlayerState(PlayerStats.STATE.WALKING);
                else playerStats.ChangePlayerState(PlayerStats.STATE.IDLE);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                isGrounded = true;
                airJumpCount = 0;
                isFalling = false;
            }
        }
    }
}