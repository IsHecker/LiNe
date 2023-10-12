using UnityEngine;
using EZCameraShake;
using System.Collections;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject DeathEffect;
	[SerializeField] private GameObject bestParticle;

    [SerializeField] private BestScoreIndicator bestScoreIndicator;


    private static Camera _camera;
	private Rigidbody2D RB;
	private bool isGameOver = false;
	private PlayerBehaviour player;


	private void Awake() 
	{
		_camera = Helpers.Camera;
		player = FindAnyObjectByType<PlayerBehaviour>();
		RB = player.GetComponent<Rigidbody2D>();
		player.AddDeadEvent(GameOver);
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
		if (ScoreSystem.IsBestScore() && ScoreSystem.GetBestScore() > 0 && bestParticle)
			bestParticle.SetActive(true);
	}

    public static Vector3 GetScreenArea() => new(_camera.ScreenToWorldPoint(Vector3.zero).x, _camera.ScreenToWorldPoint(Vector3.zero).y);

    public static GameObject SpawnObjectAtArea(GameObject prefab, Vector3 area) => Instantiate(prefab, area, Quaternion.identity);

    public bool IsGameOver() => isGameOver;


    public void IncreaseFOV()
    {
		const float Speed = 1f;
        float targetSize = _camera.orthographicSize;
        Time.timeScale = 0;
        StartCoroutine(Animation());

        IEnumerator Animation()
        {
            yield return new WaitForSecondsRealtime(0.3f);
            Size(++targetSize, Speed).setEaseInOutBack().setOnComplete(() => Time.timeScale = 1f);
        }
    }
    private LTDescr Size(float to, float time) => LeanTween.value(_camera.orthographicSize, to, time).setIgnoreTimeScale(true).setOnUpdate((value) => { _camera.orthographicSize = value; });
}
