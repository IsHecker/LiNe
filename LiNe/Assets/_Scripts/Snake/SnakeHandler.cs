using UnityEngine;
using EZCameraShake;

public class SnakeHandler : PlayerBehaviour
{
    //5, 1.5, 0, 0.8
    [SerializeField] private float RotationSpeed = 100f, TailLength = 0.5f;
    [SerializeField] private GameObject DeathPrefab;

    private float Turn = 0f;
    private bool moveRight, moveLeft;
    private bool isGameStarted = false;
    private AudioSource audiosfx;
    private TrailRenderer trail;
    private CameraHandler cameraHandler;

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
        if (Input.GetMouseButtonDown(0) ) { isGameStarted = true; UIDisplay.Instance.CloseStartUI(); }

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

    private int targetPoints = 0;

    void IncreaseDifficulty()
    {
        if (targetPoints < 2) return;
        targetPoints = 0;
        playerSpeed += 0.15f;
        RotationSpeed += 5;
        ResizeCamera();
    }
    private void ResizeCamera() => cameraHandler.IncreaseFOV();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        { 
            trail.time += TailLength;
            CameraShaker.Instance.ShakeOnce(5, 1.5f, 0, 0.8f);
            UIDisplay.Instance.UpdateScoreDisplay(++playerScore, true);
            targetPoints++;
            IncreaseDifficulty();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SnakeTail") Die();
    }

    
}
