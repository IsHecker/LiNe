using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIEvents : MonoBehaviour
{
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject GameOverUI;
    private Animator animator;

    public static UIEvents Instance { get; private set; }
    private void Start()
    {
        Instance ??= this;
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
        animator.Play("Pause Inversed");
        Invoke("ClosePauseUI", 0.16f);
    }
    private void ClosePauseUI() => PauseUI.SetActive(false);

    public void RestartGame() => FindAnyObjectByType<Fading>().FadeTo(SceneManager.GetActiveScene().name);

    public void ExitGame() => FindAnyObjectByType<Fading>().FadeTo("Main Menu");
}
