using UnityEngine;
using UnityEngine.EventSystems;
using EZCameraShake;

public class GravityHandler : PlayerBehaviour
{
	private bool onGround = true;
	private bool isGameStarted = false;
	private bool checkBounds;

	[SerializeField] private float gravity;
    [SerializeField] private GameObject death;
    [SerializeField] private GameObject bestScoreEFX;
    [SerializeField] private AudioClip deafaultsfx;

    [HideInInspector] public Rigidbody2D RB;
	[HideInInspector] public AudioSource soundFX;

    private void Start()
    {
		playerCamera = Camera.main;
		RB = GetComponent<Rigidbody2D>();
        cameraPosition = new Vector3(0, 0.5f, -10);

        GravityDirection(0, -9.8f);
    }

	private void Update()
	{
		if (gameManager.IsGameOver() || EventSystem.current.currentSelectedGameObject) return;
		CheckInput();
		if (!checkBounds) return;
		CheckOutOfWidthBounds(transform.position);
		CheckOutOfHeightBounds(transform.position);
	}
	private void FixedUpdate()
	{
		if (!isGameStarted || gameManager.IsGameOver()) return;

		HandleMovment();
		CamerFollow();
	}

	protected override void CheckInput()
	{
		if (!Input.GetMouseButtonDown(0) || !onGround) return;
		isGameStarted = true;
		onGround = false;
		currentSpeed = playerSpeed;
		RB.gravityScale = gravity *= -1f;
		cameraPosition.y *= -1f;
		UIDisplay.Instance.CloseStartUI();
	}
	protected override void HandleMovment() 
	{
        transform.Translate(new Vector3(currentSpeed * Time.fixedDeltaTime, 0, 0), Space.World);
		playerScore = (int)transform.position.x;
		if (playerScore > 0)
			UIDisplay.Instance.UpdateScoreDisplay(playerScore);
    }

    private const float Smooth_Camera = 4;
    private void CamerFollow()
	{
		Vector3 offSet = Vector3.LerpUnclamped(playerCamera.transform.position, cameraPosition, Smooth_Camera * Time.fixedDeltaTime);
		if (transform.position.x >= playerCamera.transform.position.x)
		{
			checkBounds = true;
            cameraPosition.x = transform.position.x;
			CameraShaker.Instance.RestPositionOffset = offSet;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		onGround = true; //cuz it hits the walls 
		if (collision.collider.CompareTag("spike")) Die(); 
	}
}
