using System;
using UnityEngine;
using UnityEngine.Events;
using Entities.Enemies;
using Entities.Enemies.Objects;

namespace Entities.Player
{
    public class PlayerEnemies : MonoBehaviour
    {
        [Header("Obstacles tags")]
        public string enemiesTag = "";
        public string bulletsTag = "";

        [Header("Unity events")]
        [SerializeField] private UnityEvent OnCollideEnemy = null;

        private PlayerStats playerStats = null;

        public static Action<int> OnLoseLife = null;

        private void Awake()
        {
            playerStats = GetComponent<PlayerStats>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(enemiesTag))
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                OnLoseLife?.Invoke(enemy.GetDamage());
                OnCollideEnemy?.Invoke();
                playerStats.ChangePlayerState(PlayerStats.STATE.DAMAGE);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(bulletsTag))
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                OnLoseLife?.Invoke(bullet.Damage);
                OnCollideEnemy?.Invoke();
                playerStats.ChangePlayerState(PlayerStats.STATE.DAMAGE);
            }
        }
    }
}