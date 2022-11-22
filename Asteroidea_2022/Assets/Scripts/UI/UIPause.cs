using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIPause : MonoBehaviour
    {
        [Header("Game data")]
        public TMP_Text distanceText = null;
        public TMP_Text scoreText = null;
        public TMP_Text coinsText = null;

        public Action<TMP_Text, TMP_Text, TMP_Text> OnShowGameData;

        public void ShowGameData()
        {
            OnShowGameData?.Invoke(distanceText, scoreText, coinsText);
        }
    }
}