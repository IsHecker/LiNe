using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private GameObject spawnParticle;
    private void Start() => SpawnFood(new Vector2(Random.Range(-2.75f, 2.75f), Random.Range(-5.75f, 4.75f)));
    public void SpawnFood(Vector2 position)
    {
        GameObject startposparticle = Instantiate(spawnParticle, position, Quaternion.identity);
        Instantiate(foodPrefab, position, Quaternion.identity);
        Destroy(startposparticle, 0.6f);
    }
}
