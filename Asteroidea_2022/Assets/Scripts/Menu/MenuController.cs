using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace Menu
{
    public class MenuController : MonoBehaviour
    {
        [Header("Menu panel")]
        [SerializeField] private CanvasGroup startingPanel = null;
        [SerializeField] private TMP_Text versionText = null;
        [SerializeField] private GameObject tutorialButton = null;
        [SerializeField] private GameObject playButton = null;
        [SerializeField] private GameObject playCenterButton = null;

        [Header("Game scene")]
        [SerializeField] private string gameSceneName = default;
        [SerializeField] private string tutorialSceneName = default;

        [Header("Skins")]
        [SerializeField] private Image player = null;
        [SerializeField] private Skin[] skins = null;

        [Header("Shop")]
        [SerializeField] private string shopPanelName = null;
        [SerializeField] private TMP_Text coins = null;

        [SerializeField] private Slider sliderMusic = null;
        [SerializeField] private Slider sliderSFX = null;

        private string musicKey = "music";
        private string sfxKey = "sfx";

        private SliderRTPC musicRTPC = null;
        private SliderRTPC sfxRTPC = null;

        private CanvasGroup actualPanel = null;

        public static string seenTutorialKey = "tutorial";
        public static int TRUE_VALUE = 1;
        public static int FALSE_VALUE = 0;
        private string coinsKey = "Coins";

        private static int actualShopCoins = 0;

        

        private void Awake()
        {
            WwiseInterface.StopGameplayMusic();

            SetProjectVersion();

            actualPanel = startingPanel;

            foreach (CanvasGroup canvasGroup in GetComponentsInChildren<CanvasGroup>())
            {
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }

            actualPanel.alpha = 1;
            actualPanel.blocksRaycasts = true;
            actualPanel.interactable = true;

            TryPlay();
            SetPlayerSkin();
            actualShopCoins = PlayerPrefs.GetInt("Coins");
        }

        private void Start()
        {
            StartAudio();
            Time.timeScale = 1;
        }

        private void StartAudio()
        {
            musicRTPC = sliderMusic.gameObject.GetComponent<SliderRTPC>();
            sfxRTPC = sliderSFX.gameObject.GetComponent<SliderRTPC>();
            LoadAudioPrefs();
            WwiseInterface.PlayMenuMusic();
        }

        private void LoadAudioPrefs()
        {
            if(PlayerPrefs.HasKey(musicKey))
            {
                sliderMusic.value = PlayerPrefs.GetInt(musicKey);
            }
            else
            {
                PlayerPrefs.SetInt(musicKey, 50);
            }

            if (PlayerPrefs.HasKey(sfxKey))
            {
                sliderSFX.value = PlayerPrefs.GetInt(sfxKey);
            }
            else
            {
                PlayerPrefs.SetInt(sfxKey, 50);
            }

            musicRTPC.UpdateRTPC();
            sfxRTPC.UpdateRTPC();
        }

        public void SaveAudioPrefs()
        {
            PlayerPrefs.SetInt(musicKey, (int)sliderMusic.value);
            PlayerPrefs.SetInt(sfxKey, (int)sliderSFX.value);
        }


        private void SetProjectVersion()
        {
            versionText.text = $"v{Application.version}";
        }

        public void SetPlayerSkin()
        {
            for (int i = 0; i < skins.Length; i++)
            {
                if (PlayerPrefs.GetInt("Skin") == skins[i].ID)
                {
                    player.sprite = skins[i].starUI;
                }
            }
        }
        public void UpdateCurrentCoins()
        {
            coins.text = PlayerPrefs.GetInt("Coins").ToString();
        }
        public void StartPanel(CanvasGroup newPanel)
        {
            if(newPanel.gameObject.name.Equals(shopPanelName))
            {
                UpdateCurrentCoins();
            }
            StartCoroutine(PanelChange(newPanel));
        }

        IEnumerator MakeItVisible(CanvasGroup panel)
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime;
                panel.alpha = t;
                yield return null;
            }
            panel.blocksRaycasts = true;
            panel.interactable = true;
        }
        IEnumerator MakeItInvisible(CanvasGroup panel)
        {
            panel.blocksRaycasts = false;
            panel.interactable = false;
            float t = 1;
            while (t > 0)
            {
                t -= Time.deltaTime;
                panel.alpha = t;
                yield return null;
            }
        }

        IEnumerator PanelChange(CanvasGroup panel)
        {
            yield return StartCoroutine(MakeItInvisible(actualPanel));
            StartCoroutine(MakeItVisible(panel));
            actualPanel = panel;
        }

        private void TryPlay()
        {
            if (!PlayerPrefs.HasKey(seenTutorialKey))
            {
                PlayerPrefs.SetInt(seenTutorialKey, FALSE_VALUE);
            }

            tutorialButton.gameObject.SetActive(PlayerPrefs.GetInt(seenTutorialKey) == TRUE_VALUE);
            playButton.gameObject.SetActive(PlayerPrefs.GetInt(seenTutorialKey) == TRUE_VALUE);
            playCenterButton.gameObject.SetActive(PlayerPrefs.GetInt(seenTutorialKey) == FALSE_VALUE);
        }


        public void CloseGame()
        {
            Application.Quit();
        }

        private void LoadGame()
        {
            SceneManager.LoadScene(gameSceneName);
        }
        public void LoadTutorial()
        {
            SceneManager.LoadScene(tutorialSceneName);
        }

        public void PlayButtonTap()
        {
            if (PlayerPrefs.GetInt(seenTutorialKey) == FALSE_VALUE)
            {
                LoadTutorial();
            }
            else
            {
                LoadGame();
            }
        }

        public void Exit()
        {
            Application.Quit();
        }

        public static void UpdateShopCoinsText(int newCoinsAmount) => actualShopCoins = newCoinsAmount;

        //Cheats
        public void CheatGetCoins(int pointsToGet)
        {
            PlayerPrefs.SetInt(coinsKey, PlayerPrefs.GetInt(coinsKey) + pointsToGet);
        }
        
        public void CheatResetCoins()
        {
            PlayerPrefs.SetInt(coinsKey, 0);
        }

    }
}