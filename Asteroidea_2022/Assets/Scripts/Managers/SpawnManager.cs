using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;
using Toolbox.Pool;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Objects spawn data")]
        [SerializeField] protected ObjectPooler objectPooler = null;
        [SerializeField] protected Transform cameraPosition = null;
        [SerializeField] protected float yPosition = 0;
        [SerializeField] protected float xMinPosition = 0;
        [SerializeField] protected float xMaxPosition = 0;

        protected void CheckTimer(Timer timer, string objectName)
        {
            if (timer.Active) timer.UpdateTimer();

            if (timer.ReachedTimer())
            {
                SpawnObject(objectName);
                timer.ActiveTimer();
            }
        }

        protected void SpawnObject(string objectName)
        {
            float xPosition = Random.Range(xMinPosition, xMaxPosition);
            Vector3 position = new Vector3(xPosition, cameraPosition.position.y - yPosition, 0);

            objectPooler.SpawnFromPool(objectName, position, Quaternion.identity);
        }
    }
}