using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject parent;
    [SerializeField] private int Time;
    private bool isDestroyed = false;

    private void Start() => LeanTween.value(slider.maxValue, 0, Time).setDelay(0.15f).setOnUpdate(value => { if (isDestroyed) return; slider.value = value; }).setOnComplete(Disappear);
    private void Disappear()
    {
        if (isDestroyed) return;
        circleCollider.enabled = false;
        LeanTween.value(canvasGroup.alpha, 0, 0.5f).
    setOnUpdate(value => canvasGroup.alpha = value).
    setOnComplete(_ => Destroy(parent));
    }
    private void OnDestroy() => isDestroyed = true;
}
