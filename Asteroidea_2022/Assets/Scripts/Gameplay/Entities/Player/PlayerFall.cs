using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerFall : MonoBehaviour
    {
        protected bool isFalling = false;

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Floor")) isFalling = true;
        }
    }
}