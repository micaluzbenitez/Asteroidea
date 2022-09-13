using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Player data")]
        [SerializeField] private int initialLife = 0;

        public static Action<float, float, float> OnLoseLife;

        public int life = 0;

        private DeathChecker deathChecker = null;

        private void Awake()
        {
            deathChecker = new DeathChecker();
            life = initialLife;
        }

        public void LoseLife(int damage)
        {
            life -= damage;
            OnLoseLife?.Invoke(life, 0, initialLife);
            if (life <= 0) deathChecker.DeadPlayer();
        }
    }
}