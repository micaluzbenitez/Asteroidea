using System;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;
using Entities.Enemies.Objects;

namespace Entities.Enemies
{
    public class Mine : Enemy
    {
        [Header("Mine data")]
        [SerializeField] private Bullet bullet = null;
        [Tooltip("Target to shoot")]
        [SerializeField] private Transform target = null;

        [Header("Shot data")]
        [SerializeField] private int timePerShot = 0;

        [Header("Up movement")]
        [SerializeField] private float maxYPosition = 0;

        private Timer timerPerShot = new Timer();

        protected override void Awake()
        {
            base.Awake();
            bullet.Damage = damage;
            timerPerShot.SetTimer(timePerShot, Timer.TIMER_MODE.DECREASE, true);
        }

        protected override void Update()
        {
            base.Update();
            CheckUpLimit();
            UpdateTimerPerShot();
        }

        private void CheckUpLimit()
        {
            if (transform.position.y > maxYPosition) gameObject.SetActive(false);
        }

        private void UpdateTimerPerShot()
        {
            if (timerPerShot.Active) timerPerShot.UpdateTimer();

            if (timerPerShot.ReachedTimer())
            {
                Shot();
                timerPerShot.ActiveTimer();
            }
        }

        private void Shot()
        {
            bullet.gameObject.SetActive(true);
            bullet.Shot(target.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}