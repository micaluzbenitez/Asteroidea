using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolbox.Lerpers
{
    public abstract class Lerper<T>
    {
        #region PARAMETERS
        // Enum
        public enum LERPER_TYPE
        {
            NONE,
            EASE_IN,
            EASE_OUT,
            EXPONENTIAL,
            STEP_SMOOTH,
            STEP_SMOOTHER
        }
        protected LERPER_TYPE lerperType = LERPER_TYPE.NONE;

        // Lerper start and end value
        protected T start;
        protected T end;

        // Lerper time
        protected float time = 0;
        // Lerper actual time
        protected float currentTime = 0;
        // Lerper time in percent
        protected float percentage = 0;
        // Lerper actual value in time
        protected T currentValue;

        // If the lerper is active or not
        private bool active = false;
        // Is the lerper reached or not
        private bool reached = false;

        // Properties
        public bool Active { get => active; }
        public bool Reached { get => reached; }
        #endregion

        #region METHODS
        /// <summary>
        /// Call this to set lerper values, auto start if you want the timer to start or not yet
        /// </summary>
        public void SetValues(float time, LERPER_TYPE lerperType, bool autoStart = false)
        {
            this.time = time;
            this.lerperType = lerperType;
            ChangeLerperState(autoStart);
            Reset();
        }

        /// <summary>
        /// Call this to set lerper values, auto start if you want the timer to start or not yet
        /// </summary>
        public void SetValues(T start, T end, float time, LERPER_TYPE lerperType, bool autoStart = false)
        {
            this.start = start;
            this.end = end;
            SetValues(time, lerperType, autoStart);
        }

        /// <summary>
        /// Call this every time the timer is active
        /// </summary>
        public void UpdateLerper()
        {
            if (!active)
            {
                if (reached) reached = false;
                return;
            }

            currentTime += Time.deltaTime;
            if (currentTime > time) currentTime = time;

            percentage = currentTime / time;
            percentage = SmoothLerp(percentage, lerperType);

            UpdateCurrentPosition(percentage);
            if (CheckReached())
            {
                reached = true;
                ChangeLerperState(false);
            }
        }

        /// <summary>
        /// Call this to get the lerper percent between the actual time and the total time
        /// </summary>
        public float GetPercent()
        {
            return percentage;
        }

        /// <summary>
        /// Call this if you want to changed lerper values
        /// </summary>
        public void ResetValues(float time, LERPER_TYPE lerperType, bool autoStart = false)
        {
            SetValues(time, lerperType, autoStart);
        }
        
        /// <summary>
        /// Call this if you want to changed lerper values
        /// </summary>
        public void ResetValues(T start, T end, float time, LERPER_TYPE lerperType, bool autoStart = false)
        {
            SetValues(start, end, time, lerperType, autoStart);
        }

        /// <summary>
        /// Call this to active lerper
        /// </summary>
        public void ActiveLerper()
        {
            Reset();
            ChangeLerperState(true);
        }

        /// <summary>
        /// Call this to desactive lerper
        /// </summary>
        public void DesactiveLerper()
        {
            ChangeLerperState(false);
        }

        /// <summary>
        /// Change if the lerper is active or not
        /// </summary>
        private void ChangeLerperState(bool state)
        {
            active = state;
            reached = !state;
        }
        #endregion

        #region PROTECTED_METHODS
        /// <summary>
        /// Call this to set lerper values, auto start if you want the timer to start or not yet
        /// </summary>
        public abstract void SetLerperValues(T start, T end, float time, LERPER_TYPE lerperType, bool autoStart = false);

        /// <summary>
        /// Call this to get the lerper actual value
        /// </summary>
        public abstract T GetValue();

        /// <summary>
        /// Update lerper position
        /// </summary>
        protected abstract void UpdateCurrentPosition(float perc);

        /// <summary>
        /// Check if the lerper has reached or not
        /// </summary>
        protected abstract bool CheckReached();

        /// <summary>
        /// Reset the lerper
        /// </summary>
        protected void Reset()
        {
            currentTime = 0;
            reached = false;
            UpdateCurrentPosition(0);
        }

        /// <summary>
        /// Set the lerper smooth type
        /// </summary>
        protected float SmoothLerp(float value, LERPER_TYPE lerperType)
        {
            float smooth = value;

            switch (lerperType)
            {
                case LERPER_TYPE.NONE:
                    break;
                case LERPER_TYPE.EASE_IN:
                    smooth = 1f - Mathf.Cos(value * Mathf.PI * 0.5f);
                    break;
                case LERPER_TYPE.EASE_OUT:
                    smooth = Mathf.Sin(value * Mathf.PI * 0.5f);
                    break;
                case LERPER_TYPE.EXPONENTIAL:
                    smooth = value * value;
                    break;
                case LERPER_TYPE.STEP_SMOOTH:
                    smooth = value * value * (3f - 2f * value);
                    break;
                case LERPER_TYPE.STEP_SMOOTHER:
                    smooth = value * value * value * (value * (6f * value - 15f) + 10f);
                    break;
            }

            return smooth;
        }
        #endregion
    }
}