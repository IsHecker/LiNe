using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnColumn : MonoBehaviour
{
    public ColomnData[] colomn;
    public GameObject Money;

    public static float spawnTime = 2.3f;
    public List<GameObject> cols = new List<GameObject>();

    public static bool pauseTask = true;
    private float timeDown = 0;

    void Update()
    {
        if (pauseTask) return;

        timeDown += Time.deltaTime;
        if (timeDown >= spawnTime)
            SpawnNew();
    }
    void SpawnNew()
    {
        //reset timer
        timeDown = 0;

        GameObject _column;

        //spawn at random position
        int randomColumn = Random.Range(0, colomn.Length);
        Vector3 randomPosition = new Vector3(Random.Range(colomn[randomColumn].min, colomn[randomColumn].max), transform.position.y, 0);
        
        _column = Instantiate(colomn[randomColumn].Colomn, randomPosition, transform.rotation);

        if (Random.Range(0, 11) == 5)
            Instantiate(Money, new Vector3(Random.Range(-6, -10.5f), randomPosition.y + Random.Range(0.5f, 2.5f)), Quaternion.identity);
        
        cols.Add(_column);
        if (cols.Count >= 5)
        {
            Destroy(cols[0]);
            cols.RemoveAt(0);
        }
    }
}
