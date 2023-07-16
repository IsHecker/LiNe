using UnityEngine;

public class FoodSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private GameObject foodPrefab;

    public void Spawn()
    {
        Vector3 screenArea = GameManager.GetScreenArea();
        float width = screenArea.x;
        float height = screenArea.y;
        GameManager.SpawnObjectAtArea(foodPrefab, new Vector3(
            Random.Range(width + 0.4f, -width - 0.4f),
            Random.Range(height + 0.67f, -height - 2.17f)));
    }
}
