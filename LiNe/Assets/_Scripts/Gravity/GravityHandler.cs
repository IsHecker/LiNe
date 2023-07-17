using UnityEngine;
using UnityEngine.EventSystems;

public class GravityHandler : PlayerBehaviour
{
	[SerializeField] private float gravity;
    [SerializeField] private AudioClip deafaultsfx;
	[SerializeField] private GamePlayCamera gamePlayCamera;
    [SerializeField] private BestScoreIndicator bestScoreIndicator;

    [HideInInspector] public Rigidbody2D RB;
	[HideInInspector] public AudioSource soundFX;

	private bool onGround = true;
	private bool isGameStarted = false;
	private bool checkBounds;
	private Transform mytransform;

    private void Start()
    {
		mytransform = GetComponent<Transform>();
		RB = GetComponent<Rigidbody2D>();
        cameraPosition = new Vector3(0, 0.5f, -10);
        GravityDirection(0, -9.8f);
    }

	private void Update()
	{
		if (gameManager.IsGameOver() || EventSystem.current.currentSelectedGameObject) return;
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
		if (!Input.GetMouseButtonDown(0) || !onGround) return;
		isGameStarted = true;
		onGround = false;
		currentSpeed = playerSpeed;
		RB.gravityScale = gravity *= -1f;
		cameraPosition.y *= -1f;
		//LeanTween.value(gamePlayCamera.GetPosition().y, cameraPosition.y, 0.7f).setEaseOutSine().setOnUpdate(value => cameraPosition.y = value);

        UIDisplay.Instance.CloseStartUI();
	}
	protected override void HandleMovment() 
	{
        mytransform.Translate(new Vector3(currentSpeed * Time.fixedDeltaTime, 0, 0), Space.World);
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
		onGround = true; //cuz it hits the walls 
		if (collision.collider.CompareTag("spike")) Die(); 
	}
}
