using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class UIGame : MonoBehaviour
    {
        [Header("Life bar data")]
        public Image lifeBar;
        public bool changeGradient = true;
        public Gradient gradient = null;

        [Header("Game data")]
        public TMP_Text distanceText = null;
        public TMP_Text scoreText = null;

        public void UpdateLifeBar(float value, float minValue, float maxValue)
        {
            float actualValue = Mathf.Clamp(value, minValue, maxValue);
            float actualLife = actualValue / maxValue;

            lifeBar.fillAmount = actualLife;
            if (changeGradient) lifeBar.color = gradient.Evaluate(actualLife);
        }

        public void UpdateGameData(int distance, int score)
        {
            distanceText.text = distance.ToString() + " m";
            scoreText.text = score.ToString();
        }

        public void SetLifeBarValue(float value)
        {
            lifeBar.fillAmount = value;
            if (changeGradient) lifeBar.color = gradient.Evaluate(value);
        }
    }
}