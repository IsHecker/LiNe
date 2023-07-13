using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Fading : MonoBehaviour
{

	public Image img;
	public AnimationCurve curve;
	public GameObject[] canvasholder;
    void Start()
	{
		StartCoroutine(FadeIn());
	}

    public void FadeTo(string scenename) => StartCoroutine(FadeOut(scenename));

    public void Canvastranstion(string UIName) => StartCoroutine(CanvasFadeOut(UIName));

    IEnumerator FadeIn()
	{
		float t = 1f;

		while (t > 0f)
		{
			t -= Time.deltaTime * 2;
			float a = curve.Evaluate(t);
			img.color = new Color(0f, 0f, 0f, a);
			yield return 0;
		}
		img.raycastTarget = false;
	}

	IEnumerator FadeOut(string scene)
	{
		float t = 0f;
		img.raycastTarget = true;
		while (t < 1f)
		{
			t += Time.unscaledDeltaTime * 2;
			float a = curve.Evaluate(t);
			img.color = new Color(0f, 0f, 0f, a);
			yield return 0;
		}
		Time.timeScale = 1f;
		SceneManager.LoadScene(scene);
	}

	IEnumerator CanvasFadeOut(string CanvasNameToOpen)
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