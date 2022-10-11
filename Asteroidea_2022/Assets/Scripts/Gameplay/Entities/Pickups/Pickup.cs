using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Pickup
{
    public class Pickup : MonoBehaviour
    {
        [Header("Reset trigger")]
        [SerializeField] private string triggerTagName = "";

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(triggerTagName)) gameObject.SetActive(false);
        }
    }
}