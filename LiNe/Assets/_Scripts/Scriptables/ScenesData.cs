using UnityEngine;

[CreateAssetMenu( menuName = "SceneData")]
public class ScenesData : ScriptableObject
{
    public static bool isFirstTimeOpenedGame = true;

    public bool fpsActiveState = true;

    public RenderMode renderMode = RenderMode.ScreenSpaceCamera;

    public static Color trailColor = Color.white;
}
