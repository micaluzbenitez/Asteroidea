using System;
using UnityEngine;
using Toolbox;
using Toolbox.Pool;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Pickups spawn")]
        [SerializeField] protected ObjectPooler objectPooler = null;
        [SerializeField] protected Transform cameraPosition = null;
        [SerializeField] protected float cameraYOffset = 0;

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
            Vector3 position = new Vector3(0, cameraPosition.position.y - cameraYOffset, 0);
            objectPooler.SpawnFromPool(objectName, position, Quaternion.identity);
        }
    }
}