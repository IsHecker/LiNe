using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEvents : MonoBehaviour
{
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject GameOverUI;
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PauseGame() 
    {
        Time.timeScale = 0;
        PauseUI.SetActive(true);
        animator.Play("Pause");
    }

    public void ResumeGame() 
    {
        Time.timeScale = 1; 
        animator.Play("Pause Reversed");
        Invoke(nameof(ClosePauseUI), 0.50f);
    }

    private void ClosePauseUI() => PauseUI.SetActive(false);

    public void RestartGame() { AudioManager.Instance?.RestartEffect(); FindAnyObjectByType<Fading>().FadeTo(SceneManager.GetActiveScene().name); }

    public void ExitGame() { AudioManager.Instance?.RestartEffect(); FindAnyObjectByType<Fading>().FadeTo("Main Menu"); }
}
