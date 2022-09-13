using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIGame : MonoBehaviour
    {
        [Header("Life bar data")]
        public Image lifeBar;
        public bool changeGradient = true;
        public Gradient gradient = null;

        public void UpdateLifeBar(float value, float minValue, float maxValue)
        {
            float actualValue = Mathf.Clamp(value, minValue, maxValue);
            float actualLife = actualValue / maxValue;

            lifeBar.fillAmount = actualLife;
            if (changeGradient) lifeBar.color = gradient.Evaluate(actualLife);
        }
    }
}