using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ButtonTouch : MonoBehaviour
{
	//private Button thisButton;


	//void Start()
	//{
	//	thisButton = GetComponent<Button>();
	//}

	public void PlaySound()
    {
        WwiseInterface.ExecuteWwiseEvent(WwiseInterface.WwiseEvents.Button_Touch);
    }
}
