using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolbox.Lerpers
{
    public class FloatLerper : Lerper<float>
    {
        /// <summary>
        /// Call this to set float lerper values, auto start if you want the timer to start or not yet
        /// </summary>
        public override void SetLerperValues(float start, float end, float time, LERPER_TYPE lerperType, bool autoStart = false)
        {
            SetValues(start, end, time, lerperType, autoStart);
        }

        /// <summary>
        /// Call this to get the float lerper actual value
        /// </summary>
        public override float GetValue()
        {
            return currentValue;
        }

        /// <summary>
        /// Update float lerper position
        /// </summary>
        protected override void UpdateCurrentPosition(float percentage)
        {
            currentValue = Mathf.Lerp(start, end, percentage);
        }

        /// <summary>
        /// Check if the float lerper has reached or not
        /// </summary>
        protected override bool CheckReached()
        {
            if (currentValue == end) return true;
            else return false;
        }
    }
}