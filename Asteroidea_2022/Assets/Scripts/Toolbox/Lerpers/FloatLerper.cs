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

/* Testo float lerper:
public class test : MonoBehaviour
{
    public float time = 0;
    private FloatLerper floatLerper = new FloatLerper();

    private void Awake()
    {
        floatLerper.SetLerperValues(5, 1, time, Lerper<float>.LERPER_TYPE.STEP_SMOOTH, true);
    }

    private void Update()
    {
        if (floatLerper.Active) floatLerper.UpdateLerper();

        if (Input.GetKeyDown(KeyCode.M)) floatLerper.ActiveLerper();
        if (Input.GetKeyDown(KeyCode.N)) floatLerper.DesactiveLerper();

        Debug.Log(floatLerper.GetValue());
    }
}
*/