using UnityEngine;

namespace Toolbox
{
    public class Timer
    {
        #region PARAMETERS
        // Enum
        public enum TIMER_MODE
        {
            INCREASE,
            DECREASE
        }
        private TIMER_MODE timerMode = TIMER_MODE.DECREASE;

        // The total time of the timer
        private float totalTime = 0;
        // The current time of the timer
        private float currentTime = 0;
        // If the timer is active or not
        private bool active = false;
        // If the timer is was active or not
        private bool wasActive = false;
        // If the timer reached or not
        private bool reached = false;

        // Properties
        public float CurrentTime { get => currentTime; }
        public bool Active { get => active; }
        #endregion

        #region METHODS
        /// <summary>
        /// Call this to set timer values, auto start if you want the timer to start or not yet
        /// </summary>
        public void SetTimer(float time, TIMER_MODE timerMode, bool autoStart = false)
        {
            this.timerMode = timerMode;
            totalTime = time;

            if (this.timerMode == TIMER_MODE.DECREASE) currentTime = time;
            else currentTime = 0;

            ChangeTimerState(autoStart);
        }

        /// <summary>
        /// Call this every time the timer is active
        /// </summary>
        public void UpdateTimer(float speed = 1)
        {
            if (!active) return;

            if (timerMode == TIMER_MODE.DECREASE) CheckTimer(-speed, currentTime <= 0, totalTime);
            else CheckTimer(speed, currentTime >= totalTime, 0);
        }
        
        /// <summary>
        /// Call this if you want to changed time values
        /// </summary>
        public void ResetTimer(float time, TIMER_MODE timerMode, bool autoStart = false)
        {
            SetTimer(time, timerMode, autoStart);
        }

        /// <summary>
        /// Call this to active timer
        /// </summary>
        public void ActiveTimer()
        {
            ResetTimer(totalTime, timerMode, true);
        }

        /// <summary>
        /// Call this to desactive timer
        /// </summary>
        public void DesactiveTimer()
        {
            ChangeTimerState(false);
        }

        /// <summary>
        /// Check the timer already reached
        /// </summary>
        public bool ReachedTimer()
        {
            if (wasActive && reached)
            {
                wasActive = false;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check timer state
        /// </summary>
        private void CheckTimer(float speed, bool condition, float initialTime)
        {
            currentTime += Time.deltaTime * speed;

            if (condition)
            {
                active = false;
                reached = true;
                currentTime = initialTime;
            }
        }

        /// <summary>
        /// Change if the timer is active or not
        /// </summary>
        private void ChangeTimerState(bool state)
        {
            active = state;
            wasActive = state;
            reached = !state;
        }
    }
#endregion
}

/* Testeo timer:
public class test : MonoBehaviour
{
    public Timer.TIMER_MODE timerMode = Timer.TIMER_MODE.DECREASE;
    public float totalTime = 0;

    private Timer timer = new Timer();

    private void Awake()
    {
        timer.SetTimer(totalTime, timerMode);
    }

    private void Update()
    {
        if (timer.Active) timer.UpdateTimer();

        if (Input.GetKeyDown(KeyCode.M)) timer.ActiveTimer();
        if (Input.GetKeyDown(KeyCode.N)) timer.DesactiveTimer();

        Debug.Log(timer.CurrentTime);
    }
}
*/