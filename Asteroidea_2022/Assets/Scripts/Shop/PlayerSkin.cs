using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace Shop
{
    public class PlayerSkin : MonoBehaviour
    {
        [SerializeField] private GameObject frame = null;
        [SerializeField] private Image icon = null;
        [SerializeField] private TMP_Text cost = null;
        [SerializeField] private int ID = 0;
        [SerializeField] private Skin[] skins = null;

        [Header("Events")]
        [SerializeField] private UnityEvent OnUnlockSkin = null;

        private void Awake()
        {
            PlayerPrefs.SetInt("Skin0", 1);

            /* TEST
            PlayerPrefs.SetInt("Skin1", 0);
            PlayerPrefs.SetInt("Skin2", 0);
            PlayerPrefs.SetInt("Skin3", 0);
            PlayerPrefs.SetInt("Skin4", 0);
            PlayerPrefs.SetInt("Skin5", 0);
            PlayerPrefs.SetInt("Coins", 10000);
            */
        }

        private void Start()
        {
            if (PlayerPrefs.GetInt($"Skin{ID}") == 0)
            {
                for (int i = 0; i < skins.Length; i++)
                {
                    if (ID == skins[i].ID) cost.text = "$" + skins[i].cost;
                }
                icon.color = Color.black;
            }
            else
            {
                cost.enabled = false;
                icon.color = Color.white;
            }            
        }

        public void UnlockSkin(int ID)
        {
            if (PlayerPrefs.GetInt($"Skin{ID}") == 0)
            {
                if (PlayerPrefs.GetInt("Coins") > skins[ID].cost)
                {
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - skins[ID].cost);
                    PlayerPrefs.SetInt($"Skin{ID}", 1);
                    PlayerPrefs.SetInt("Skin", ID);
                    cost.enabled = false;
                    icon.color = Color.white;
                    frame.SetActive(true);
                    OnUnlockSkin?.Invoke();
                }
            }
            else
            {
                PlayerPrefs.SetInt("Skin", ID);
                cost.enabled = false;
                icon.color = Color.white;
                frame.SetActive(true);
                OnUnlockSkin?.Invoke();
            }
        }
    }
}