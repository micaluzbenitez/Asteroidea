using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Walls
{
    public enum WallType
    {
        COMMON,
        LAVA
    }
    public class Wall : MonoBehaviour
    {
        [Header("Wall data")]
        [SerializeField] private SpriteRenderer sprite = null;
        [SerializeField] private float maxPosition = 10.7f;
        [SerializeField] private float resetPosition = -10.7f;
        [SerializeField] private int lavaDamage = 0;
        [SerializeField] private string wallTag = "";
        [SerializeField] private string lavaWallTag = "";
        [SerializeField] private bool forceLavaWall = false;

        [Header("Assets")]
        [SerializeField] private Sprite[] commonWalls = null;
        [SerializeField] private Sprite[] lavaWalls = null;
        [SerializeField] private float lavaSpawnRate = 0.07f;

        [Header("Tutorial")]
        [SerializeField] private bool isTutorial = false;

        private bool forcedToLava = false;

        private void Start()
        {
            RandomAsset();
        }

        private void Update()
        {
            if (!isTutorial)
            {
                VerticalMovement();
                if (transform.localPosition.y > maxPosition) Reset();
            }

            CheckForcingLavaWall();
        }

        private void VerticalMovement()
        {
            float speed = Managers.GameManager.VerticalSpeed;
            Vector3 pos = transform.localPosition;
            pos.y += speed * Time.deltaTime;
            transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
        }

        private void Reset()
        {
            transform.localPosition = new Vector3(transform.localPosition .x, resetPosition, transform.localPosition.z);
            RandomAsset();
        }

        private void RandomAsset()
        {
            bool lavaWall = Random.Range(0.0f, 1.0f) < lavaSpawnRate;

            if (lavaWall || forceLavaWall)
            {
                int index = Random.Range(0, lavaWalls.Length);
                sprite.sprite = lavaWalls[index];
                tag = lavaWallTag;
                forcedToLava = true;
            }
            else
            {
                int index = Random.Range(0, commonWalls.Length);
                sprite.sprite = commonWalls[index];
                tag = wallTag;
            }
        }

        public int GetDamage()
        {
            return lavaDamage;
        }

        private void CheckForcingLavaWall()
        {
            if (!forcedToLava)
            {
                if (forceLavaWall)
                {
                    int index = Random.Range(0, lavaWalls.Length);
                    sprite.sprite = lavaWalls[index];
                    tag = lavaWallTag;
                    forcedToLava = true;
                }
            }
            else
            {
                if(!forceLavaWall)
                {
                    int index = Random.Range(0, commonWalls.Length);
                    sprite.sprite = commonWalls[index];
                    tag = wallTag;
                    forcedToLava = false;
                }
            }
        }

    }
}