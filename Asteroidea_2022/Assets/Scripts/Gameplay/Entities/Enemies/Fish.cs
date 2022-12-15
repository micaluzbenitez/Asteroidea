using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Enemies
{
    public class Fish : Enemy
    {
        [Header("Reset trigger")]
        [SerializeField] private string triggerTagName = "";

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(triggerTagName)) gameObject.SetActive(false);
        }

        public override void PlaySound()
        {
            WwiseInterface.ExecuteWwiseEvent(WwiseInterface.WwiseEvents.Hit_Fish);
        }

    }
}