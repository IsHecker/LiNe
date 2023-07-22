using UnityEngine;

public class MoneySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject moneyPrefab;
    public static MoneySpawnManager Instance;
    private void Awake() => Instance = this;
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

    public void WaveMoneySpawn(Vector3 position) => Instantiate(moneyPrefab, position, Quaternion.identity);

    public void GravityMoneySpawn() { }
}
