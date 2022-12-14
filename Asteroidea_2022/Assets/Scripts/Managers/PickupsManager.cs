using System;
using UnityEngine;
using Toolbox;
using Random = UnityEngine.Random;

namespace Managers
{
    public class PickupsManager : SpawnManager
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

        private void Start()
        {
            for (int i = 0; i < pickup.Length; i++)
            {
                if (pickup[i].startPickup) SpawnObject(pickup[i].name);
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

        public void SpawnRandomPickup(Vector2 position)
        {
            int randomPickup = Random.Range(0, pickup.Length);
            objectPooler.SpawnFromPool(pickup[randomPickup].name, position, Quaternion.identity);
        }
    }
}