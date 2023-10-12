using UnityEngine;
using UnityEngine.SceneManagement;

public class BestScoreIndicator : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    private Vector3 indicatorPosition;

    private string xKey;
    private string yKey;

    private void Start()
    {
        xKey = SceneManager.GetActiveScene().name + " Indicator X";
        yKey = SceneManager.GetActiveScene().name + " Indicator Y";

        if (!(ScoreSystem.GetBestScore() > 0)) return;

        indicatorPosition.x = PlayerPrefs.GetFloat(xKey);
        indicatorPosition.y = PlayerPrefs.GetFloat(yKey);

        indicator.transform.position = indicatorPosition;
    }

    public void SetBestScorePosition(Vector3 position) => indicatorPosition = position;

    public void SavePosition()
    {
        if (!ScoreSystem.IsBestScore()) return;

        PlayerPrefs.SetFloat(xKey, indicatorPosition.x);
        PlayerPrefs.SetFloat(yKey, indicatorPosition.y);
    }
}
