using UnityEngine;

public class Food : MonoBehaviour
{
    private FoodSpawner foodSpawner;
    [SerializeField] private GameObject eatParticle;
    private void Start() => foodSpawner = FindAnyObjectByType<FoodSpawner>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(Instantiate(eatParticle, collision.transform.position, Quaternion.identity), 1.2f);
        foodSpawner.SpawnFood(new Vector2(Random.Range(-2.75f, 2.75f), Random.Range(-5.75f, 4.75f)));
        Destroy(gameObject);
    }
}
