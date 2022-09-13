using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Enemies.Objects
{
    public class Bullet : MonoBehaviour
    {
        [Tooltip("Shot movement speed")]
        [SerializeField] private float shotSpeed = 0;

        private int damage = 0;
        private Rigidbody2D rigidBody = null;

        ///  Properties
        public int Damage { get => damage; set => damage = value; }

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            gameObject.SetActive(false);
        }

        public void Shot(Vector2 target)
        {
            rigidBody.AddForce(target * shotSpeed, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Enemy"))
            {
                transform.localPosition = Vector2.zero;
                gameObject.SetActive(false);
            }
        }
    }
}