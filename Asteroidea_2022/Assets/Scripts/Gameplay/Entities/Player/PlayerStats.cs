using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerStats : MonoBehaviour
    {
        public enum STATE
        {
            IDLE,
            WALKING,
            FALLING,
            DAMAGE
        }

        [Header("State")]
        [SerializeField] private STATE playerState = STATE.IDLE;
        [SerializeField] private SpriteRenderer expression = null;
        [SerializeField] private Sprite[] expressions = null;

        [Header("Player data")]
        [SerializeField] private int initialLife = 0;
        [SerializeField] private float recuperateLifeValue = 0;
        [SerializeField] private float recuperateLifeSpeed = 0;

        [Header("Pickups points")]
        [SerializeField] private int pickupScore = 0;

        private float life = 0;
        private DeathChecker deathChecker = null;

        public static Action<float, float, float> OnUpdateLife;
        public static Action<int> OnAddScore;

        private void Awake()
        {
            deathChecker = GetComponent<DeathChecker>();
            life = initialLife;
        }

        private void Update()
        {
            if (Time.timeScale == 1) RecuperateLife();
        }

        private void RecuperateLife()
        {
            if (life < initialLife)
            {
                life += recuperateLifeValue * recuperateLifeSpeed;
                OnUpdateLife?.Invoke(life, 0, initialLife);
            }
        }

        public void LoseLife(int damage)
        {
            life -= damage;
            OnUpdateLife?.Invoke(life, 0, initialLife);
            if (life <= 0) deathChecker.DeadPlayer();
        }

        public void ChangePlayerState(STATE state)
        {
            playerState = state;
            expression.sprite = expressions[(int)state];
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Pickup"))
            {
                OnAddScore(pickupScore);
                collision.gameObject.SetActive(false);
            }
        }
    }
}