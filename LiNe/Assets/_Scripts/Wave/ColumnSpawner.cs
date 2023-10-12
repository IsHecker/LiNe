using System.Collections.Generic;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private Transform player;

    [SerializeField] private ColomnData[] columnData;

    [SerializeField] private float distanceToSpawn;

    private List<GameObject> _columns = new List<GameObject>();

    private PlayerBehaviour playerBehaviour;

    private GameObject spawnedColumn;

    public static bool pauseTask = true;

    private float spawnOffset = 0;

    private int startIndex = 0, lastIndex = 1;
    private int targetScoreToSpawn = 5;


    private void Start()
    {
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
        if (playerBehaviour.PlayerScore >= targetScoreToSpawn)
        {
            targetScoreToSpawn += lastIndex >= 3 ? 10 : 5; //changing required score to spawn a column to "10"
            
            if (lastIndex < columnData.Length - 1)
            {
                if (lastIndex >= 3) startIndex++;
                lastIndex++;
            }
        }


        int randomColumn = Random.Range(startIndex, lastIndex);

        Vector3 randomPosition = new Vector3(Random.Range(columnData[randomColumn].minRandomPosition, columnData[randomColumn].maxRandomPosition), spawnOffset, 0);

        spawnedColumn = Instantiate(columnData[randomColumn].Colomn, randomPosition, transform.rotation);

        spawnOffset += columnData[randomColumn].NextSpawnPoint;

        MoneySpawnManager.Instance.WaveMoneySpawn(spawnedColumn.transform.position +
            new Vector3(Random.Range(columnData[randomColumn].MinMoneyArea.x, columnData[randomColumn].MaxMoneyArea.x),
            Random.Range(columnData[randomColumn].MinMoneyArea.y, columnData[randomColumn].MaxMoneyArea.y)));
        
        _columns.Add(spawnedColumn);
        if (_columns.Count >= 10)
        {
            Destroy(_columns[0]);
            _columns.RemoveAt(0);
        }
    }
}
