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
        IndicateBestPoint(spawnedFood.transform.localPosition);
    }
    private GameObject arrow;
    private void IndicateBestPoint(Vector3 position)
    {
        if (ScoreSystem.PlayerScore != ScoreSystem.GetBestScore() - 1 || ScoreSystem.GetBestScore() <= 0)
        {
            LeanTween.alphaCanvas(bestScoreIndicator.GetComponent<CanvasGroup>(), 0, 0.5f).setOnComplete(() => bestScoreIndicator.gameObject.SetActive(false));
            return;
        }
        bestScoreIndicator.gameObject.SetActive(true);
        bestScoreIndicator.localPosition = position;
        LeanTween.alphaCanvas(bestScoreIndicator.GetComponent<CanvasGroup>(), 1, 0.5f);
        if (position.x > 0)
        {
            bestScoreIndicator.GetChild(0).transform.rotation = Quaternion.Euler(0, -180, 0);
            bestScoreIndicator.rotation = Quaternion.Euler(0, -180, 0);
        }
        arrow = bestScoreIndicator.GetChild(1).gameObject;
        LeanTween.scale(arrow, new Vector3(0.04f, 0.04f), 1f).setEaseInOutSine().setLoopPingPong();

    }
}
