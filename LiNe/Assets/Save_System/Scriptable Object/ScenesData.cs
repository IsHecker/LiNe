using UnityEngine;

[CreateAssetMenu( menuName = "SceneData")]
public class ScenesData : ScriptableObject
{
    public static bool firstTimeOpened = false;
    public bool fpsActiveState = true;
    public RenderMode renderMode = RenderMode.ScreenSpaceCamera;
    public static Color trailColor = Color.white;
}
