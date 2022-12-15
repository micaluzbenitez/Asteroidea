using UnityEngine;
using Toolbox;
using Objects;

namespace Entities.Enemies
{
    public class Jellyfish : Enemy
    {
        [Header("Jellyfish data")]
        [SerializeField] private Animator feetsAnimator = null;
        [SerializeField] private Bullet bullet = null;

        [Tooltip("Target to shoot")]
        [SerializeField] private Transform target = null;

        [Header("Shot data")]
        [SerializeField] private int timePerShot = 0;

        [Header("Reset trigger")]
        [SerializeField] private string triggerTagName = "";

        [Header("Tutorial")]
        [SerializeField] private bool onTutorial = false;

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
            UpdateTimerPerShot();
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

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.CompareTag(triggerTagName))
            {
                if (!onTutorial)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, -2, 0);
                }
            }
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision); 
            if (collision.gameObject.CompareTag("Player")) feetsAnimator.Play("Attack");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.position);
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
        public override void PlaySound()
        {
            WwiseInterface.ExecuteWwiseEvent(WwiseInterface.WwiseEvents.Hit_Jelly);
        }

    }
}