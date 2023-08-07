using UnityEngine;
using EZCameraShake;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject DeathEffect;
	[SerializeField] private GameObject bestParticle;

    [SerializeField] private BestScoreIndicator bestScoreIndicator;


    private static Camera _camera;
	private CameraControl _cameraControl;
	private Rigidbody2D RB;
	private bool isGameOver = false;
	private PlayerBehaviour player;
	private void Awake() 
	{
		_camera = Helpers.Camera;
		_cameraControl = Helpers.Camera.GetComponent<CameraControl>();
    }
	public void GameOver()
	{
		CameraShaker.Instance.ShakeOnce(7, 5, 0, 2);
		RB.bodyType = RigidbodyType2D.Static;
		Instantiate(DeathEffect, RB.transform.position, Quaternion.identity);
		isGameOver = true;

		bestScoreIndicator.SavePosition();
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
    public bool IsGameOver() => isGameOver;
	

  //  class GamePreparation
  //  {
		//public void PrepareGame()
  //      {
  //          player = FindAnyObjectByType<PlayerBehaviour>();
  //          RB = player.GetComponent<Rigidbody2D>();
  //          player.AddDeadEvent(GameOver);
  //      }
		//public static void TargetCamera(Vector2 position, float time)
  //      {
  //          LeanTween.value(lineTitle.alpha, 1, 0.5f).setEaseOutQuart().setOnUpdate(value => lineTitle.alpha = value);
  //      }
  //      public static void TargetPlayerPosition(Vector2 position, float time)
  //      {
  //          LeanTween.value(lineTitle.alpha, 1, 0.5f).setEaseOutQuart().setOnUpdate(value => lineTitle.alpha = value);
  //      }
  //  }
}
