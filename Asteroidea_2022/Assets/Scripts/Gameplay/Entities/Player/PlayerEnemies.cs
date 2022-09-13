using System;
using System.Collections.Generic;
using UnityEngine;
using Entities.Enemies;
using Entities.Enemies.Objects;

namespace Entities.Player
{
    public class PlayerEnemies : MonoBehaviour
    {
        [Header("Obstacles tags")]
        public string enemiesTag = "";
        public string bulletsTag = "";

        public Action<int> OnLoseLife = null;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(enemiesTag))
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                OnLoseLife?.Invoke(enemy.GetDamage());
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(bulletsTag))
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                OnLoseLife?.Invoke(bullet.Damage);
            }
        }
    }
}