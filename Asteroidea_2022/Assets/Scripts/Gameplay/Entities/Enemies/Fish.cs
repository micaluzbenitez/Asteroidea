using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Enemies
{
    public class Fish : Enemy
    {
        [Header("Up movement")]
        [SerializeField] private float maxYPosition = 0;

        protected override void Update()
        {
            base.Update();
            CheckUpLimit();
        }

        private void CheckUpLimit()
        {
            if (transform.position.y > maxYPosition) gameObject.SetActive(false);
        }
    }
}