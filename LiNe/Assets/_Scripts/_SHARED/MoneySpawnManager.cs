using UnityEngine;
using UnityEngine.UIElements;

public class MoneySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject moneyPrefab;

    public static MoneySpawnManager Instance;


    private void Awake() => Instance = this;

    public void WaveMoneySpawn(WaveObstacleData obstacleData, Transform parent)
    {
        if (Helpers.GenerateRandomNumber(1, 9) != 5) return;

        Vector3 position = new Vector3
            (Random.Range(obstacleData.MinMoneyArea.x, obstacleData.MaxMoneyArea.x),
            Random.Range(obstacleData.MinMoneyArea.y, obstacleData.MaxMoneyArea.y));

        Instantiate(moneyPrefab, Vector3.zero, Quaternion.identity, parent).transform.localPosition = position;
    }

    public void SnakeMoneySpawn()
    {
        Vector3 screenArea = GameManager.GetScreenArea();

        float width = screenArea.x;
        float height = screenArea.y;

        GameObject money = GameManager.SpawnObjectAtArea(moneyPrefab, new Vector3(
            Random.Range(width + 0.4f, -width - 0.4f),
            Random.Range(height + 0.67f, -height - 2.17f)));

        LeanTween.scale(money, new Vector3(0.9f, 0.9f, 1f), 0.8f).setEaseInOutSine().setLoopPingPong();
    }

    public void GravityMoneySpawn(GravityObstacleData obstacleData, Transform parent)
    {
        if (Helpers.GenerateRandomNumber(1, 10) != 5 || 
            obstacleData.minMoneyPosition.magnitude == 0 || 
            obstacleData.maxMoneyPosition.magnitude == 0)
            return;

        Vector3 position = new Vector3
            (Random.Range(obstacleData.minMoneyPosition.x, obstacleData.maxMoneyPosition.x),
            Random.Range(obstacleData.minMoneyPosition.y, obstacleData.maxMoneyPosition.y));

            Instantiate(moneyPrefab, Vector3.zero, Quaternion.identity, parent).transform.localPosition = position;
    }
}
