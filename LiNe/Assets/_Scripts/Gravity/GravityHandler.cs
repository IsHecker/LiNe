using UnityEngine;

public class GravityHandler : PlayerBehaviour
{
	[SerializeField] private float gravity;
	[SerializeField] private GamePlayCamera gamePlayCamera;
    [SerializeField] private BestScoreIndicator bestScoreIndicator;

    [HideInInspector] public Rigidbody2D RB;
	[HideInInspector] public AudioSource soundFX;

	private Transform mytransform;

	private bool isOnGround = true;
	private bool isGameStarted = false;
	private bool checkBounds;

    private void Start()
    {
		mytransform = GetComponent<Transform>();
		RB = GetComponent<Rigidbody2D>();
        cameraPosition = new Vector3(0, 0.5f, -10);
        GravityDirection(0, -9.8f);
    }

	private void Update()
	{
		if (gameManager.IsGameOver() || Helpers.IsOverUI()) return;
		CheckInput();
		if (!checkBounds) return;
		CheckOutOfWidthBounds(mytransform.position);
		CheckOutOfHeightBounds(mytransform.position);
	}
	private void FixedUpdate()
	{
		if (!isGameStarted || gameManager.IsGameOver()) return;

		HandleMovment();
		CamerFollow();
    }

	protected override void CheckInput()
	{
		if (!Input.GetMouseButtonDown(0) || !isOnGround) return;
		isGameStarted = true;
		isOnGround = false;
		currentSpeed = playerSpeed;
		RB.gravityScale = gravity *= -1f;
		cameraPosition.y *= -1f;
        AudioManager.Instance.PlaySound(AudioHolder, "Tap");

        UIDisplay.Instance.CloseStartUI();
	}

	private Vector3 playerVelocity;
	protected override void HandleMovment() 
	{
		playerVelocity.Set(currentSpeed * Time.fixedDeltaTime, 0, 0);
        mytransform.Translate(playerVelocity, Space.World);
		playerScore = (int)mytransform.position.x;
		if (playerScore < 0) return;
		UIDisplay.Instance.UpdateScoreDisplay(playerScore);
		bestScoreIndicator.SetBestScorePosition(Vector3.right * mytransform.position.x);
    }

    private void CamerFollow()
	{
		cameraPosition.x = mytransform.position.x;

        if (mytransform.position.x < gamePlayCamera.GetPosition().x) return;
		checkBounds = true;
        gamePlayCamera.SmoothFollow(cameraPosition);
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		isOnGround = true; //cuz it hits the walls 
		if (collision.collider.CompareTag("spike")) Die(); 
	}
}
