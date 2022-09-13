using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Platforms
{
    public class Platform : MonoBehaviour
    {
        #region VARIABLES
        #region SERIALIZED VARIABLES
        [Header("Lateral Limits")]
        [SerializeField] private float minX = -2.75f;
        [SerializeField] private float maxX = 2.75f;

        [Header("Obstacle Spawn")]
        [SerializeField] private GameObject obstacle;

        [Header("Horizontal movement")]
        [SerializeField] private bool horizontalMovement = false;
        [SerializeField] private float horizontalSpeed = 0;
        [SerializeField] private float maxXLimit = 0;
        [SerializeField] private float minXLimit = 0;
        #endregion

        #region STATIC VARIABLES
        #endregion

        #region PUBLIC VARIABLES
        #endregion

        #region PRIVATE VARIABLES
        private float resetYPos = -5.5f;
        private Rigidbody2D rb;

        private float distanceToObstacle = 0.0f;
        #endregion
        #endregion

        #region METHODS
        #region PUBLIC METHODS

        #endregion

        #region STATIC METHODS

        #endregion

        #region PRIVATE METHODS
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            distanceToObstacle = Vector3.Distance(obstacle.transform.position, transform.position);
        }

        private void Start()
        {
            if (!horizontalMovement) horizontalSpeed = 0;
            SetRandomX();
        }

        private void LateUpdate()
        {
            Movement();
        }        

        private void SetRandomX()
        {
            float newX = minX + UnityEngine.Random.Range(0, (Mathf.Abs(minX - maxX)));
            transform.position = new Vector3(newX, transform.position.y);
        }

        private void ResetPosition()
        {
            obstacle.SetActive(false);
            SetRandomX();
            transform.position = new Vector3(transform.position.x, resetYPos);
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            float rand = UnityEngine.Random.Range(0.0f, 1.0f);
            Debug.Log("Random: " + rand + "/ SpawnRate: " + PlatformController.ObstacleSpawnRate);
            if (PlatformController.ObstacleSpawnRate > 1.0f || rand < PlatformController.ObstacleSpawnRate)
            {
                obstacle.transform.position = new Vector3(transform.position.x, resetYPos + distanceToObstacle);
                obstacle.SetActive(true);
            }
        }

        private void Movement()
        {
            float newX = transform.position.x + horizontalSpeed * Time.deltaTime;
            float newY = transform.position.y + PlatformController.PlatformVerticalSpeed * Time.deltaTime;
            rb.MovePosition(new Vector2(newX, newY));

            CheckHorizontalLimits();
        }

        private void CheckHorizontalLimits()
        {
            if (horizontalMovement)
            {
                if (transform.position.x < minXLimit || transform.position.x > maxXLimit)
                    Turn();
            }
        }

        private void Turn()
        {
            if (horizontalMovement) horizontalSpeed *= -1;

            /// Fixed turn
            float offset = 0.01f;
            if (horizontalSpeed > 0) transform.position = new Vector2(minXLimit + offset, transform.position.y);
            else transform.position = new Vector2(maxXLimit - offset, transform.position.y);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals("ResetZone"))
            {
                ResetPosition();
            }
        }
        #endregion
        #endregion
    }
}