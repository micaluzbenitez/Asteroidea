using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTPCWise : MonoBehaviour
{
    // Start is called before the first frame update

    public int volumensfxRTPC = 0;

    public AK.Wwise.RTPC volumensfx;

    void Start()
    {
        volumensfx.SetValue(gameObject, volumensfxRTPC);
    }

    // Update is called once per frame
    void Update()
    {
        volumensfx.SetValue(gameObject, volumensfxRTPC);
    }
}
