using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreSystem
{
	static int score = 0;
	public static int Score { get => score; set => score = value; }
	public static void SaveBestScore() 
	{
		if (Score > GetBestScore())
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, score);
	}
	public static int GetBestScore() => PlayerPrefs.GetInt(SceneManager.GetActiveScene().name);
}
