using UnityEngine;

public class FoodSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private Transform bestScoreIndicator;
    private Vector3 randomPosition;
    public void Spawn()
    {
        Vector3 screenArea = GameManager.GetScreenArea();
        float width = screenArea.x;
        float height = screenArea.y;
        randomPosition = new Vector3(
            Random.Range(width + 0.4f, -width - 0.4f),
            Random.Range(height + 0.67f, -height - 2.17f));
        GameObject spawnedFood = GameManager.SpawnObjectAtArea(foodPrefab, randomPosition);
        Debug.Log(ScoreSystem.PlayerScore);
        IndicateBestPoint(spawnedFood.transform.localPosition);
    }

    private void IndicateBestPoint(Vector3 position)
    {
        if (ScoreSystem.PlayerScore != ScoreSystem.GetBestScore() - 1 || ScoreSystem.GetBestScore() <= 0)
        {
            bestScoreIndicator.gameObject.SetActive(false);
            return;
        }

        bestScoreIndicator.gameObject.SetActive(true);
        bestScoreIndicator.transform.localPosition = position;

        if (position.x > 0)
        {
            bestScoreIndicator.GetChild(0).rotation = new Quaternion(0, 180, 0, 0);
            bestScoreIndicator.rotation = Quaternion.Euler(0, -180, 0);
        }


    }
}
