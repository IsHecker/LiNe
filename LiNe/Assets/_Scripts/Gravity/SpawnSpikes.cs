using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpikes : MonoBehaviour
{
    [SerializeField] private List<GameObject> spikes = new List<GameObject>();
    [SerializeField] private GameObject[] spikePrefabs;
    private Transform playerPosition;
    private float currentPosition = 5.5f;
    // Start is called before the first frame update
    private void Start()
    {
        playerPosition = GameObject.Find("Head_Player").transform;
        Spawn();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector3.Distance(playerPosition.position, spikes[spikes.Count - 1].transform.position) < 5)
            Spawn();

    }
    private void Spawn()
    {
        
        if (spikes.Count > 2)
		{
            Destroy(spikes[0]);
            spikes.RemoveAt(0);
		}
        GameObject go = Instantiate(spikePrefabs[Random.Range(0,spikePrefabs.Length)], new Vector3(currentPosition, -1.976524f), Quaternion.identity);
        currentPosition += 10;
        spikes.Add(go);
    }
}
