using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Background
{
    public class BackgroundEntity : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private bool rightDirection = false;
        [SerializeField] private float speed = 0;
        [SerializeField] private float maxPosition = 0;
        [SerializeField] private float minPosition = 0;

        private SpriteRenderer spriteRenderer = null;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();   
        }

        private void Update()
        {
            if (rightDirection)
            {
                if (transform.localPosition.x < maxPosition)
                    transform.localPosition += new Vector3(speed, 0 ,0) * Time.deltaTime;
            }
            else
            {
                if (transform.localPosition.x > minPosition)
                    transform.localPosition -= new Vector3(speed, 0, 0) * Time.deltaTime;
            }
        }

        public void Init()
        {
            int direction = Random.Range(0, 1);
            rightDirection = direction == 0;

            if (rightDirection) spriteRenderer.flipX = true;
            else spriteRenderer.flipX = false;
        }
    }
}