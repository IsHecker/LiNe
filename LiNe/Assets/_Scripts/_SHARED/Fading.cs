using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Fading : MonoBehaviour
{
	[SerializeField] private Image img;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private GameObject[] canvasholder;

	[SerializeField] private float fadeInSpeed = 2;
	[SerializeField] private float fadeOutSpeed = 2;

	[SerializeField] private float fadeInDelay = 1f;
	[SerializeField] private float fadeOutDelay = 1f;

    void Start()
    {
		if (ScenesData.isFirstTimeOpenedGame && SceneManager.GetActiveScene().name == "Main Menu")
			fadeInDelay = 1f;

        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scenename) => StartCoroutine(FadeOut(scenename));

    public void Canvastranstion(string UIName) => StartCoroutine(CanvasFadeOut(UIName));

    private IEnumerator FadeIn()
	{
		float t = fadeInDelay;

		while (t > 0f)
		{
			t -= Time.deltaTime * fadeInSpeed;
			float a = curve.Evaluate(t);
			img.color = new Color(0f, 0f, 0f, a);
			yield return 0;
		}

		img.raycastTarget = false;
	}

    private IEnumerator FadeOut(string scene)
    {
        float t = 0f;
        img.raycastTarget = true;
        while (t < fadeOutDelay)
        {
            t += Time.unscaledDeltaTime * fadeOutSpeed;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }

    private IEnumerator CanvasFadeOut(string CanvasNameToOpen)
	{
		float t = 0f;
		img.raycastTarget = true;
		while (t < 1.2f)
		{
			t += Time.unscaledDeltaTime * 2;
			float a = curve.Evaluate(t);
			img.color = new Color(0f, 0f, 0f, a);
			yield return 0;
		}
		canv(CanvasNameToOpen);
		StartCoroutine(FadeIn());
	}

	public void canv(string canvasname)
	{
		string currentname;
		for (int i = 0; i < canvasholder.Length; i++)
		{
			currentname = canvasholder[i].transform.name;
			canvasholder[i].gameObject.SetActive(currentname == canvasname);
			if (currentname != canvasname)
			{
				canvasholder[i].gameObject.SetActive(false);
			}
		}
	}
}