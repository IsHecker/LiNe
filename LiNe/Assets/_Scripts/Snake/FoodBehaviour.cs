using UnityEngine;

public class FoodBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject eatParticle;
    private SnakeSpawnManager spawnManager;
    private void Start() => spawnManager = FindAnyObjectByType<SnakeSpawnManager>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(Instantiate(eatParticle, collision.transform.position, Quaternion.identity), 1.2f);
        spawnManager.Spawn();
        Destroy(gameObject);
    }
}
