using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

namespace Entities.Platforms
{
    public class PlatformController : MonoBehaviour
    {
        #region VARIABLES
        #region SERIALIZED VARIABLES

        [Header("Platform Speed Control")]
        [SerializeField] private float startingPlatformVerticalSpeed = 1.0f;
        [SerializeField] private float timeForNextAugment = 5;
        [SerializeField] private float augmentDuration = 3;
        [SerializeField] private float augmentValue = 0.001f;


        [Header("Obstacle Spawn")]
        [SerializeField] private float startingObstacleSpawnRate = 0.1f;
        [SerializeField] private float spawnRateAugment = 0.02f;


        [Header("Platform Limits")]
        [SerializeField] private GameObject leftWall = null;
        [SerializeField] private GameObject rightWall = null;


        #endregion

        #region STATIC VARIABLES

        private static float platformVerticalSpeed;
        private static Limits platformHorizontalLimits;

        #endregion

        #region PRIVATE VARIABLES

        #endregion

        #region PROPERTIES
        public static float PlatformVerticalSpeed
        {
            get
            {
                return platformVerticalSpeed;
            }
            private set
            {
                platformVerticalSpeed = value;
            }
        }

        public static float ObstacleSpawnRate { get; private set; }

        public struct Limits
        {
            public float Left; public float Right;
        }
        public static Limits HorizontalLimits
        {
            get
            {
                return platformHorizontalLimits;
            }
            private set
            {
                platformHorizontalLimits = value;
            }
        }

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
            platformHorizontalLimits.Left = leftWall.transform.position.x + leftWall.GetComponent<BoxCollider2D>().bounds.size.x / 2;
            platformHorizontalLimits.Right = rightWall.transform.position.x - rightWall.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        }

        private void Start()
        {
            PlatformVerticalSpeed = startingPlatformVerticalSpeed;
            ObstacleSpawnRate = startingObstacleSpawnRate;
            StartCoroutine(SecondsTimer());


        }

        IEnumerator SecondsTimer()
        {
            StartCoroutine(SpeedAugment());
            yield return new WaitForSeconds(timeForNextAugment);
        }
        IEnumerator SpeedAugment()
        {
            float t = 0;
            while (t < augmentDuration)
            {
                t += Time.deltaTime;
                platformVerticalSpeed += augmentValue;
                yield return null;
            }
            if (GameManager.GameRunning)
            {
                StartCoroutine(SecondsTimer());
                if (ObstacleSpawnRate < 1)
                    ObstacleSpawnRate += spawnRateAugment;
            }

        }

        #endregion
        #endregion
    }
}