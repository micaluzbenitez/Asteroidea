using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseInterface
{
    public enum WwiseEvents
    {
        Player_Touch_Platform,
        Player_Movement,
        Player_Damage,
        Player_Death,
        Platform_Break,
        //Skin_Purchase,
        //Skin_Select,
        Button_Touch,
        Bomb_Explode,
        Hit_Pick_Up,
        Hit_Jelly,
        Hit_Crab,
        Hit_Coin,
        Hit_Fish
    }

    private static bool playingMenuMusic = false;

    private static bool playingGameplayMusic = false;
    private static int gameplayMusicIndex = 0;

    public static void PauseAllSounds()
    {
        AkSoundEngine.PostEvent("Stop_SFX_Muteables", AkInitializer.GetInstance().gameObject);
    }
    public static void PlayMenuMusic()
    {
        if (!playingMenuMusic)
        {
            Debug.Log("Intento correr la musica del menu");
            AkSoundEngine.PostEvent("Play_Menu_Music", AkInitializer.GetInstance().gameObject);
            playingMenuMusic = true;
        }
    }
    public static void StopMenuMusic()
    {
        if (playingMenuMusic)
        {
            Debug.Log("Intento parar la musica del menu");
            AkSoundEngine.PostEvent("Stop_Menu_Music", AkInitializer.GetInstance().gameObject);
            playingMenuMusic = false;
        }
    }
    public static void PlayGameplayMusic()
    {
        if (!playingGameplayMusic)
        {
            Debug.Log("Intento correr la musica del gameplay");
            
            gameplayMusicIndex = Random.Range(1, 4);

            string eventName = "Play_Gameplay" + gameplayMusicIndex.ToString();

            AkSoundEngine.PostEvent(eventName, AkInitializer.GetInstance().gameObject);
            playingGameplayMusic = true;
        }
    }
    public static void StopGameplayMusic()
    {
        if (playingGameplayMusic)
        {
            Debug.Log("Intento parar la musica del gameplay");

            string eventName = "Stop_Gameplay" + gameplayMusicIndex.ToString();

            AkSoundEngine.PostEvent(eventName, AkInitializer.GetInstance().gameObject);
            playingGameplayMusic = false;
        }
    }



    public static void ExecuteWwiseEvent(WwiseEvents wwiseEvent)
    {
        string eventText = "";
        switch (wwiseEvent)
        {
            case WwiseEvents.Hit_Jelly:
                eventText = "Jellyfish_Movement";
                break;
            case WwiseEvents.Button_Touch:
                eventText = "Play_bubles";
                break;
            case WwiseEvents.Hit_Crab:
                eventText = "Play_cascarudo";
                break;
            case WwiseEvents.Hit_Coin:
                eventText = "Play_Coins";
                break;
            case WwiseEvents.Player_Damage:
                eventText = "Play_Damage";
                break;
            case WwiseEvents.Hit_Fish:
                eventText = "Play_fish_bite";
                break;
            case WwiseEvents.Platform_Break:
                eventText = "Play_broke_plataform";
                break;
            case WwiseEvents.Player_Death:
                eventText = "Play_death";
                break;
            case WwiseEvents.Player_Touch_Platform:
                eventText = "Play_impact_plataform";
                break;
            case WwiseEvents.Hit_Pick_Up:
                eventText = "Play_pickup";
                break;
            case WwiseEvents.Player_Movement:
                eventText = "Starfish_Movements";
                break;
            case WwiseEvents.Bomb_Explode:
                eventText = "Play_Bombas";
                break;
            default:
                eventText = "Incorrect Index";
                break;
        }

        //If (No se esta reproduciendo "eventText")
        AkSoundEngine.PostEvent(eventText, AkInitializer.GetInstance().gameObject);

    }

}