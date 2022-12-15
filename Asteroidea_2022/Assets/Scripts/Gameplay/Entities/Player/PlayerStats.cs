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
            DAMAGE, 
            DEAD
        }

        [Header("State")]
        [SerializeField] private STATE playerState = STATE.IDLE;
        [SerializeField] private SpriteRenderer expression = null;
        [SerializeField] private Sprite[] expressions = null;
        [SerializeField] private bool onTutorial = false;

        [Header("Player data")]
        [SerializeField] private int initialLife = 0;
        [SerializeField] private float recuperateLifeValue = 0;
        [SerializeField] private float recuperateLifeSpeed = 0;

        [Header("Pickups")]
        [SerializeField] private int pickupScore = 0;
        [SerializeField] private int coinsValue = 0;

        private float life = 0;
        private DeathChecker deathChecker = null;

        public static Action<float, float, float> OnUpdateLife;
        public static Action<int> OnAddScore;
        public static Action<int> OnAddCoins;

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
            if (life <= 0)
            {
                deathChecker.DeadPlayer();
                ChangePlayerState(STATE.DEAD);
            }
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
                WwiseInterface.ExecuteWwiseEvent(WwiseInterface.WwiseEvents.Hit_Pick_Up);
                if (!onTutorial)
                {
                    OnAddScore?.Invoke(pickupScore);
                }
                else
                {
                    OnAddScore?.Invoke(0);
                }
                collision.gameObject.SetActive(false);
            }

            if (collision.gameObject.CompareTag("Coin"))
            {
                WwiseInterface.ExecuteWwiseEvent(WwiseInterface.WwiseEvents.Hit_Coin);
                if (!onTutorial)
                {
                    OnAddCoins?.Invoke(coinsValue);
                }
                else
                {
                    OnAddCoins?.Invoke(0);
                }
                collision.gameObject.SetActive(false);
            }
        }
    }
}