using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    [System.Serializable]
    public struct Pool
    {
        public string poolName;
        public GameObject prefab;
        public int size;
    }


    private readonly Pool[] pools;

    private readonly Dictionary<string, Queue<GameObject>> poolDictionary;

    private readonly Transform objectPool;

    public ObjectPool(ObstacleData[] data)
    {
        pools = new Pool[data.Length];

        objectPool = GameObject.Find("Object Pool").transform;

        poolDictionary = new Dictionary<string, Queue<GameObject>>();


        GrowPool(data);


        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                var obj = Object.Instantiate(pool.prefab, this.objectPool);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.poolName, objectPool);
        }
    }

    public GameObject GetFromPool(string poolName, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(poolName)) return null;

        var objectToSpawn = poolDictionary[poolName].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.SetPositionAndRotation(position, rotation);

        poolDictionary[poolName].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    private void GrowPool(ObstacleData[] data)
    {

        for (int i = 0; i < data.Length; i++)
        {
            pools[i] = new Pool
            {
                poolName = data[i].ObstacleName,
                prefab = data[i].ObstaclePrefab,
                size = 6
            };
        }
    }

}
