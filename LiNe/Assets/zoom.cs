using UnityEngine;

public class zoom : MonoBehaviour
{
    public SpriteRenderer targetsize;
    // Start is called before the first frame update
    private void Awake() => scale();
    private void scale()
    {
        float screenratio = (float)Screen.width / (float)Screen.height;
        float targetratio = targetsize.bounds.size.x / targetsize.bounds.size.y;
        if (screenratio >= targetratio)
        {
            Camera.main.orthographicSize = targetsize.bounds.size.y / 2;
        }
        else
        {
            float diffrentsize = targetratio / screenratio;
            Camera.main.orthographicSize = targetsize.bounds.size.y / 2 * diffrentsize;
        }
    }
}
