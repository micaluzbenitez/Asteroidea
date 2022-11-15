using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI
{
    public class UIShop : MonoBehaviour
    {
        [SerializeField] private TMP_Text coins = null;

        private void Start()
        {
            UpdateCoins();
        }

        public void UpdateCoins()
        {
            coins.text = PlayerPrefs.GetInt("Coins").ToString();
        }
    }
}