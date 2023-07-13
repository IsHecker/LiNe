using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaController : MonoBehaviour
{
    public float alphaspeed;
    float alphanumber;
    public bool flashstyle = true, fadein, fadeout;
    CanvasGroup Alphas;
    private void Start()
    {
        Alphas = GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        if (flashstyle)
        {
            if (Alphas.alpha >= alphanumber - 0.1f)
                alphanumber = 0;
            if (Alphas.alpha <= alphanumber + 0.1f)
                alphanumber = 1;
        }
        if (fadeout)
        {
            if (Alphas.alpha >= alphanumber - 0.1f)
                alphanumber = 0;
        }
        if (fadein)
        {
            if (Alphas.alpha <= alphanumber + 0.1f)
                alphanumber = 1;
        }
        Alphas.alpha = Mathf.Lerp(Alphas.alpha, alphanumber, Time.unscaledDeltaTime * alphaspeed);
    }
}
