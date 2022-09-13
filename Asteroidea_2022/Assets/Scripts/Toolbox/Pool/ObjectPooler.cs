using System;
using System.Collections.Generic;
using UnityEngine;

namespace Toolbox.Pool
{
    public class ObjectPooler : MonoBehaviour
    {
        /// Pool data
        [Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }

        #region Singleton
        public static ObjectPooler Instance;

        private void Awake()
        {
            Instance = this;
        }
        #endregion

        /// Pool list
		[Tooltip("Pool list")][NonReorderable]
        public List<Pool> pools;
        /// Pool distionary
		[Tooltip("Pool distionary")]
        public Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
        /// Pool game objects parent
		[Header("Parent"), Tooltip("Pool game objects parent")]
        public Transform parent = null;

        /// <summary>
        /// Initialize the pool lists and instantiate its objects
        /// </summary>
        private void Start()
        {
            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    if (parent) obj.transform.SetParent(parent);
                    obj.name = pool.tag;
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);
            }
        }

        /// <summary>
        /// Function by which the objects in the pool are called and controlled
        /// </summary>
        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

            if (pooledObj != null) pooledObj.OnObjectSpawn();

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
    }
}

/*
Script que lo llame (ejemplo):

ObjectPooler objectPooler

private void Start()
{
    objectPooler = ObjectPooler.Instance;
}

// Update is called once per frame
void FixedUpdate()
{
    objectPooler.SpawnFromPool("Cube", transform.position, Quaternion.identity);
}
 */