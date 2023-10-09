using UnityEngine;
using EZCameraShake;

public class SnakeHandler : PlayerBehaviour
{
    //5, 1.5, 0, 0.8
    [SerializeField] private GameObject DeathPrefab;
    [SerializeField] private float RotationSpeed = 100f, TailLength = 0.5f;

    private TrailRenderer trail;
    private CameraHandler cameraHandler;
    private SnakeSpawnManager spawnManager;
    private float Turn = 0f;
    private bool moveRight, moveLeft;
    private bool isGameStarted = false;

    private void Start()
    {
        trail = GetComponent<TrailRenderer>();
        spawnManager = FindObjectOfType<SnakeSpawnManager>();
        cameraHandler = Camera.main.GetComponent<CameraHandler>();
        currentSpeed = playerSpeed;
    }

    public void TurnRight()
    {
        //To Start Game only When Left Or Right is pressed
        isGameStarted = true;
        UIDisplay.Instance.CloseStartUI();

        StopTurnLeft();
        moveRight = true;
        AudioManager.Instance.PlaySound(AudioHolder, "Tap");
    }

    public void StopTurnRight() => moveRight = false;

    public void TurnLeft()
    {
        //To Start Game only When Left Or Right is pressed
        isGameStarted = true;
        UIDisplay.Instance.CloseStartUI();

        StopTurnRight();
        moveLeft = true;
        AudioManager.Instance.PlaySound(AudioHolder, "Tap");
    }

    public void StopTurnLeft() => moveLeft = false;

    protected override void CheckInput()
    {

        if (moveRight)
            Turn = 1;
        else if (moveLeft)
            Turn = -1;
        else
            Turn = 0;
    }

    protected override void HandleMovment()
    {
        transform.Translate(playerSpeed * Time.fixedDeltaTime * Vector2.up);
        transform.Rotate(RotationSpeed * Time.fixedDeltaTime * -Turn * Vector3.forward);
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

    private void IncreaseDifficulty()
    {
        if (targetPoints < 15) return;
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
            targetPoints++;
            IncreaseDifficulty();

            CameraShaker.Instance.ShakeOnce(4, 2f, 0, 1f);
            UIDisplay.Instance.UpdateScoreDisplay(++playerScore, true);
            EatFood(collision.gameObject);

            spawnManager.Spawn();
        }
    }

    private void EatFood(GameObject food)
    {
        food.GetComponent<Renderer>().enabled = false;
        food.transform.GetChild(0).gameObject.SetActive(true);
        AudioManager.Instance.PlaySound(AudioHolder, "Food");
        Destroy(food, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SnakeTail")) Die();
    }
}
