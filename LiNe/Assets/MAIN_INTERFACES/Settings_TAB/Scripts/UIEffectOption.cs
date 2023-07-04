using UnityEngine;

public class UIEffectOption : IOptionable
{
    private Canvas canvas;
    [SerializeField] private ScenesData scenesData;
    private void Awake() => canvas = GetComponent<Canvas>();
    public override void SwitchState(SwitchBehaviour switchValue)
    {
        canvas.renderMode = switchValue.GetSwitchState() ? RenderMode.ScreenSpaceCamera : RenderMode.ScreenSpaceOverlay;
        scenesData.renderMode = canvas.renderMode;
    }

    public override object CaptureSate() => canvas.renderMode;

    public override void RestoreState(object state) => canvas.renderMode = (RenderMode)state;
}
