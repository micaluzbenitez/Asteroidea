using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Background
{
    public class BackgroundEntity : MonoBehaviour
    {
        [Header("Movement")]
        public bool rightDirection = false;
        public float speed = 0;

        private SpriteRenderer spriteRenderer = null;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();   
        }

        private void Update()
        {
            if (rightDirection)
            {
                if (transform.localPosition.x < 1)
                    transform.localPosition += new Vector3(speed, 0 ,0) * Time.deltaTime;
            }
            else
            {
                if (transform.localPosition.x > -1)
                    transform.localPosition -= new Vector3(speed, 0, 0) * Time.deltaTime;
            }
        }

        public void Init()
        {
            if (rightDirection)
            {
                transform.localPosition = new Vector3(-1, transform.localPosition.y, transform.localPosition.z);
                spriteRenderer.flipX = true;
            }
            else
            {
                transform.localPosition = new Vector3(1, transform.localPosition.y, transform.localPosition.z);
                spriteRenderer.flipX = false;
            }
        }
    }
}