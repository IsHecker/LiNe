using UnityEngine;

public class GravityHandler : PlayerBehaviour
{
	[SerializeField] private float gravity;
	[SerializeField] private CameraController gamePlayCamera;
    [SerializeField] private BestScoreIndicator bestScoreIndicator;

	[HideInInspector] public AudioSource soundFX;

	private Transform mytransform;

	[SerializeField] private float inputHoldTime;
	private float touchInputStartTime;
	public float distance;
	public LayerMask whatisground;
	private bool jumpInput;
    private bool isGameStarted = false;

    private Vector3 playerVelocity;


    private void Start()
    {
		mytransform = GetComponent<Transform>();
        cameraPosition = new Vector3(0, 0.5f, -10);
        GravityDirection(0, -9.8f);
    }

	private void Update()
	{
		if (gameManager.IsGameOver() || Helpers.IsOverUI())
			return;

		CheckInputHoldTime();
		CheckInput();
		CheckOutOfHeightBounds(mytransform.position);
	}

	private void FixedUpdate()
	{
		if (!isGameStarted || gameManager.IsGameOver()) 
			return;

		HandleMovment();
		CamerFollow();
    }

	protected override void CheckInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			jumpInput = true;

            touchInputStartTime = Time.time;
            isGameStarted = true;
        }

		if (!jumpInput || !IsGround((int)gravity))
			return;

		UseJumpInput();

        currentSpeed = playerSpeed;
		RB.gravityScale = gravity *= -1f;
		cameraPosition.y *= -1f;
        AudioManager.Instance.PlaySound(AudioHolder, "Tap");

        UIDisplay.Instance.CloseStartUI();
	}

	protected override void HandleMovment() 
	{
		playerVelocity.Set(currentSpeed * Time.fixedDeltaTime, 0, 0);
        mytransform.Translate(playerVelocity, Space.World);
		playerScore = (int)mytransform.position.x;

		if (playerScore < 0)
			return;

		UIDisplay.Instance.UpdateScoreDisplay(playerScore);
		bestScoreIndicator.SetBestScorePosition(Vector3.right * mytransform.position.x);
    }

    private void CamerFollow()
	{
		cameraPosition.x = mytransform.position.x;

        if (mytransform.position.x < gamePlayCamera.GetPosition().x) return;
        gamePlayCamera.SmoothFollow(cameraPosition);
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("spike")) Die(); 
	}

	private void CheckInputHoldTime()
	{
		if (Time.time > touchInputStartTime + inputHoldTime)
			jumpInput = false;
	}

	private void UseJumpInput()=> jumpInput = false;

    private void OnDrawGizmos()
    {
		Gizmos.color = Color.red;
        Vector2 dir = gravity > 0 ? Vector2.down : Vector2.up;
		Gizmos.DrawLine(transform.position, transform.position + (Vector3)(dir * distance));
    }

    private bool IsGround(int gravityDirection)
	{
		Vector2 dir = gravityDirection > 0 ? Vector2.down : Vector2.up;
		return Physics2D.Raycast(mytransform.position, dir, distance, whatisground);
    }
}
