using System.Collections.Generic;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private Transform player;
    [SerializeField] private ColomnData[] column;
    [SerializeField] private float distance;
    private List<GameObject> _columns = new List<GameObject>();
    private GameObject spawnedColumn;

    public static bool pauseTask = true;

    private void Start()
    {
        pauseTask = true;
        for (int i = 0; i < 5; i++) Spawn();
    }
    private void FixedUpdate()
    {
        if (pauseTask) return;
        if (Vector2.Distance(spawnedColumn.transform.position, player.localPosition) < distance)
            Spawn();
    }
    private float spawnOffset = 0;
    public void StartOffset(float offset) => spawnOffset = offset;
    public void Spawn()
    {
        int randomColumn = Random.Range(0, column.Length);
        Vector3 randomPosition = new Vector3(Random.Range(column[randomColumn].MinWidth, column[randomColumn].MaxWidth), spawnOffset, 0);
        spawnedColumn = Instantiate(column[randomColumn].Colomn, randomPosition, transform.rotation);
        spawnOffset += column[randomColumn].SpawnPoint;

        if (Random.Range(0, 9) == 5)
            MoneySpawnManager.Instance.WaveMoneySpawn(spawnedColumn.transform.position +
                new Vector3(Random.Range(column[randomColumn].MinMoneyArea.x, column[randomColumn].MaxMoneyArea.x),
                Random.Range(column[randomColumn].MinMoneyArea.y, column[randomColumn].MaxMoneyArea.y)));
        
        _columns.Add(spawnedColumn);
        if (_columns.Count >= 10)
        {
            Destroy(_columns[0]);
            _columns.RemoveAt(0);
        }
    }
}
