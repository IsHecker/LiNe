using System.Collections.Generic;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private Transform player;

    [SerializeField] private ColumnData[] obstacleData;

    [SerializeField] private float distanceToSpawn;

    private readonly List<GameObject> _columns = new List<GameObject>();

    private PlayerBehaviour playerBehaviour;

    private GameObject spawnedColumn;

    private ObjectPool objectPool;

    public static bool pauseTask = true;

    private float spawnOffset = 0;

    private int lastIndex = 3;
    private int startIndex = 0;
    private int targetScoreToSpawn = 5;


    private void Start()
    {
        objectPool = new ObjectPool(obstacleData);

        playerBehaviour = FindObjectOfType<PlayerBehaviour>();

        pauseTask = true;
        for (int i = 0; i < 5; i++) Spawn();
    }

    private void FixedUpdate()
    {
        if (pauseTask) return;

        if (Vector2.Distance(spawnedColumn.transform.position, player.localPosition) < distanceToSpawn)
            Spawn();
    }
    
    public void Spawn()
    {
        //Obtacle Proggresion
        if (playerBehaviour.PlayerScore >= targetScoreToSpawn)
        {
            targetScoreToSpawn += 5;

            if (lastIndex < obstacleData.Length - 1)
            {
                if (lastIndex > 5) startIndex++;
                lastIndex++;
            }
        }


        int randomColumn = Helpers.GenerateRandomNumber(startIndex, lastIndex);

        Vector3 randomPosition = new Vector3(Random.Range(obstacleData[randomColumn].minRandomPosition, obstacleData[randomColumn].maxRandomPosition), spawnOffset, 0);

        spawnedColumn = objectPool.GetFromPool(obstacleData[randomColumn].ObstacleName, randomPosition, transform.rotation);

        spawnOffset += obstacleData[randomColumn].NextSpawnPoint;

        MoneySpawnManager.Instance.WaveMoneySpawn(spawnedColumn.transform.position +
            new Vector3(Random.Range(obstacleData[randomColumn].MinMoneyArea.x, obstacleData[randomColumn].MaxMoneyArea.x),
            Random.Range(obstacleData[randomColumn].MinMoneyArea.y, obstacleData[randomColumn].MaxMoneyArea.y)));
        
        _columns.Add(spawnedColumn);
        if (_columns.Count >= 7)
        {
            _columns[0].SetActive(false);
            _columns.RemoveAt(0);
        }
    }
}
