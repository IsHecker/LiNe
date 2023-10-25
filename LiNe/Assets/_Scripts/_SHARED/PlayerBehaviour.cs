using System;
using UnityEngine;

public abstract class PlayerBehaviour : MonoBehaviour
{
    public float playerSpeed;
    public int PlayerScore => playerScore;

    [SerializeField] protected AudioClipsHolder AudioHolder;


    protected GameManager gameManager;

    protected Rigidbody2D RB;

    protected float currentSpeed;

    protected Vector3 cameraPosition;

    protected int playerScore = 0;


    private Camera _camera;

    private float width;
    private float height;

    private event Action OnPlayerDie = default;


    private void OnEnable() => GetComponent<TrailRenderer>().material.color = ScenesData.trailColor;

    private void Awake() 
    { 
        _camera = Camera.main;
        width = _camera.pixelWidth;
        height = _camera.pixelHeight;
        ScoreSystem.PlayerScore = 0;

        gameManager = FindObjectOfType<GameManager>();
        RB = GetComponent<Rigidbody2D>();
    }

    public void AddDeadEvent(Action action) => OnPlayerDie += action;

    protected void GravityDirection(float x, float y) => Physics2D.gravity = new Vector2(x, y);

    protected virtual void Die() 
    { 
        AudioManager.Instance?.PlaySound(AudioHolder, "Death");
        AudioManager.Instance?.GameOverEffect();
        OnPlayerDie?.Invoke(); 
    }

    protected abstract void CheckInput();

    protected abstract void HandleMovment();

    protected void CheckOutOfWidthBounds(Vector2 position)
    {
        if (gameManager.IsGameOver()) return;

        if (_camera.WorldToScreenPoint(position).x > width - 10 || _camera.WorldToScreenPoint(position).x <  10) Die();
    }

    protected void CheckOutOfHeightBounds(Vector2 position)
    {
        if (gameManager.IsGameOver()) return;
        if (_camera.WorldToScreenPoint(position).y > height - 10 || _camera.WorldToScreenPoint(position).y < 10) Die();
    }
}
