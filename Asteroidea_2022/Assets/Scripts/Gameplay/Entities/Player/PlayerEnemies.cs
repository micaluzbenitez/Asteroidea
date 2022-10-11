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

        [Header("Enemy feedback")]
        [SerializeField] private GameObject enemyParticles = null;

        [Header("Unity events")]
        [SerializeField] private UnityEvent OnCollideEnemy = null;

        private PlayerStats playerStats = null;

        public static Action<int> OnLoseLife = null;

        private void Awake()
        {
            playerStats = GetComponent<PlayerStats>();
        }

        private void PlayerDamage(int damage)
        {
            Instantiate(enemyParticles, transform.position, Quaternion.identity);
            playerStats.ChangePlayerState(PlayerStats.STATE.DAMAGE);
            OnLoseLife?.Invoke(damage);
            OnCollideEnemy?.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(enemiesTag))
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                PlayerDamage(enemy.GetDamage());
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(bulletsTag))
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                PlayerDamage(bullet.Damage);
            }
        }
    }
}