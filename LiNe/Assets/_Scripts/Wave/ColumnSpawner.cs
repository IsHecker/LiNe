using System.Collections.Generic;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private ColomnData[] colomn;
    [SerializeField] private List<GameObject> _columns = new List<GameObject>();
    //public GameObject Money;


    public static float spawnTime = 2.3f;
    public static bool pauseTask = true;
    private float timer = 0;

    private void Update()
    {
        if (pauseTask) return;

        timer += Time.deltaTime;
        if (timer >= spawnTime)
            Spawn();
    }
    public void Spawn()
    {
        //reset timer
        timer = 0;
        GameObject spawnedColumn;
        //spawn at random position
        int randomColumn = Random.Range(0, colomn.Length);
        Vector3 randomPosition = new Vector3(Random.Range(colomn[randomColumn].min, colomn[randomColumn].max), transform.position.y, 0);
        spawnedColumn = Instantiate(colomn[randomColumn].Colomn, randomPosition, transform.rotation);

        if (Random.Range(0, 11) == 5)
            MoneySpawnManager.Instance.WaveMoneySpawn(new Vector3(Random.Range(-6, -10.5f), randomPosition.y + Random.Range(0.5f, 2.5f)));
        
        _columns.Add(spawnedColumn);
        if (_columns.Count >= 5)
        {
            Destroy(_columns[0]);
            _columns.RemoveAt(0);
        }
    }
}
