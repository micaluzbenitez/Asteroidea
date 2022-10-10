using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Managers;

namespace Entities.Platforms
{
    public class PlatformController : MonoBehaviour
    {
        #region VARIABLES
        #region SERIALIZED VARIABLES

        [Header("Reset Control")]
        [SerializeField] private int secondsForPlatformRespawn = 1;
        [SerializeField] private Platform startingPlatform = null;

        [Header("Obstacle Spawn")]
        [SerializeField] private float startingObstacleSpawnRate = 0.1f;
        [SerializeField] private float spawnRateAugment = 0.02f;
        [SerializeField] private float timeForNextAugment = 5;
        [SerializeField] private float augmentDuration = 3;


        [Header("Platform Limits")]
        [SerializeField] private GameObject leftWall = null;
        [SerializeField] private GameObject rightWall = null;


        #endregion

        #region STATIC VARIABLES

        private static float platformVerticalSpeed;
        private static Limits platformHorizontalLimits;
        private int nextPlatformIndexToSpawn = 0;

        public static Action OnPlatformMustSpawn;

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

        #region PRIVATE VARIABLES

        private Platform[] platformList = null;
        private float time = 0;

        #endregion

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
            platformList = GetComponentsInChildren<Platform>();
            foreach (Platform platform in platformList)
            {
                platform.gameObject.SetActive(platform.name.Equals(startingPlatform.name));
            }
                
            
        }

        private void Start()
        {
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
                yield return null;
            }
            if (GameManager.GameRunning)
            {
                StartCoroutine(SecondsTimer());
                if (ObstacleSpawnRate < 1)
                    ObstacleSpawnRate += spawnRateAugment;
            }

        }

        private void Update()
        {
            if (PauseSystem.Paused) return;
            float maxSpawnTime = GameManager.GetMaxVerticalSpeed();
            time += Time.deltaTime;
            if (time > secondsForPlatformRespawn - (GameManager.VerticalSpeed * 2 / maxSpawnTime))
            {
                platformList[nextPlatformIndexToSpawn].gameObject.SetActive(true);
                platformList[nextPlatformIndexToSpawn].ResetPlatform();
                nextPlatformIndexToSpawn++;
                if (nextPlatformIndexToSpawn == platformList.Length)
                {
                    nextPlatformIndexToSpawn = 0;
                }
                time = 0;
            }

        }

        #endregion
        #endregion
    }
}