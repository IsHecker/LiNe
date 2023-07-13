using System;
using UnityEngine;
using UnityEngine.EventSystems;
using EZCameraShake;

public class WaveHandler : PlayerBehaviour
{
    [SerializeField] private float smoothCamera;
    [SerializeField] private float gravityForce;
    [SerializeField] private AudioClip scoreup, deafaultsfx;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public AudioSource TapSfx;

    private ParticleSystem tapParticle; //effect played when tapping screen
    void Start()
    {
        playerCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        TapSfx = GetComponent<AudioSource>();
        tapParticle = transform.GetChild(0).transform.GetComponent<ParticleSystem>();
        GravityDirection(-9.8f, 0);
    }

    protected override void CheckInput()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        SpawnColumn.pauseTask = false;
        currentSpeed = playerSpeed;
        rb.gravityScale = gravityForce *= -1;
        tapParticle.Play();
        TapSfx.Play();
        UIDisplay.Instance.CloseStartUI();
    }

    private void CameraFollow()
    {
        cameraPosition = new Vector3(-8, transform.position.y + 2.5f, playerCamera.transform.position.z);
        Vector3 smooth = Vector3.Lerp(playerCamera.transform.position, cameraPosition, smoothCamera * Time.fixedDeltaTime);
        CameraShaker.Instance.RestPositionOffset = smooth;
    }

    protected override void HandleMovment() => transform.Translate(new Vector3(0, currentSpeed * Time.deltaTime), Space.World);

    private void Update()
    {
        if (gameManager.IsGameOver() || EventSystem.current.currentSelectedGameObject) return;
        CheckInput();
        CheckOutOfWidthBounds(transform.position);
        CheckOutOfHeightBounds(transform.position);
    }

    private void FixedUpdate()
    {
        if (gameManager.IsGameOver()) return;

        HandleMovment();
        CameraFollow();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            TapSfx.PlayOneShot(scoreup, 1);
            UIDisplay.Instance.UpdateScoreDisplay(++playerScore, true);
            SpawnColumn.spawnTime -= 0.01f;
            playerSpeed += 0.02f;
            gravityForce += 0.01f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("wall")) Die();
    }

    //public void GameOver()
    //{
    //    canMove = false;
    //    isgameover = true;

    //    //uimanager.Instance.gameoverpanel.SetActive(true);
    //    //uimanager.Instance.pausebtn.SetActive(false);
    //    //stopWorking?.Invoke(true);
    //    //uimanager.Instance.anim.Play("Game Over");
    //    //CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadein, fadeout);
    //    //Money_System.SaveMoney("Money");
    //    //if (Score_System.GetScore() > Score_System.GetHighScore("WaveHighScore"))
    //    //    Score_System.SaveHighScore("WaveHighScore");
    //    //updateScore?.Invoke();



    //    //Camera.main.transform.GetChild(0).gameObject.SetActive(false);
    //    // soundsfx.PlayOneShot(deathsound[deathsoundname], 1);
    //    // //Collect Garbage
    //    // GC.Collect();
    //}


}
