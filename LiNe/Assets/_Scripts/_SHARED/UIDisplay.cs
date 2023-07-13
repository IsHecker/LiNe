using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.UIElements;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private TMPro.TMP_Text scoreDisplay;
    [SerializeField] private AnimationCurve curve;

    private Animator animator;
    public static UIDisplay Instance { get; private set; }

    private void Awake() => Instance = Instance == null ? this : Instance;
    private void Start()
    {
        animator = GetComponent<Animator>();
        canvas = GetComponent<Canvas>();
        RestoreData();
    }
    public void CloseStartUI() => startUI.SetActive(false);

    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;
    public void GameOverDisplay()
    {
        scoreText.text = ScoreSystem.Score.ToString();
        bestScoreText.text = ScoreSystem.GetBestScore().ToString();
        pauseButton.SetActive(false);
        GameOverUI.SetActive(true);

        animator.Play("Game Over");
    }
    public void UpdateScoreDisplay(int score, bool applyEffect = false)
    {
        if (applyEffect) StartCoroutine(scoreanim());
        scoreDisplay.text = score.ToString();
        ScoreSystem.Score = score;
        IEnumerator scoreanim()
        {
            float t = 0;

            while (t < 1f)
            {
                t += Time.unscaledDeltaTime * 2;
                float a = curve.Evaluate(t);
                scoreDisplay.fontSize = a * 100;
                yield return 0;
            }
        }
    }

    [Header("Saved GameObjects")]
    [SerializeField] private ScenesData scenesData;
    [SerializeField] private GameObject fpsCounter;
    private Canvas canvas;

    private void RestoreData()
    {
        canvas.renderMode = scenesData.renderMode;
        fpsCounter.SetActive(scenesData.fpsActiveState);
    }
}
