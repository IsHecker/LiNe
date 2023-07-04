using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class SnakeHandler : PlayerBehaviour
{
    //5, 1.5, 0, 0.8
    [SerializeField] private float RotationSpeed = 100f, TailLength = 0.5f;
    [SerializeField] private float Turn = 0f;
    [SerializeField] private bool tutorial, level;
    [SerializeField] private GameObject DeathPrefab;

    private AudioSource audiosfx;
    private TrailRenderer trail;
    private CameraHandler cameraHandler;
    private bool moveRight, moveLeft;
    private bool isGameStarted = false;

    private void Start()
    {
        trail = GetComponent<TrailRenderer>();
        audiosfx = GetComponent<AudioSource>();
        cameraHandler = Camera.main.GetComponent<CameraHandler>();
        currentSpeed = playerSpeed;
    }

    public void TurnRight()
    {
        StopTurnLeft();
        moveRight = true;
        audiosfx.Play();
    }

    public void StopTurnRight() => moveRight = false;

    public void TurnLeft()
    {
        StopTurnRight();
        moveLeft = true;
        audiosfx.Play();
    }

    public void StopTurnLeft() => moveLeft = false;

    protected override void CheckInput()
    {
        if (Input.GetMouseButtonDown(0)) { isGameStarted = true; UIDisplay.Instance.CloseStartUI(); }

        if (moveRight)
            Turn = 1;
        else if (moveLeft)
            Turn = -1;
        else
            Turn = 0;
    }

    protected override void HandleMovment()
    {
        transform.Translate(Vector2.up * playerSpeed * Time.fixedDeltaTime);
        transform.Rotate(Vector3.forward * -Turn * RotationSpeed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        CheckInput();
        CheckOutOfWidthBounds(transform.position);
        CheckOutOfHeightBounds(transform.position);
    }

    private void FixedUpdate()
    {
        if (gameManager.IsGameOver() || !isGameStarted) return;

        HandleMovment();
    }

    private int targetToResizeCamera = 0;
    private void ResizeCamera()
    {
        if (targetToResizeCamera < 10) return;

        targetToResizeCamera = 0;
        cameraHandler.IncreaseFOV();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        { 
            trail.time += TailLength;
            CameraShaker.Instance.ShakeOnce(5, 1.5f, 0, 0.8f);
            UIDisplay.Instance.UpdateScoreDisplay(++playerScore, true);
            targetToResizeCamera++;
            ResizeCamera();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SnakeTail") Die();
    }

    
}
