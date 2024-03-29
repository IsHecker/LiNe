using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreSystem
{
	private static int score = 0;
	public static int PlayerScore { get => score; set => score = value; }
	public static void SaveBestScore() 
	{
		if (IsBestScore())
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, score);
	}
	public static int GetBestScore() => PlayerPrefs.GetInt(SceneManager.GetActiveScene().name);
	public static bool IsBestScore() => PlayerScore > GetBestScore();
}
