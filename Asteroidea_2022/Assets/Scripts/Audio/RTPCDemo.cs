using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTPCDemo : MonoBehaviour
{

    public int distanciaRTPC = 0;

    public AK.Wwise.RTPC distancia;

    // Start is called before the first frame update
    void Start()
    {
        distancia.SetValue(gameObject, distanciaRTPC);
    }

    // Update is called once per frame
    void Update()
    {
        distancia.SetValue(gameObject, distanciaRTPC);   
    }
}
