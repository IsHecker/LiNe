﻿using UnityEngine;
using UnityEngine.EventSystems;

public class WaveHandler : PlayerBehaviour
{
    [SerializeField] private float gravityForce;
    [SerializeField] private CameraController gameplayCamera;
    [SerializeField] private BestScoreIndicator bestScoreIndicator;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public AudioSource TapSfx;

    private ParticleSystem tapParticle; //effect played when tapping screen
    private Transform mytransform;

    private Vector2 playerVelocity;


    void Start()
    {
        mytransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        TapSfx = GetComponent<AudioSource>();
        tapParticle = transform.GetChild(0).transform.GetComponent<ParticleSystem>();
        GravityDirection(-9.8f, 0);
    }

    protected override void CheckInput()
    {
        if (!TouchInput()) return;

        ColumnSpawner.pauseTask = false;

        currentSpeed = playerSpeed;

        rb.gravityScale = gravityForce *= -1;


        tapParticle.Play();
        AudioManager.Instance?.PlaySound(AudioHolder, "Tap");

        UIDisplay.Instance.CloseStartUI();
    }


    protected override void HandleMovment()
    {
        playerVelocity.Set(0, currentSpeed * Time.deltaTime);
        mytransform.Translate(playerVelocity, Space.World);
    }

    private void Update()
    {
        if (gameManager.IsGameOver()) return;

        CheckInput();
        CheckOutOfWidthBounds(mytransform.position);
    }

    private void FixedUpdate()
    {
        if (gameManager.IsGameOver()) return;

        HandleMovment();
        gameplayCamera.FollowY(mytransform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            UIDisplay.Instance.UpdateScoreDisplay(++playerScore, true);
            playerSpeed += 0.02f;
            gravityForce += 0.01f;

            bestScoreIndicator.SetBestScorePosition(new Vector3(gameplayCamera.CameraWidth, other.transform.position.y));
        }
    }

    private void OnCollisionEnter2D(Collision2D other) => Die();

    private bool TouchInput()
    {
        if (Input.touchCount <= 0)
            return false;

        Touch touch = Input.GetTouch(0);
        return Helpers.IsOverUI(touch);
    }
}
