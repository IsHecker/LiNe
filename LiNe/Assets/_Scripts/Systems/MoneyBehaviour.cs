using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class MoneyBehaviour : MonoBehaviour
{
    private int moneyValue;
    private void Start() => moneyValue = RandMoney(1, 16);

    private void OnTriggerEnter2D(Collider2D collision) => Collect();
    private void Collect()
    {
        CameraShaker.Instance.ShakeOnce(4f, 3.1f, 0f, 1f);

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = moneyValue + "$";
        MoneySystem.Money += moneyValue;

        Destroy(gameObject,1.5f);
    }
    private int RandMoney(int min, int max) => Random.Range(min, max);
}
