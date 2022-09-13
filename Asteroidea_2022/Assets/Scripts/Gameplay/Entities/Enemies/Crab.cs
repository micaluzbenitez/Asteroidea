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
        [SerializeField] private float minSpeed = 0.5f;
        [SerializeField, Range(0.5f,2)] private float maxSpeed = 1;

        private bool movingRight = false;
        private float moveTime;
        private SpriteRenderer spriteRenderer;

        protected override void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            base.Awake();
            SetMovingSide();
        }

        private void OnEnable() //Hacer cuando se resetea la plataforma y no cuando se hace enable
        {//Hacer un trigger al final de las plataformas y hacerlo girar ahí :D
            SetMovingSide();
            speedX = Random.Range(minSpeed, maxSpeed);
            spriteRenderer.color = Random.ColorHSV();
        }

        protected override void Update()
        {

            float delTime = Time.deltaTime;

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

        private bool SetMovingSide()
        {
            return Random.Range(0, 2) == 0; //Random entre 0 y 1
        }

    }
}