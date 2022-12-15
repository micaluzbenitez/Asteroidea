using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Entities.Enemies
{
    public class Crab : Enemy
    {
        [Header("Lateral Limits")]
        [SerializeField] private Transform leftLimit;
        [SerializeField] private Transform rightLimit;

        [Header("Configuration")]
        [SerializeField] private bool startMovingRight;
        [SerializeField] private float minSpeed = 0.1f;
        [SerializeField, Range(0.0f,2.0f)] private float maxSpeed = 1;

        private bool movingRight = false;
        private float moveTime = 0;
        private float speed = 0;

        protected override void Awake()
        {
            base.Awake();
            SetMovingSide();
        }

        private void OnEnable() 
        {
            SetMovingSide();
            speed = Random.Range(minSpeed, maxSpeed);
        }

        protected override void Update()
        {

            float delTime = Time.deltaTime * speed;

            moveTime += movingRight ? delTime : -delTime ;

            rigidBody.MovePosition(Vector2.Lerp(leftLimit.position, rightLimit.position, moveTime));

            if (moveTime < 0)
            {
                movingRight = true;
                moveTime = 0;
            }
            else if(moveTime > 1) //si completa el lerp
            {
                movingRight = false;
                moveTime = 1;
            }
        }

        public override void PlaySound()
        {
            WwiseInterface.ExecuteWwiseEvent(WwiseInterface.WwiseEvents.Hit_Crab);
        }

        private void SetMovingSide()
        {
            movingRight = Random.Range(0, 2) == 0; //Random entre 0 y 1
        }

    }
}