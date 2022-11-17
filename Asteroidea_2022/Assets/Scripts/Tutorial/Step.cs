using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public void ShowStep()
    {
        Instantiate(prefab, GetComponentInParent<Transform>());
    }
    public void HideStep()
    {
        Destroy(this.gameObject);
    }

}
