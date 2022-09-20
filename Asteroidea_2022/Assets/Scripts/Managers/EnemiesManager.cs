using System;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;
using Toolbox.Pool;

namespace Managers
{
    public class EnemiesManager : MonoBehaviour
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

        [Header("Enemies spawn")]
        [SerializeField] private Transform cameraPosition = null;
        [SerializeField] private float cameraYOffset = 0;

        private ObjectPooler objectPooler = null;

        private void Start()
        {
            objectPooler = ObjectPooler.Instance;

            for (int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i].startEnemy) SpawnEnemy(enemy[i].name);
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

        private void CheckTimer(Timer timer, string enemyName)
        {
            if (timer.Active) timer.UpdateTimer();

            if (timer.ReachedTimer())
            {
                SpawnEnemy(enemyName);
                timer.ActiveTimer();
            }
        }

        private void SpawnEnemy(string enemyName)
        {
            Vector3 position = new Vector3(0, cameraPosition.position.y - cameraYOffset, 0);
            objectPooler.SpawnFromPool(enemyName, position, Quaternion.identity);
        }
    }
}