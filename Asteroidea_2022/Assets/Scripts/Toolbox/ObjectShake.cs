using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Toolbox
{
    public class ObjectShake : MonoBehaviour
    {
        [Header("Shake data")]
        public bool regularShake = true;
        public bool startRightDireccion = true;
        public float timePerShake = 0;
        public float duration = 0;
        public float rangeX = 0;
        public float rangeY = 0;
        public float magnitude = 0;

        private bool shake = false;
        private Vector2 originalPosition = Vector2.zero;
        private bool rightXShake = false;
        private float timeForShake = 0;
        private float timeForDuration = 0;

        public Action OnFinishShake;

        private void Start()
        {
            if (startRightDireccion) rightXShake = true;
            else rightXShake = false;
            originalPosition = transform.position;
        }

        private void Update()
        {
            if (shake)
            {
                timeForDuration += Time.deltaTime;
                Shake();
            }
        }

        public void Shake()
        {
            if (timeForDuration < duration)
            {          
                if (timeForShake > timePerShake)
                {
                    float x = 0;
                    float y = 0;

                    if (regularShake)
                    {
                        if (rightXShake)
                        {
                            x = (transform.position.x + rangeX) * magnitude;
                            y = (transform.position.y + rangeY) * magnitude;                            
                        }
                        else
                        {
                            x = (transform.position.x - rangeX) * magnitude;
                            y = (transform.position.y - rangeY) * magnitude;
                        }

                        rightXShake = !rightXShake;
                    }
                    else
                    {
                        x = Random.Range(transform.position.x - rangeX, transform.position.x + rangeX) * magnitude;
                        y = Random.Range(transform.position.y - rangeY, transform.position.y + rangeY) * magnitude;
                    }

                    transform.position = new Vector2(x, y);
                    timeForShake = 0;
                }
                else
                    timeForShake += Time.deltaTime;
            }
            else
            {
                shake = false;
                transform.position = originalPosition;
                timeForDuration = 0;
                OnFinishShake?.Invoke();
            }
        }

        public void StartShake()
        {
            shake = true;
        }
    }
}