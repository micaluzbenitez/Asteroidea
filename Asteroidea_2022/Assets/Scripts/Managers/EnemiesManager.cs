using System;
using UnityEngine;
using Toolbox;
using Toolbox.Pool;

namespace Managers
{
    public class EnemiesManager : SpawnManager
    {
        [Serializable] public class Enemy
        {
            public string name = "";
            public bool startEnemy = false;
            public float timePerEnemy = 0;
            [NonSerialized] public Timer timer = new Timer();
        }

        [Header("Enemies")]
        [SerializeField] private Enemy[] enemy = null;

        private void Start()
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i].startEnemy) SpawnObject(enemy[i].name);
                enemy[i].timer.SetTimer(enemy[i].timePerEnemy, Timer.TIMER_MODE.DECREASE, true);
            }
        }

        private void Update()
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                CheckTimer(enemy[i].timer, enemy[i].name);
            }
        }
    }
}