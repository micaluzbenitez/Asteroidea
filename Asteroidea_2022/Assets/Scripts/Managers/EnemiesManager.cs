using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;
using Toolbox.Pool;

namespace Managers
{
    public class EnemiesManager : MonoBehaviour
    {
        [Header("Enemies spawn")]
        [SerializeField] private float initialYPosition = 0;
        [SerializeField] private float timePerFish = 0;
        [SerializeField] private float timePerMine = 0;

        private ObjectPooler objectPooler = null;

        /// Timers
        private Timer fishTimer = new Timer();
        private Timer mineTimer = new Timer();

        private void Awake()
        {
            fishTimer.SetTimer(timePerFish, Timer.TIMER_MODE.DECREASE, true);
            mineTimer.SetTimer(timePerMine, Timer.TIMER_MODE.DECREASE, true);
        }

        private void Start()
        {
            objectPooler = ObjectPooler.Instance;            
        }

        private void Update()
        {
            CheckTimer(fishTimer, "Fish");
            CheckTimer(mineTimer, "Mine");
        }

        private void CheckTimer(Timer timer, string enemyName)
        {
            if (timer.Active) timer.UpdateTimer();

            if (timer.ReachedTimer())
            {
                Vector3 position = new Vector3(0, initialYPosition, 0);
                objectPooler.SpawnFromPool(enemyName, position, Quaternion.identity);
                timer.ActiveTimer();
            }
        }
    }
}