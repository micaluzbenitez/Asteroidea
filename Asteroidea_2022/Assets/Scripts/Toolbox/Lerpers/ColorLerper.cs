using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolbox.Lerpers
{
    public class ColorLerper : Lerper<Color>
    {
        /// <summary>
        /// Call this to set color lerper values, auto start if you want the timer to start or not yet
        /// </summary>
        public override void SetLerperValues(Color start, Color end, float time, LERPER_TYPE lerperType, bool autoStart = false)
        {
            SetValues(start, end, time, lerperType, autoStart);
        }

        /// <summary>
        /// Call this to get the color lerper actual value
        /// </summary>
        public override Color GetValue()
        {
            return currentValue;
        }

        /// <summary>
        /// Update color lerper position
        /// </summary>
        protected override void UpdateCurrentPosition(float percentage)
        {
            currentValue = Color.Lerp(start, end, percentage);
        }

        /// <summary>
        /// Check if the color lerper has reached or not
        /// </summary>
        protected override bool CheckReached()
        {
            if (currentValue == end) return true;
            else return false;
        }
    }
}