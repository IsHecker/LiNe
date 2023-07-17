using System;
using UnityEngine;

public abstract class PlayerBehaviour : MonoBehaviour
{
    private void OnEnable() => GetComponent<TrailRenderer>().material.color = ScenesData.trailColor;
    private Camera _camera;
    private float width;
    private float height;
    private void Awake() 
    { 
        _camera = Camera.main;
        width = _camera.pixelWidth;
        height = _camera.pixelHeight;
    }

    public int PlayerScore => playerScore;
    public GameManager gameManager;
    public float playerSpeed;
    protected float currentSpeed;
    protected Vector3 cameraPosition;
    protected int playerScore = 0;
    private event Action isDeadEvent = () => { };
    public void AddDeadEvent(Action action) => isDeadEvent += action;
    protected void GravityDirection(float x, float y) => Physics2D.gravity = new Vector2(x, y);
    protected virtual void Die() => isDeadEvent?.Invoke();
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
