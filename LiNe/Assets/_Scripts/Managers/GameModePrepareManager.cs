using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModePrepareManager : MonoBehaviour
{
    [SerializeField] private GameObject GamePlaySetup;
    [SerializeField] private GameObject waveHead;

    [SerializeField] private Transform Player;
    [SerializeField] private Transform cameraHolder;

    [SerializeField] private CanvasGroup lineTitle;

    [SerializeField] private IntroPrepare introPrepare;

    [SerializeField] private D_GameModeSetup waveData;

    private Animator _headAnimator;
    private TrailRenderer _headTrail;

    private void Start()
    {
        _headTrail = Player.GetComponentInChildren<TrailRenderer>();
        _headAnimator = Player.GetComponentInChildren<Animator>();
    }
    [SerializeField] private ColumnSpawner columnSpawner;
    public void WaveGameSetup()
    {
        PrepareGame();
    }
    public void PrepareGame()
    {
        //player = FindAnyObjectByType<PlayerBehaviour>();
        //RB = player.GetComponent<Rigidbody2D>();
        //player.AddDeadEvent(GameOver);

        _headTrail.time = 2;
        Helpers.Update(() =>
        {
            if (Player.GetChild(0).localPosition.x >= -0.1f && Player.GetChild(0).localPosition.x <= 0.1f)
            {
                Player.GetChild(0).localPosition = new Vector2(0, Player.GetChild(0).localPosition.y);
                _headAnimator.enabled = false;
                introPrepare.enabled = false;
                waveHead.SetActive(true);
            }
        },
        () => _headAnimator.enabled);
        GamePlaySetup.SetActive(true);
        LineTitleDisplay(false);
        SelectionMenuUIEvents.Instance.animator.CrossFade("SelectMode_UI REVERSED 0", 0, 0);
    }
    public void TargetCamera(Vector2 position, float time) => LeanTween.moveLocal(cameraHolder.gameObject, new Vector3(position.x, position.y, -100), time).setEaseOutQuart();
    public void TargetPlayerPosition(Vector2 position, float time) => Player.LeanMoveLocal(position, time).setEaseOutQuart();
    private void LineTitleDisplay(bool state)
    {
        if (state)
            LeanTween.value(lineTitle.alpha, 1, 0.5f).setEaseOutQuart().setOnUpdate(value => lineTitle.alpha = value);
        else
            LeanTween.value(lineTitle.alpha, 0, 0.5f).setEaseOutQuart().setOnUpdate(value => lineTitle.alpha = value);
    }
}
