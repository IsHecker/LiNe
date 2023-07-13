using UnityEngine;
using EZCameraShake;

public class GameManager : MonoBehaviour
{
	[SerializeField] private string SaveKey;
	[SerializeField] private GameObject Death;
	[SerializeField] private Rigidbody2D RB;
	[SerializeField] private Transform bestLine; //best score line ----
	private bool isGameover = false;
	private void Start() 
	{
		FindAnyObjectByType<PlayerBehaviour>().AddDeadEvent(GameOver);
		RB = FindAnyObjectByType<PlayerBehaviour>().GetComponent<Rigidbody2D>();

    }
	public void GameOver()
	{
		CameraShaker.Instance.ShakeOnce(7, 5, 0, 2);
		RB.bodyType = RigidbodyType2D.Static;
		Instantiate(Death, RB.transform.position, Quaternion.identity);
		isGameover = true;

		ScoreSystem.SaveBestScore();
		UIDisplay.Instance.GameOverDisplay();
		MoneySystem.SaveMoney();
    }
	public bool IsGameOver() => isGameover;
}
