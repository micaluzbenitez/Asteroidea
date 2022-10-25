using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Walls
{
    public class Wall : MonoBehaviour
    {
        [Header("Wall data")]
        [SerializeField] private SpriteRenderer sprite = null;
        [SerializeField] private float maxPosition = 10.7f;
        [SerializeField] private float resetPosition = -10.7f;

        [Header("Assets")]
        [SerializeField] private Sprite[] assets = null;

        private void Start()
        {
            RandomAsset();
        }

        private void Update()
        {
            VerticalMovement();

            if (transform.localPosition.y > maxPosition) Reset();
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
            int index = Random.Range(0, assets.Length);
            sprite.sprite = assets[index];
        }
    }
}