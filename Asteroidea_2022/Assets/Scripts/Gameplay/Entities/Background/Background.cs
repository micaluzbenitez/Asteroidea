using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Walls
{
    public class Background : MonoBehaviour
    {
        [Header("Background data")]
        [SerializeField] private float maxPosition = 0;
        [SerializeField] private float resetPosition = 0;
        [SerializeField] private float speed = 0;

        private void Update()
        {
            VerticalMovement();
        
            if (transform.localPosition.y > maxPosition) Reset();
        }
        
        private void VerticalMovement()
        {
            Vector3 pos = transform.localPosition;
            pos.y += speed * Time.deltaTime;
            transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
        }
        
        private void Reset()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, resetPosition, transform.localPosition.z);
        }
    }
}