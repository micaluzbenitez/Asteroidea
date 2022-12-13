using System;
using UnityEngine;
using UnityEngine.Events;
using Entities.Enemies;
using Objects;
using Entities.Walls;

namespace Entities.Player
{
    public class PlayerEnemies : MonoBehaviour
    {
        [Header("Obstacles tags")]
        public string enemiesTag = "";
        public string bulletsTag = "";
        public string lavaTag = "";

        [Header("Enemy feedback")]
        [SerializeField] private GameObject enemiesParticles = null;

        [Header("Unity events")]
        [SerializeField] private UnityEvent OnCollideEnemy = null;

        private PlayerStats playerStats = null;
        private bool damage = false;

        public static Action<int> OnLoseLife = null;

        private void Awake()
        {
            playerStats = GetComponent<PlayerStats>();
        }

        private void PlayerDamage(int damage)
        {
            Instantiate(enemiesParticles, transform.position, Quaternion.identity);
            playerStats.ChangePlayerState(PlayerStats.STATE.DAMAGE);
            OnLoseLife?.Invoke(damage);
            OnCollideEnemy?.Invoke();
            this.damage = true;
        }

        public bool GetDamageState()
        {
            return damage;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(lavaTag))
            {
                WwiseInterface.ExecuteWwiseEvent(WwiseInterface.WwiseEvents.Player_Damage, this.gameObject);
                Wall wall = collision.gameObject.GetComponent<Wall>();
                PlayerDamage(wall.GetDamage());
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(enemiesTag))
            {
                WwiseInterface.ExecuteWwiseEvent(WwiseInterface.WwiseEvents.Player_Damage, this.gameObject);
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                PlayerDamage(enemy.GetDamage());
                enemy.PlaySound();
                if (collision.gameObject.name == "Mine")
                {
                    Destroy(collision.gameObject);
                }
            }

            if (collision.gameObject.CompareTag(bulletsTag))
            {
                WwiseInterface.ExecuteWwiseEvent(WwiseInterface.WwiseEvents.Player_Damage, this.gameObject);
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                PlayerDamage(bullet.Damage);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(enemiesTag)) damage = false;
            if (collision.gameObject.CompareTag(lavaTag)) damage = false;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(bulletsTag)) damage = false;
        }
    }
}