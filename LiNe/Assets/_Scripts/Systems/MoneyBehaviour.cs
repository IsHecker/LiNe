using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class MoneyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject canvasPrefab, particlePrefab;
    private int moneyValue;

    // Start is called before the first frame update
    private void Start() => moneyValue = RandMoney(1, 16);

    private void OnTriggerEnter2D(Collider2D collision) => Collect();
    private void Collect()
    {
        CameraShaker.Instance.ShakeOnce(3f, 1.5f, 0, 1);

        GameObject moneycanv = Instantiate(canvasPrefab, transform.position, Quaternion.identity);
        GameObject moneypartc = Instantiate(particlePrefab, transform.position, Quaternion.identity);

        moneycanv.transform.GetChild(0).gameObject.GetComponent<Text>().text = moneyValue + "$";
        MoneySystem.Money += moneyValue;

        //for destroying after Equiped
        gameObject.SetActive(false);
        Destroy(gameObject);
        Destroy(moneypartc, 1.6f);
        Destroy(moneycanv, 1.5f);
    }
    private int RandMoney(int min, int max) => Random.Range(min, max);
}
