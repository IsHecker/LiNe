using UnityEngine;
using EZCameraShake;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject Death;
	[SerializeField] private Transform bestLine; //best score line ----
	[SerializeField] private GameObject bestParticle;

    private static Camera _camera;
	private Rigidbody2D RB;
	private bool isGameover = false;
	private PlayerBehaviour player;
	private void Awake() 
	{
        player = FindAnyObjectByType<PlayerBehaviour>();
        RB = player.GetComponent<Rigidbody2D>();
		player.AddDeadEvent(GameOver);
		_camera = Camera.main;
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
	public void IsBestScore() 
	{
		if (ScoreSystem.IsBestScore() && ScoreSystem.GetBestScore() > 0)
			bestParticle.SetActive(true);
	}

    public static Vector3 GetScreenArea() => new(_camera.ScreenToWorldPoint(Vector3.zero).x, _camera.ScreenToWorldPoint(Vector3.zero).y);
    public static GameObject SpawnObjectAtArea(GameObject prefab, Vector3 area) => Instantiate(prefab, area, Quaternion.identity);
    public bool IsGameOver() => isGameover;
}
