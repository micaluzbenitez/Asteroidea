using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox.Pool;

public class Mouse : MonoBehaviour
{
    [SerializeField] private string bubblesName = null;
    [SerializeField] private GameObject bubblesParticleSystem = null;

    private ObjectPooler objectPooler = null;

    private void Awake()
    {
        objectPooler = GetComponent<ObjectPooler>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 bubblesPosition = new Vector3(worldPosition.x, worldPosition.y, 0);
            objectPooler.SpawnFromPool(bubblesName, bubblesPosition, bubblesParticleSystem.transform.rotation);
        }
    }
}
