using System;
using UnityEngine;
using Toolbox;
using Toolbox.Pool;

namespace Managers
{
    public class PickupsManager : MonoBehaviour
    {
        [Serializable]
        public class Pickup
        {
            public string name = "";
            public bool startPickup = false;
            public float timePerPickup = 0;
            [NonSerialized] public Timer timer = new Timer();
        }

        [Header("Pickups")]
        [SerializeField] private Pickup[] pickup = null;

        [Header("Pickups spawn")]
        [SerializeField] private ObjectPooler objectPooler = null;
        [SerializeField] private Transform cameraPosition = null;
        [SerializeField] private float cameraYOffset = 0;

        private void Start()
        {
            for (int i = 0; i < pickup.Length; i++)
            {
                if (pickup[i].startPickup) SpawnEnemy(pickup[i].name);
                pickup[i].timer.SetTimer(pickup[i].timePerPickup, Timer.TIMER_MODE.DECREASE, true);
            }
        }

        private void Update()
        {
            for (int i = 0; i < pickup.Length; i++)
            {
                CheckTimer(pickup[i].timer, pickup[i].name);
            }
        }

        private void CheckTimer(Timer timer, string pickupName)
        {
            if (timer.Active) timer.UpdateTimer();

            if (timer.ReachedTimer())
            {
                SpawnEnemy(pickupName);
                timer.ActiveTimer();
            }
        }

        private void SpawnEnemy(string pickupName)
        {
            Vector3 position = new Vector3(0, cameraPosition.position.y - cameraYOffset, 0);
            objectPooler.SpawnFromPool(pickupName, position, Quaternion.identity);
        }
    }
}