using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerFeet : MonoBehaviour
    {
        public PlayerMovement playerMovement = null;

        private Animator animator = null;
        public bool collidingFloor = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (playerMovement.HorizontalInput != 0)
            {
                if (collidingFloor) animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Floor")) collidingFloor = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Floor")) collidingFloor = false;
        }
    }
}