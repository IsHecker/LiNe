using UnityEngine;

public class SnakeSpawnManager : MonoBehaviour, ISpawner
{
    [SerializeField] private FoodSpawner foodSpawner;

    private void Start() => Spawn();

    public void Spawn()
    {
        foodSpawner.Spawn();

        if (Random.Range(0, 16) != 10) return;
        MoneySpawnManager.Instance.SnakeMoneySpawn();
    }
}
