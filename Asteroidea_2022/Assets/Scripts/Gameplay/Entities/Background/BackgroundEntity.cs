using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
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

        [Header("Light")]
        [SerializeField] private Light2D entityLight = null;
        [SerializeField] private float rightPosition = 0;
        [SerializeField] private float leftPosition = 0;

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

            if (rightDirection)
            {
                spriteRenderer.flipX = true;
                if (entityLight.enabled) 
                    entityLight.transform.localPosition = new Vector2(rightPosition, entityLight.transform.localPosition.y);
            }
            else
            {
                spriteRenderer.flipX = false;
                if (entityLight.enabled) 
                    entityLight.transform.localPosition = new Vector2(leftPosition, entityLight.transform.localPosition.y);
            }
        }
    }
}